using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using NHibernate;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;
using Notezilla.App_Start;
using Notezilla.Auth;
using Notezilla.Controllers;
using Notezilla.Models.Repositories;
using Notezilla.Models.Users;
using Notezilla.Permission;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Notezilla.App_Start
{
    public partial class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var connectionString = ConfigurationManager.ConnectionStrings["MSSQL"];
            if (connectionString == null)
            {
                throw new Exception("Не найдена строка соединения");
            }

            var assembly = Assembly.GetAssembly(typeof(User));
            var builder = new ContainerBuilder();

            builder.Register(x =>
            {
                var cfg = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012
                        .ConnectionString(connectionString.ConnectionString)
                        .Dialect<MsSql2012Dialect>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<User>())
                    .ExposeConfiguration(c =>
                    {
                        SchemaMetadataUpdater.QuoteTableAndColumns(c);
                    })
                    .CurrentSessionContext("call");
                var conf = cfg.BuildConfiguration();
                var schemaExport = new SchemaUpdate(conf);
                schemaExport.Execute(true, true);
                ISessionFactory session = cfg.BuildSessionFactory();
                InitialData(session);
                return session;
            }).As<ISessionFactory>().SingleInstance();
            builder.Register(x => x.Resolve<ISessionFactory>().OpenSession())
                .As<ISession>()
                .InstancePerRequest();
            builder.RegisterControllers(Assembly.GetAssembly(typeof(BaseController)));
            builder.RegisterModule(new AutofacWebTypesModule());
            foreach (var type in assembly.GetTypes())
            {
                var attr = type.GetCustomAttribute(typeof(RepositoryAttribute));
                if (attr != null)
                {
                    builder.RegisterType(type);
                }
            }
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            app.UseAutofacMiddleware(container);

            app.CreatePerOwinContext(() =>
                new UserManager(new IdentityStore(DependencyResolver.Current.GetServices<ISession>().FirstOrDefault())));
            app.CreatePerOwinContext(() =>
                new RoleManager(new RoleStore(DependencyResolver.Current.GetServices<ISession>().FirstOrDefault())));
            app.CreatePerOwinContext<SignInManager>((options, context) =>
                new SignInManager(context.GetUserManager<UserManager>(), context.Authentication));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Signin"),
                Provider = new CookieAuthenticationProvider()
            });
        }

        public static void InitialData(ISessionFactory sessionFactory)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                var roleAdmin = new Role("Admin");
                var roleUser = new Role("User");
                var roleManager = new RoleManager(new RoleStore(session));
                var userManager = new UserManager(new IdentityStore(session));
                roleManager.CreateAsync(roleUser);
                roleManager.CreateAsync(roleAdmin);
                var userAdmin = new User("admin");
                var result = userManager.CreateAsync(userAdmin, "admin");
                if (result.Result.Succeeded)
                {
                    userManager.AddToRoleAsync(userAdmin.Id, roleAdmin.Name);
                    userManager.AddToRoleAsync(userAdmin.Id, roleUser.Name);
                }
            }
        }
    }
}