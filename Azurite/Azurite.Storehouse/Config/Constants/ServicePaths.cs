using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Azurite.Storehouse.Config.Constants
{
    public static class ServicePaths
    {
        public static string CDN;
        public static string Files;
        public static string Save;
        public static string Remove;

        static ServicePaths()
        {
            string cdn = WebConfigurationManager.AppSettings["CdnAddress"];

            CDN = cdn;
            Files = cdn + "files/";
            Save = cdn + "save/";
            Remove = cdn + "remove/";
        }
    }
}