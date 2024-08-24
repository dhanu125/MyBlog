using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MYBlog.Repositories;
using System.Net;

namespace MYBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageInterface imageInterface;
        public ImagesController(IImageInterface IImage)
        {
            this.imageInterface = IImage;
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            //call a repository
           var imagurl= await imageInterface.UploadAsync(file);
            if(imagurl == null)
            {
                return Problem("Somthing went wrong",null,(int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new {link=imagurl});
        }
    }
}
