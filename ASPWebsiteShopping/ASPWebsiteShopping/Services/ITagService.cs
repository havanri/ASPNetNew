using ASPWebsiteShopping.Models;

namespace ASPWebsiteShopping.Services
{
    public interface ITagService
    {
        void AddTag(Tag tag);
        void DeleteByObj(Tag tag);
        void DeleteRange(List<Tag> tags);
        IEnumerable<Tag> GetAllTags();
        Tag GetTagById(int? id);
        void UpdateTag(Tag tag);

        Tag getTagByName(string tagName);

        bool checkTagReturnBool(string tagName);
    }
}