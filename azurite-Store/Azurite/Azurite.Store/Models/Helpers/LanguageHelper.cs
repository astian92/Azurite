using Azurite.Infrastructure.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Store.Models.Helpers
{
    public enum Language
    {
        BG = 0,
        EN = 1
    }

    public static class LanguageHelper
    {
        public static Language GetCurrentLanguage()
        {
            var cookie = HttpContext.Current.Request.Cookies["Language"];
            if(cookie != null && cookie.Value != null)
            {
                switch (cookie.Value)
                {
                    case "bg":
                        return Language.BG;
                    case "en":
                        return Language.EN;
                    default:
                        return Language.BG;
                }
            }

            return Language.BG;
        } 

    }
}