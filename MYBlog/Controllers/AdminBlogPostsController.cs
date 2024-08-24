using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MYBlog.Models.Domain;
using MYBlog.Models.ViewModels;
using MYBlog.Repositories;

namespace MYBlog.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagInterface Itag;
        private readonly IBlogPostInterface IBlogPost;
        public AdminBlogPostsController(ITagInterface Itag,IBlogPostInterface IBlogPost)
        {
            this.Itag = Itag;  
            this.IBlogPost = IBlogPost;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags=await Itag.GetAllTags();
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest Blogpost)
        {
            var blogpost = new BlogPost
            {
                Heading = Blogpost.Heading,
                PageTitle = Blogpost.PageTitle,
                Content = Blogpost.Content,
                ShortDescription = Blogpost.ShortDescription,
                FeaturedImageURL = Blogpost.FeaturedImageURL,
                URLHandle = Blogpost.URLHandle,
                PublishedDate = Blogpost.PublishedDate,
                Author = Blogpost.Author,
                Visible = Blogpost.Visible
            };
            var selectedtags = new List<Tag>();
            foreach(var tag in Blogpost.SelectedTags) {
                var selectedTagIdAsGuid = Guid.Parse(tag);
                var existingtag=await Itag.GetTagById(selectedTagIdAsGuid);
                if(existingtag != null)
                {
                    selectedtags.Add(existingtag);
                }

            }
            blogpost.Tags = selectedtags;
           await  IBlogPost.AddBlogPost(blogpost);
            return RedirectToAction("Add");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
           var allBlogPosts= await IBlogPost.GetALlBlog();
            return View(allBlogPosts);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var EditData = await IBlogPost.GetByID(Id);
            var tags=await Itag.GetAllTags();
            var model = new EditBlogPostRequest
            {
                Id = EditData.Id,
                Heading = EditData.Heading,
                PageTitle = EditData.PageTitle,
                Content = EditData.Content,
                ShortDescription = EditData.ShortDescription,
                FeaturedImageURL = EditData.FeaturedImageURL,
                URLHandle = EditData.URLHandle,
                PublishedDate = EditData.PublishedDate,
                Author = EditData.Author,
                Visible = EditData.Visible,
                Tags = tags.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                SelectedTags=EditData.Tags.Select(x=>x.Id.ToString()).ToArray()

            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest EditedBlog)
        {
            var blogpost = new BlogPost
            {
                Id=EditedBlog.Id,
                Heading = EditedBlog.Heading,
                PageTitle = EditedBlog.PageTitle,
                Content = EditedBlog.Content,
                ShortDescription = EditedBlog.ShortDescription,
                FeaturedImageURL = EditedBlog.FeaturedImageURL,
                URLHandle = EditedBlog.URLHandle,
                PublishedDate = EditedBlog.PublishedDate,
                Author = EditedBlog.Author,
                Visible = EditedBlog.Visible
            };
            var selectedtags = new List<Tag>();
            foreach (var tag in EditedBlog.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(tag);
                var existingtag = await Itag.GetTagById(selectedTagIdAsGuid);
                if (existingtag != null)
                {
                    selectedtags.Add(existingtag);
                }

            }
            blogpost.Tags = selectedtags;
            await IBlogPost.UpdateBlogPost(blogpost);
            
            return RedirectToAction("List");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await IBlogPost.DeleteBlogPost(id);
            return RedirectToAction("List");
        }
    }
}
