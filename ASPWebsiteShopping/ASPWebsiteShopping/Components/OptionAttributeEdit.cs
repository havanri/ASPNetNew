using ASPWebsiteShopping.Models;

namespace ASPWebsiteShopping.Components
{
    public class OptionAttributeEdit
    {
        private string _htmlOption;
        private List<ProductAttribute> _attributeFull;
        private List<ProductAttribute> _attributeContains;

        public OptionAttributeEdit(List<ProductAttribute> attributeFull, List<ProductAttribute> attributeContains)
        {
            _attributeFull = attributeFull;
            _attributeContains = attributeContains;
        }
        public string ReturnOptionAttribute()
        {
            foreach (var attribute in _attributeFull)
            {
                //check xem attribute nay co ton tai trong danh sach thuoc tinh cua san pham nay k
                bool containsItem = _attributeContains.Any(item => item.Name == attribute.Name);
                if (containsItem == true)//co ton tai
                {
                    string option = "<option disabled value=" +attribute.Id+" >"+attribute.Name+"</option>";
                    _htmlOption = String.Concat(_htmlOption, option);
                }
                else
                {
                    string option = "<option value=" + attribute.Id + " >" + attribute.Name + "</option>";
                    _htmlOption = String.Concat(_htmlOption, option);
                }
            }
            return _htmlOption;
        }
    }
}
