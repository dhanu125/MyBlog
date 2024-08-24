using MYBlog.Models.Domain;

namespace MYBlog.Repositories
{
    public interface ITagInterface
    {
       Task<IEnumerable<Tag>> GetAllTags();
        Task<Tag?> GetTagById(Guid id);
        Task<Tag> AddTag(Tag tag);
        Task<Tag?> UpdateTag(Tag tag);
        Task<Tag?> DeleteTag(Guid id);
    }
}
