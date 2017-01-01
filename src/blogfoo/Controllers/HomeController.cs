using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using blogfoo.Options;
using blogfoo.Services;
using HeyRed.MarkdownSharp;

namespace blogfoo.Controllers
{
    public class HomeController : Controller
    {
        private readonly Navigation _navOptions;
        private readonly IBlogReader _blog;

        public HomeController(IOptions<Navigation> NavOptions,IBlogReader blog)
        {
            _navOptions = NavOptions.Value;
            _blog = blog;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["nav1"] = _navOptions.Navs.Where(n => n.ItemTitle == "Item 1").FirstOrDefault().ItemLink;
            var blogEntry = await _blog.ReadEntryMetadataAsync("my-first-blog");
            ViewData["blogtitle"] = blogEntry.Title;
            return View();
        }

        public async Task<IActionResult> B(string id)
        {
            try
            {
                var blogEntry = await _blog.ReadEntryMetadataAsync(id);
                var content = await _blog.ReadEntryAsync(id);
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
