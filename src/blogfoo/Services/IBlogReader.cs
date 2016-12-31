using blogfoo.Models.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blogfoo.Services
{
    public interface IBlogReader
    {
        Task<Entry> ReadBlogEntryAsync(string BlogTitle);
    }
}
