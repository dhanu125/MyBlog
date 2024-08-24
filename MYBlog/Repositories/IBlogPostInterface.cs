using MYBlog.Models.Domain;

namespace MYBlog.Repositories
{
    public interface IBlogPostInterface
    {
        Task<IEnumerable<BlogPost>> GetALlBlog();
        Task<BlogPost?> GetByID(Guid id);
        Task<BlogPost?> GetByUrlHandle(string urlHandle);
        Task<BlogPost> AddBlogPost(BlogPost blogPost);
        Task<BlogPost?> UpdateBlogPost(BlogPost blogPost);
        Task<BlogPost?> DeleteBlogPost(Guid id);
    }
}
