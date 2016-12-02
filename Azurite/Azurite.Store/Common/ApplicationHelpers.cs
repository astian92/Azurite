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

        public static MvcHtmlString Price(this HtmlHelper html, double itemPrice, double discount = 0)
        {
            var currency = GetCurrentCurrency();
            double price = itemPrice / currency.Value;

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
            htmlOutput = "<span class=\"price-new\">" + hole + ".<sup>" + (coins == 0 ? "00" : coins < 10 ? "0" + coins.ToString() : coins.ToString()) + "</sup><span>" + currency.Sign + "</span></span>";
            if(discount > 0)
            {
                //calculate hole part and coins of the old price
                double holeOld = Math.Truncate(price);
                double coinsOld = Math.Truncate((price - holeOld) * 100);
                htmlOutput += "<span class=\"price-old\">" + holeOld + ".<sup>" + (coinsOld == 0 ? "00" : coinsOld < 10 ? "0" + coinsOld.ToString() : coinsOld.ToString()) + "</sup><span>" + currency.Sign + "</span></span>";
            }

            return MvcHtmlString.Create(htmlOutput);
        }

        public static MvcHtmlString TotalPrice(this HtmlHelper html, double itemPrice, double discount = 0)
        {
            var currency = GetCurrentCurrency();
            double price = itemPrice / currency.Value;

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
            htmlOutput = "<span class=\"price-new\">" + hole + ".<sup>" + (coins == 0 ? "00" : coins < 10 ? "0" + coins.ToString() : coins.ToString()) + "</sup><span>" + currency.Sign + "</span></span>";
            return MvcHtmlString.Create(htmlOutput);
        }

        public static string CurrentCurrency(this HtmlHelper html)
        {
            var currency = GetCurrentCurrency();
            return currency.Code;
        }

        private static CurrencyCoursW GetCurrentCurrency()
        {
            var currency = new CurrencyCoursW();
            var cookie = HttpContext.Current.Request.Cookies["Currency"];
            if (cookie != null && !String.IsNullOrEmpty(cookie.Value))
            {
                var currencyParts = cookie.Value.Split('|');
                currency.Id = int.Parse(currencyParts[0]);
                currency.Code = currencyParts[1];
                currency.Value = double.Parse(currencyParts[2]);
                currency.Sign = currencyParts[3];
            }
            else
            {
                currency.Id = 0;
                currency.Code = "BG";
                currency.Value = 1;
                currency.Sign = "лв";
            }

            return currency;
        }
    }
}