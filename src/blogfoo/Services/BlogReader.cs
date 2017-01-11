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

        public Task<string> ReadEntryAsync(string BlogTitle,Entry BlogMetadata)
        {
            var content = File.ReadAllText(Path.Combine(_env.WebRootPath, "data", BlogMetadata.EntryDate.ToString("yyyy"), BlogMetadata.EntryDate.ToString("MMMM"), $"{BlogTitle}.md"));
            return Task.Factory.StartNew(() => { return content; });
        }

        public Task<Entry> ReadEntryMetadataAsync(string BlogTitle)
        {
            var provider = File.ReadAllText(Path.Combine(_env.WebRootPath, "data", $"{BlogTitle}.json"));
            return Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Entry>(provider));
        }

        public async Task<List<Entry>> ReadTopTenEntryMetadataAsync()
        {
            var allMetadataFiles = new DirectoryInfo(Path.Combine(_env.WebRootPath, "data"));
            var blogEntries = new List<Entry>();
            foreach (var f in allMetadataFiles.GetFiles("*.json").OrderByDescending(x => x.CreationTime).Take(10))
            {
                blogEntries.Add(await ReadEntryMetadataAsync(f.Name.Replace(".json", "")));
            }

            return blogEntries;
        }
    }
}
