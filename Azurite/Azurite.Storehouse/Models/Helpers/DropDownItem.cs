namespace Azurite.Storehouse.Models.Helpers
{
    public class DropDownItem
    {
        public DropDownItem()
        {
        }

        public DropDownItem(string value, string text)
        {
            this.Value = value;
            this.Text = text;
        }

        public string Value { get; set; }

        public string Text { get; set; }
    }
}