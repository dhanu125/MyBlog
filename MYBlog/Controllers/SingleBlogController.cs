using Microsoft.AspNetCore.Mvc;
using MYBlog.Repositories;

namespace MYBlog.Controllers
{
    public class SingleBlogController : Controller
    {


        private readonly IBlogPostInterface _IBlog;
        public SingleBlogController(IBlogPostInterface Iblog)
        {
                this._IBlog = Iblog;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogpost=await _IBlog.GetByUrlHandle(urlHandle);
            return View(blogpost);
        }
    }
}
