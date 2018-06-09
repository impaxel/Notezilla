using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Notezilla.Models;

namespace Notezilla.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString SortLink(this HtmlHelper html, string linkText, string sortExpression)
        {
            var controller = html.ViewContext.RouteData.Values["controller"].ToString();
            var action = html.ViewContext.RouteData.Values["action"].ToString();

            var routeValues = new RouteValueDictionary();
            SortDirection? sort = null;
            var sortDirectionStr = html.ViewContext.HttpContext.Request["SortDirection"];
            if (!string.IsNullOrEmpty(sortDirectionStr)
                && html.ViewContext.HttpContext.Request["SortExpression"] == sortExpression)
            {
                if (Enum.TryParse(sortDirectionStr, out SortDirection s))
                {
                    sort = s;
                }
            }
            routeValues["SortExpression"] = sortExpression;
            routeValues["SortDirection"] = sort.HasValue && sort.Value == SortDirection.Ascending ?
                SortDirection.Descending : SortDirection.Ascending;
            return html.Partial("SortLink", new SortLinkModel
            {
                ActionName = action,
                ControllerName = controller,
                SortDirection = sort,
                RouteValues = routeValues,
                LinkText = linkText
            });
        }
    }
}