using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Models.Helpers
{
    public class DropDownItem
    {
        public string Value { get; set; }
        public string Text { get; set; }

        public DropDownItem()
        {

        }

        public DropDownItem(string value, string text)
        {
            this.Value = value;
            this.Text = text;
        }
    }
}