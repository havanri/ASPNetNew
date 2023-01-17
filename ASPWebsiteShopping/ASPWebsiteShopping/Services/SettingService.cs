using ASPWebsiteShopping.Data;
using ASPWebsiteShopping.Models;

namespace ASPWebsiteShopping.Services
{
    public class SettingService : ISettingService
    {
        private readonly ApplicationDbContext _db;
        public SettingService(ApplicationDbContext db)
        {
            _db = db;
        }

        public void DeleteById(Setting setting)
        {
          _db.Settings.Remove(setting);
            _db.SaveChanges();
        }

        public Setting Get(int id)
        {
            var rs = _db.Settings.Where(e=>e.Id == id && e.DeletedAt==null).Select(e=> new Setting
            {
                Id = e.Id,
                ConfigKey = e.ConfigKey,
                ConfigValue = e.ConfigValue,
            }).FirstOrDefault();
            return rs;
        }

        public List<Setting> GetList()
        {
            var rs = _db.Settings.Select(e=> new Setting
            {
                Id = e.Id,
                ConfigKey = e.ConfigKey,
                ConfigValue = e.ConfigValue,
            }).ToList();
            return rs;
        }

        public Setting GetSettingById(int? id)
        {
           if(id == null || id == 0)
            {
                return null;

            }
           var setting = _db.Settings.Where(e => e.Id == id).FirstOrDefault(e=>e.Id == id);
            return setting;
        }

        public void Insert(SettingViewModel viewModel)
        {
            var setting = new Setting
            {
                ConfigKey = viewModel.Setting.ConfigKey,
                ConfigValue = viewModel.Setting.ConfigValue,
                CreatedAt = DateTime.Now
            };
            _db.Settings.Add(setting);
            _db.SaveChanges();
        }

        public void Update(SettingViewModel viewModel)
        {
            var rs = _db.Settings.Where(e => e.Id == viewModel.Setting.Id).FirstOrDefault();
            if(rs != null)
            {
                rs.ConfigKey = viewModel.Setting.ConfigKey;
                rs.ConfigValue = viewModel.Setting.ConfigValue; 
                rs.UpdatedAt= DateTime.Now;
                _db.Settings.Update(rs);
                _db.SaveChanges();
            }
        }
    }
}
