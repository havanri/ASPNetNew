using ASPWebsiteShopping.Data;
using ASPWebsiteShopping.Models;

namespace ASPWebsiteShopping.Services
{
    public class MenuService : IMenuService
    {
        private readonly ApplicationDbContext _db;
        public MenuService(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Menu> GetAllMenus()
        {
            return _db.Menus.Where(e => e.DeletedAt == null).ToList();
        }
        public Menu GetMenuById(int? id)
        {
            if (id == null || id == 0)
            {
                return null;
            }
            var Menu = _db.Menus.Where(e => e.DeletedAt == null).FirstOrDefault(x => x.Id == id);
            return Menu;
        }
        public void DeleteByObj(Menu Menu)
        {
            _db.Menus.Remove(Menu);
            _db.SaveChanges();
        }
        public void UpdateMenu(Menu Menu)
        {
            _db.Menus.Update(Menu);
            _db.SaveChanges();
        }
        public void AddMenu(Menu Menu)
        {
            _db.Menus.Add(Menu);
            _db.SaveChanges();
        }
    }
}
