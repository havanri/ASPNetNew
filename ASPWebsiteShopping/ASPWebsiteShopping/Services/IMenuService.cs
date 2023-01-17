using ASPWebsiteShopping.Models;

namespace ASPWebsiteShopping.Services
{
	public interface IMenuService
	{
		void AddMenu(Menu Menu);
		void DeleteByObj(Menu Menu);
		IEnumerable<Menu> GetAllMenus();
		Menu GetMenuById(int? id);
		void UpdateMenu(Menu Menu);
	}
}