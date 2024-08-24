using Microsoft.EntityFrameworkCore;
using MYBlog.Models.Domain;

namespace MYBlog.Data
{
    public class BlogDBContext:DbContext
    {
        public BlogDBContext(DbContextOptions Options):base(Options)
        {

        }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
