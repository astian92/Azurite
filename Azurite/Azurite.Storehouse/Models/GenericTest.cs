using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Models
{
    public class GenericTest
    {
        public int Get<TProp>(TProp key)
        {
            if (typeof(TProp) == typeof(int))
            {
                return Convert.ToInt32(key);
            }

            return 0;
        }
    }
}