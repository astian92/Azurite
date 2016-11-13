using Azurite.Store.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Azurite.Store.Common
{
    public static class ApplicationHelpers
    {
        public static string BuildBreadcrumbNavigation(this HtmlHelper helper)
        {
            // optional condition: I didn't wanted it to show on home and account controller
            if (helper.ViewContext.RouteData.Values["controller"].ToString() == "Home" ||
                helper.ViewContext.RouteData.Values["controller"].ToString() == "Account")
            {
                return string.Empty;
            }

            StringBuilder breadcrumb = new StringBuilder("<div class=\"breadcrumb\"><li>").Append(helper.ActionLink("Home", "Index", "Home").ToHtmlString()).Append("</li>");


            breadcrumb.Append("<li>");
            breadcrumb.Append(helper.ActionLink(helper.ViewContext.RouteData.Values["controller"].ToString().Titleize(),
                                               "Index",
                                               helper.ViewContext.RouteData.Values["controller"].ToString()));
            breadcrumb.Append("</li>");

            if (helper.ViewContext.RouteData.Values["action"].ToString() != "Index")
            {
                breadcrumb.Append("<li>");
                breadcrumb.Append(helper.ActionLink(helper.ViewContext.RouteData.Values["action"].ToString().Titleize(),
                                                    helper.ViewContext.RouteData.Values["action"].ToString(),
                                                    helper.ViewContext.RouteData.Values["controller"].ToString()));
                breadcrumb.Append("</li>");
            }

            return breadcrumb.Append("</div>").ToString();
        }
         
        public static MvcHtmlString CategoryTree(this HtmlHelper html, Guid activeCategory, IEnumerable<CategoryW> allCategories, IEnumerable<CategoryW> categories)
        {
            string htmlOutput = string.Empty;

            if (categories.Count() > 0)
            {
                if(categories.Any(x => x.ParentId != null) && !categories.Any(x => x.Id == activeCategory))
                {
                    htmlOutput += "<ul class=\"sub-nav collapse\">";
                }
                else
                {
                    htmlOutput += "<ul>";
                }

                foreach (var category in categories)
                {
                    if(category.Id == activeCategory)
                    {
                        htmlOutput += "<li class=\"active\">";
                    }
                    else
                    {
                        htmlOutput += "<li>";
                    }

                    htmlOutput += "<a href=\"/Categories/Index/" + category.Id + "\">" + category.Name + "</a>";
                    htmlOutput += html.CategoryTree(activeCategory, allCategories, allCategories.Where(x => x.ParentId == category.Id));
                    htmlOutput += "</li>";
                }
                htmlOutput += "</ul>";
            }

            return MvcHtmlString.Create(htmlOutput);
        }
    }
}