using ASPWebsiteShopping.Models;

namespace ASPWebsiteShopping.Services
{
    public interface ISettingService
    {
        List<Setting> GetList();
        Setting Get(int id);
        void DeleteById(Setting setting);
        Setting GetSettingById(int? id);

        void Insert(SettingViewModel viewModel);
        void Update(SettingViewModel viewModel);


    }
}