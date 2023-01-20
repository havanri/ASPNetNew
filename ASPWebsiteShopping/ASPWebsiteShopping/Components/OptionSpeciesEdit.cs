using ASPWebsiteShopping.Models;
using NPOI.POIFS.Crypt.Dsig;

namespace ASPWebsiteShopping.Components
{
	public class OptionSpeciesEdit
	{
		private string _htmlOption;
		private List<ProductAttribute> _productAttributes;
		private List<Species> _speciesList;

		public OptionSpeciesEdit(List<ProductAttribute> productAttributes, List<Species> speciesList)
		{
			_productAttributes = productAttributes;
			_speciesList = speciesList;
		}

		public string ReturnSpeciesOptionHtml()
		{
			foreach(var itemAttribute in _productAttributes)
			{
                GetOptionByAttribute optionSpecies = new GetOptionByAttribute(itemAttribute.Id, itemAttribute.ListSpecies);
                string option = "<div class='species'><label>Tên:" + itemAttribute.Name + "</label><br/><label> Giá trị(s):</label ><div class='select2-purple' ><select name='attribute_values[]' class='js-species-select' multiple='multiple' data-placeholder='Chọn tên chủng loại' data-dropdown-css-class='select2-purple' style='width: 100%;'>"+optionSpecies.ReturnHtmlOption(_speciesList) +"</select></div></div> ";
                _htmlOption = String.Concat(_htmlOption, option);
            }
            return _htmlOption;
        }
	}
}
