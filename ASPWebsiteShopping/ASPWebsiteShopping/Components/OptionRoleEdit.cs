using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;

namespace ASPWebsiteShopping.Components
{
    public class OptionRoleEdit
    {
        private string _htmlOption="";
        private IList<string> _roleOfUser;
        private List<IdentityRole> _roleAll;

        public OptionRoleEdit(IList<string> roleOfUser,List<IdentityRole> roleAll)
        {
            _roleAll = roleAll;
            _roleOfUser = roleOfUser;
        }
        public string returnOptionRole()
        {
            string option = "";
            foreach (var role in _roleAll)
            {
                bool checkContain = _roleOfUser.Any(e => e.Equals(role.Name));
                if (checkContain)
                {
                    option = "<option selected value=" + role.Name + " >" + role.Name + "</option>";
                    _htmlOption = String.Concat(_htmlOption, option);
                }
                else
                {
                    option = "<option value=" + role.Name + " >" + role.Name + "</option>";
                    _htmlOption = String.Concat(_htmlOption, option);
                }
            }
            return _htmlOption;
        }
    }
}
