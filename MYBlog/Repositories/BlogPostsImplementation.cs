using Microsoft.EntityFrameworkCore;
using MYBlog.Data;
using MYBlog.Models.Domain;

namespace MYBlog.Repositories
{
    public class BlogPostsImplementation : IBlogPostInterface
    {
        private readonly BlogDBContext _dbContext;
        public BlogPostsImplementation(BlogDBContext _dbcontext)
        {
            this._dbContext = _dbcontext;
        }
        public async Task<BlogPost> AddBlogPost(BlogPost blogPost)
        {
            await _dbContext.BlogPosts.AddAsync(blogPost);
           await _dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteBlogPost(Guid id)
        {
            var existingTag = await _dbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
            if(existingTag != null)
            {
                _dbContext.Remove(existingTag);
                await _dbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetALlBlog()
        {
           return await _dbContext.BlogPosts.Include(x=>x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetByID(Guid id)
        {
            return await _dbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<BlogPost?> GetByUrlHandle(string urlHandle)
        {
            return await _dbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x=>x.URLHandle==urlHandle);
        }

        public async Task<BlogPost?> UpdateBlogPost(BlogPost blogPost)
        {
            var existingTag = await _dbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if (existingTag != null)
            {
                existingTag.Heading = blogPost.Heading;
                existingTag.PageTitle = blogPost.PageTitle;
                existingTag.Content = blogPost.Content;
                existingTag.ShortDescription = blogPost.ShortDescription;
                existingTag.FeaturedImageURL = blogPost.FeaturedImageURL;
                existingTag.URLHandle = blogPost.URLHandle;
                existingTag.PublishedDate = blogPost.PublishedDate;
                existingTag.Author = blogPost.Author;
                existingTag.Visible = blogPost.Visible;
                existingTag.Tags = blogPost.Tags;
                await _dbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
    }
}
