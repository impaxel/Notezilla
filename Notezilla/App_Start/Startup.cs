using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using NHibernate;
using NHibernate.AspNet.Identity;
using NHibernate.AspNet.Identity.Helpers;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Event;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using Notezilla.App_Start;
using Notezilla.Controllers;
using Notezilla.Models.Listeners;
using Notezilla.Models.Notes;
using Notezilla.Models.Repositories;
using Notezilla.Models.Users;
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

            foreach (var type in assembly.GetTypes())
            {
                var attr = (ListenerAttribute)type.GetCustomAttribute(typeof(ListenerAttribute));
                if (attr != null)
                {
                    var interfaces = type.GetInterfaces();
                    var b = builder.RegisterType(type);
                    foreach (var inter in interfaces)
                    {
                        b = b.As(inter);
                    }
                }
            }
            Type[] types = new Type[] { typeof(User) };
            builder.Register(x =>
            {
                var cfg = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012
                        .ConnectionString(connectionString.ConnectionString)
                        .Dialect<MsSql2012Dialect>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Note>())
                    .ExposeConfiguration(c =>
                    {
                        SchemaMetadataUpdater.QuoteTableAndColumns(c);
                        c.AddDeserializedMapping(MappingHelper.GetIdentityMappings(types), null);
                        c.EventListeners.PreInsertEventListeners = x.Resolve<IPreInsertEventListener[]>();
                        c.EventListeners.PreUpdateEventListeners = x.Resolve<IPreUpdateEventListener[]>();
                    })
                    .CurrentSessionContext("call");
                var conf = cfg.BuildConfiguration();
                var schemaExport = new SchemaUpdate(conf);
                schemaExport.Execute(true, true);
                ISessionFactory session = conf.BuildSessionFactory();
                InitialData(session);
                return session;
            }).As<ISessionFactory>().SingleInstance();
            builder.Register(x => x.Resolve<ISessionFactory>().OpenSession())
                .As<ISession>()
                .InstancePerRequest();
            //builder.Register(x => x.Resolve<ISessionFactory>().OpenSession())
            //    .As<ISession>()
            //    .InstancePerDependency();
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
                new UserManager<User>(new UserStore<User>(DependencyResolver.Current.GetServices<ISession>().FirstOrDefault())));
            app.CreatePerOwinContext(() =>
                new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(DependencyResolver.Current.GetServices<ISession>().FirstOrDefault())));
            app.CreatePerOwinContext<SignInManager<User, string>>((options, context) =>
                new SignInManager<User, string>(context.GetUserManager<UserManager<User>>(), context.Authentication));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Signin"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<UserManager<User>, User>(TimeSpan.FromMinutes(1),
                    (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
        }

        public static void InitialData(ISessionFactory sessionFactory)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                var roleAdmin = new IdentityRole("Admin");
                var roleUser = new IdentityRole("User");
                
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(session));
                var userManager = new UserManager<User>(new UserStore<User>(session));
                roleManager.CreateAsync(roleUser);
                roleManager.CreateAsync(roleAdmin);
                var userAdmin = new User("admin");
                var result = userManager.CreateAsync(userAdmin, "admin");
                if (result.Result.Succeeded)
                {
                    userManager.AddToRoleAsync(userAdmin.Id, roleAdmin.Name);
                    userManager.AddToRoleAsync(userAdmin.Id, roleUser.Name);
                    userManager.SetLockoutEnabled(userAdmin.Id, false);
                }
            }
        }
    }
}