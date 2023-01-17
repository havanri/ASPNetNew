using ASPWebsiteShopping.Models;

namespace ASPWebsiteShopping.Components
{
	public class MenuRecusive
	{
        private string htmlSelect = "";
        private IEnumerable<Menu> data;

        public MenuRecusive(IEnumerable<Menu> data)
        {
            this.data = data;
        }
        public string RecusiveModel(string ParentId, int id = 0, string text = "")
        {
            foreach (var value in data)
            {
                if (value.ParentId == id)
                {
                    if (!string.IsNullOrEmpty(ParentId) && int.Parse(ParentId) == value.Id)
                    {
                        string option = "<option selected value=" + value.Id + " >" + text + value.Name + "</option>";
                        htmlSelect = String.Concat(htmlSelect, option);
                    }
                    else
                    {
                        string option = "<option value=" + value.Id + " >" + text + value.Name + "</option>";
                        htmlSelect = String.Concat(htmlSelect, option);
                    }
                    RecusiveModel(ParentId, value.Id, text + "--");
                }
            }
            return htmlSelect;
        }
    }
}
