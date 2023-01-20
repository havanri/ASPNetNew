using ASPWebsiteShopping.Models;
using NPOI.SS.Formula.Functions;

namespace ASPWebsiteShopping.Components
{
	public class GetOptionByAttribute
	{
        private string htmlOption = "";
		private int? _attributeId;
        private List<Species> _data;
        public GetOptionByAttribute(int? attributeId, List<Species> data)
		{
			_attributeId = attributeId;
			_data = data;
		}
		public string ReturnHtmlOption(List<Species>? speciesList)
		{
            string option = "";
			foreach (var itemSpecies in _data)
			{
				if (speciesList != null)//edit
				{
                    bool containsItem = speciesList.Any(e => e.Name.Equals(itemSpecies.Name));
                    if (containsItem == true)
                    {
                        option = "<option selected value=" + itemSpecies.Id + " >" + itemSpecies.Name + "</option>";
                        htmlOption = String.Concat(htmlOption, option);
                    }
                    else
                    {
                        option = "<option value=" + itemSpecies.Id + " >" + itemSpecies.Name + "</option>";
                        htmlOption = String.Concat(htmlOption, option);
                    }
                }
                else//create
                {
                    option = "<option value=" + itemSpecies.Id + " >" + itemSpecies.Name + "</option>";
                    htmlOption = String.Concat(htmlOption, option);
                }  
            }
			return htmlOption;
		}
    }
}
