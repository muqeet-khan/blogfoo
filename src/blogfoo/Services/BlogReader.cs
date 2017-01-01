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

        public Task<string> ReadEntryAsync(string BlogTitle)
        {
            var content = File.ReadAllText(Path.Combine(_env.WebRootPath, "data", "1216", $"{BlogTitle}.md"));
            return Task.Factory.StartNew(() => { return content; });
        }

        public Task<Entry> ReadEntryMetadataAsync(string BlogTitle)
        {
            var provider = File.ReadAllText(Path.Combine(_env.WebRootPath, "data", "1216", $"{BlogTitle}.json"));
            return Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Entry>(provider));
        }
    }
}
