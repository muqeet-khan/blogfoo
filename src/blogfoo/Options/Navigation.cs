using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blogfoo.Options
{
    public class Navigation
    {
        public Navigation()
        {

        }

        public List<NavItems> Navs { get; set; }
    }

    public class NavItems
    {
        public NavItems()
        {

        }

        public string ItemTitle { get; set; }
        public string ItemLink { get; set; }
    }
}
