using Azure;
using Microsoft.EntityFrameworkCore;
using MYBlog.Data;
using MYBlog.Models.Domain;

namespace MYBlog.Repositories
{
    public class TagImplementation : ITagInterface
    {
        private BlogDBContext _dbcontextvar { get; }
        public TagImplementation(BlogDBContext dbcontextvar)
        {
            _dbcontextvar = dbcontextvar;
        }
        async Task<Tag> ITagInterface.AddTag(Tag tag)
        {
            await _dbcontextvar.Tags.AddAsync(tag);
            await _dbcontextvar.SaveChangesAsync();
            return tag;

        }

       async Task<Tag?> ITagInterface.DeleteTag(Guid id)
        {
            var existingTag = await _dbcontextvar.Tags.FindAsync(id);
            if(existingTag!=null)
            {
                _dbcontextvar.Tags.Remove(existingTag);
                await _dbcontextvar.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

         async Task<IEnumerable<Tag>> ITagInterface.GetAllTags()
        {
            return await _dbcontextvar.Tags.ToListAsync();
        }

        async Task<Tag?> ITagInterface.GetTagById(Guid id)
        {
            return await _dbcontextvar.Tags.FindAsync(id);
        } 

        async Task<Tag?> ITagInterface.UpdateTag(Tag tag)
        {
            var existingTag = await _dbcontextvar.Tags.FindAsync(tag.Id);

            if(existingTag != null) {
                existingTag.Name = tag.Name;
                existingTag.DisplayName= tag.DisplayName;
               await _dbcontextvar.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
    }
}
