using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using blogfoo.Options;
using blogfoo.Services;
using HeyRed.MarkdownSharp;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using blogfoo.Models.Blogs;
using Newtonsoft.Json;

namespace blogfoo.Controllers
{
    public class HomeController : Controller
    {
        private readonly Navigation _navOptions;
        private readonly IBlogReader _blog;
        private readonly IHostingEnvironment _env;

        public HomeController(IOptions<Navigation> NavOptions,IBlogReader blog,IHostingEnvironment env)
        {
            _navOptions = NavOptions.Value;
            _blog = blog;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            //ViewData["nav1"] = _navOptions.Navs.Where(n => n.ItemTitle == "Item 1").FirstOrDefault().ItemLink;
            try
            {
                var latestTop10Entries = await _blog.ReadTopTenEntryMetadataAsync();
                return View(latestTop10Entries);
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return RedirectToAction("Error");
            }
        }

        public async Task<IActionResult> B(string id)
        {
            try
            {
                var blogEntry = await _blog.ReadEntryMetadataAsync(id);
                var content = await _blog.ReadEntryAsync(id,blogEntry);
                var md = new Markdown();
                blogEntry.content =  md.Transform(content);
                return View(blogEntry);
            }
            catch (Exception)
            {

                Response.StatusCode = 404;
                return RedirectToAction("Error");
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [HttpPost]
        public IActionResult UploadData(string postContent, string postTitle, string postSubtitle, string value)
        {
            if (value != "sam")
            {
                Response.StatusCode = 401;
                return Json(new { message = "Unautrorized"});
            }
            try
            {
                var entry = new Entry() {
                    Author = "Muqeet Khan",
                    EntryDate = DateTime.Now,
                    Title = postTitle,
                    SubTitle = postSubtitle
                };

                var jObjectData = JsonConvert.SerializeObject(entry);

                System.IO.File.WriteAllText(Path.Combine(_env.WebRootPath, "data", "1216", $"{postTitle.Replace(" ", "-")}.json"),jObjectData);
                System.IO.File.WriteAllText(Path.Combine(_env.WebRootPath, "data", "1216", $"{postTitle.Replace(" ", "-")}.md"), postContent);
                return Json(new { message = postContent });
            }
            catch (Exception ex)
            {

                return Json(new { message = ex.Message });
            }
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            ViewData["EMessage"] = "I apologize but I was not able to find that entry.";
            
            return View();
        }
    }
}
