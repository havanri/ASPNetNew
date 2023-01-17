using ASPWebsiteShopping.Data;
using ASPWebsiteShopping.Models;

namespace ASPWebsiteShopping.Services
{
    public class SliderService : ISliderService
    {
        private readonly ApplicationDbContext _db;
        public SliderService(ApplicationDbContext db)
        {
            _db = db;
        }

        public void DeleteById(Slider slider)
        {
            _db.Sliders.Remove(slider);
            _db.SaveChanges();
        }
        

        public Slider Get(int id)
        {
            var rs =_db.Sliders.Where(e=>e.Id == id).Select(e=>new Slider
            {
                Id = e.Id,
                Name = e.Name,
                ImagePath = e.ImagePath,
                Description = e.Description,
            }).FirstOrDefault();
            return rs;
        }

        public List<Slider> GetList()
        {
            var rs = _db.Sliders.Select(e => new Slider
            {
                Id = e.Id,
                Name = e.Name,
                ImagePath = e.ImagePath,
                Description = e.Description
            }).ToList();
            return rs;
        }

        public Slider GetSliderById(int? id)
        {
            if (id == null || id == 0)
            {
                return null;
            }
            var slider = _db.Sliders.FirstOrDefault(e => e.Id == id);
            return slider;
        }

     

        public void Insert(SliderViewModel viewModel)
        {
            var slider = new Slider
            {
                Name = viewModel.Slider.Name,
                Description = viewModel.Slider.Description,
                ImagePath = viewModel.Slider.ImagePath,

            };
            _db.Sliders.Add(slider);
            _db.SaveChanges();
        }

        public void Update(SliderViewModel viewModel)
        {
            var slider = _db.Sliders.Where(e => e.Id == viewModel.Slider.Id).FirstOrDefault();
            if (viewModel.Slider.ImagePath != null) //co anh
            {
                slider.ImagePath = viewModel.Slider.ImagePath;
            }
            slider.Name = viewModel.Slider.Name;
            slider.Description = viewModel.Slider.Description;
            _db.Sliders.Update(slider);
            _db.SaveChanges();
        }
    }
}
