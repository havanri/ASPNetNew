using ASPWebsiteShopping.Data;
using ASPWebsiteShopping.Models;

namespace ASPWebsiteShopping.Services
{
    public class TagService : ITagService
    {
        private readonly ApplicationDbContext _db;
        public TagService(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Tag> GetAllTags()
        {
            return _db.Tags.ToList();
        }
        public Tag GetTagById(int? id)
        {
            if (id == null || id == 0)
            {
                return null;
            }
            var Tag = _db.Tags.FirstOrDefault(x => x.Id == id);
            return Tag;
        }
        public void DeleteByObj(Tag tag)
        {
            _db.Tags.Remove(tag);
            _db.SaveChanges();
        }
        public void UpdateTag(Tag tag)
        {
            _db.Tags.Update(tag);
            _db.SaveChanges();
        }
        public void AddTag(Tag tag)
        {
            _db.Tags.Add(tag);
            _db.SaveChanges();
        }

        public void DeleteRange(List<Tag> tags)
        {
            _db.Tags.RemoveRange(tags);
            _db.SaveChanges();
        }

        public bool checkDuplicate(string tagName)
        {
            var tagFound = _db.Tags.Where(n=>n.Name== tagName);
            if(tagFound != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Tag getTagByName(string tagName)
        {
            var tag = _db.Tags.FirstOrDefault(x => x.Name == tagName);
            return tag;
        }

        public bool checkTagReturnBool(string tagName)
        {
            var tag = _db.Tags.FirstOrDefault(x => x.Name == tagName);
            if (tag != null)
            {
                return true;
            }
            return false;
        }
    }
}
