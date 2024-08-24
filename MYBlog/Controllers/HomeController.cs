using Microsoft.AspNetCore.Mvc;
using MYBlog.Models;
using MYBlog.Models.ViewModels;
using MYBlog.Repositories;
using System.Diagnostics;

namespace MYBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostInterface _IBlog;
        private readonly ITagInterface _tag;

        public HomeController(ILogger<HomeController> logger,IBlogPostInterface IBlog,ITagInterface Itag)
        {
            _logger = logger;
            _IBlog = IBlog;
            _tag = Itag;
        }

        public async Task<IActionResult> Index()
        {
            var allBlogs = await _IBlog.GetALlBlog();
            var alltags=await _tag.GetAllTags();
           
            var alldata = new BlogpostTag
            {
                blogposts = allBlogs,
                tags = alltags
            };
                return View(alldata);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}