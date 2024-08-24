using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYBlog.Data;
using MYBlog.Models.Domain;
using MYBlog.Models.ViewModels;
using MYBlog.Repositories;
using System.ComponentModel;

namespace MYBlog.Controllers
{
    public class AdminTagsController : Controller
    {
        private  ITagInterface Itag { get; }

        public AdminTagsController(ITagInterface Itag) {
            this.Itag = Itag;
        }
        

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public  async Task<IActionResult> Add(AddTagRequest Adr) 
        {
            Tag tag = new Tag
            {
                Name = Adr.Name,
                DisplayName = Adr.DisplayName
            };
          await Itag.AddTag(tag);

            return RedirectToAction("List");
        }
        public async Task<IActionResult> List()
        {
            var alltags=await Itag.GetAllTags();
            return View(alltags);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id) {
            var existingTag = await Itag.GetTagById(Id);
            
            if (existingTag != null)
            {
                var newtag = new EditTagRequest
                {
                    Id = existingTag.Id,
                    Name = existingTag.Name,
                    DisplayName = existingTag.DisplayName

                };
                return View(newtag);
            }

            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest Edr)
        {
            var tag = new Tag
            {
                Id = Edr.Id,
                Name = Edr.Name,
                DisplayName = Edr.DisplayName
            };

            var returntag=await Itag.UpdateTag(tag);
            if(returntag != null)
            {
                
                return RedirectToAction("List");
            }
            return View(Edr.Id);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest Edr)
        {
            var returntag=await Itag.DeleteTag(Edr.Id);
            
            if (returntag != null)
            {
              
                return RedirectToAction("List");
            }
            return View(Edr.Id);
        }
    }
}
