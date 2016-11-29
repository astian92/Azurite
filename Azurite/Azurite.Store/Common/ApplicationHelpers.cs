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

        public static MvcHtmlString CategoryTreeMenu(this HtmlHelper html, IEnumerable<CategoryW> allCategories)
        {
            string htmlOutput = string.Empty;

            return MvcHtmlString.Create(htmlOutput);
        }

        public static MvcHtmlString Price(this HtmlHelper html, double price, double discount = 0)
        {
            //calculate the price with the discount
            double priceOff = price * discount / 100;
            //round the discount
            double rounded = Math.Round(priceOff, 2, MidpointRounding.AwayFromZero);
            double total = price - rounded;

            //calculate hole part and coins
            double hole = Math.Truncate(total);
            double coins = Math.Truncate((total - hole) * 100); 

            //build the html
            string htmlOutput = String.Empty;
            htmlOutput = "<span class=\"price-new\"><i class=\"fa fa-euro\"></i>" + hole + ".<sup>" + (coins == 0 ? "00" : coins < 10 ? "0" + coins.ToString() : coins.ToString()) + "</sup></span>";
            if(discount > 0)
            {
                //calculate hole part and coins of the old price
                double holeOld = Math.Truncate(price);
                double coinsOld = Math.Truncate((price - holeOld) * 100);
                htmlOutput += "<span class=\"price-old\"><i class=\"fa fa-euro\"></i>" + holeOld + ".<sup>" + (coinsOld == 0 ? "00" : coinsOld < 10 ? "0" + coinsOld.ToString() : coinsOld.ToString()) + "</sup></span>";
            }

            return MvcHtmlString.Create(htmlOutput);
        }

        public static MvcHtmlString TotalPrice(this HtmlHelper html, double price, double discount = 0)
        {
            //calculate the price with the discount
            double priceOff = price * discount / 100;
            //round the discount
            double rounded = Math.Round(priceOff, 2, MidpointRounding.AwayFromZero);
            double total = price - rounded;

            //calculate hole part and coins
            double hole = Math.Truncate(total);
            double coins = Math.Truncate((total - hole) * 100);

            //build the html
            string htmlOutput = String.Empty;
            htmlOutput = "<span class=\"price-new\"><i class=\"fa fa-euro\"></i>" + hole + ".<sup>" + (coins == 0 ? "00" : coins < 10 ? "0" + coins.ToString() : coins.ToString()) + "</sup></span>";
            return MvcHtmlString.Create(htmlOutput);
        }
    }
}