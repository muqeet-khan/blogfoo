using blogfoo.Models.Blogs;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace blogfoo.Services
{
    public class BlogReader : IBlogReader
    {
        private readonly IHostingEnvironment _env;

        public BlogReader(IHostingEnvironment env)
        {
            _env = env;
        }
        public Task<Entry> ReadBlogEntryAsync(string BlogTitle)
        {
            var provider = File.ReadAllText(Path.Combine(_env.WebRootPath, "data", "1216", "my-first-blog.json"));
            return Task.Factory.StartNew(()=> JsonConvert.DeserializeObject<Entry>(provider));
        }
    }
}
