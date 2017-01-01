using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;

namespace blogfoo.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("code")]
    public class CodeTagHelper : TagHelper
    {
        private readonly ILogger<CodeTagHelper> _logger;

        public CodeTagHelper(ILogger<CodeTagHelper> logger)
        {
            _logger = logger;
        }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var tagContent = await output.GetChildContentAsync();
            var content = tagContent.GetContent();
            _logger.LogDebug(content);
        }
    }
}
