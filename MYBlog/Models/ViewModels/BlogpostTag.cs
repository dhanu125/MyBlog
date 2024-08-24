
using MYBlog.Models.Domain;

namespace MYBlog.Models.ViewModels
{
    public class BlogpostTag
    {
        public IEnumerable<BlogPost> blogposts { get; set; }
        public IEnumerable<Tag> tags { get; set; }
    }
}
