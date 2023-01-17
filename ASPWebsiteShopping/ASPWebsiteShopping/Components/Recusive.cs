using ASPWebsiteShopping.Models;
using NPOI.SS.Formula.Functions;

namespace ASPWebsiteShopping.Components
{
    public class Recusive
    {
        private string htmlSelect="";
        private IEnumerable<Category> data;

        public Recusive(IEnumerable<Category> data)
        {
            this.data = data;
        }
        public string RecusiveModel(string ParentId,int id=0,string text="")
        {
            foreach (var value in data)
            {
                if (value.ParentId == id)
                {
                    if(!string.IsNullOrEmpty(ParentId) &&  int.Parse(ParentId)== value.Id)
                    {
                        string option = "<option selected value=" + value.Id + " >" + text + value.Name + "</option>";
                        htmlSelect =String.Concat(htmlSelect, option) ;
                    }
                    else
                    {
                        string option = "<option value=" + value.Id + " >" + text + value.Name + "</option>";
                        htmlSelect = String.Concat(htmlSelect, option);
                    }
                    RecusiveModel(ParentId,value.Id, text+"--");
                }
            }
            return htmlSelect;
        }
    }
}
