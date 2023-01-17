using ASPWebsiteShopping.Models;

namespace ASPWebsiteShopping.Services
{
    public interface ISliderService
    {
        List<Slider> GetList();
        Slider Get(int id);
        void DeleteById(Slider slider);
        Slider GetSliderById(int? id);
       
        void Insert(SliderViewModel viewModel);
        void Update(SliderViewModel Slider);

    }
}