using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerDLL
{
    class CrawlResult
    {
        private Dictionary<string, CrawlResult> dict;

        CrawlResult this[string url]
        {
            get
            {
                return dict[url];
            }
            set
            {
                dict[url] = value;
            }
        }

        List<string> Keys
        {
            get
            {
                return dict.Keys.ToList<string>();
            }
        }
    }
}
