using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerDLL
{
    public class CrawlResult
    {
        private Dictionary<string, CrawlResult> dict;

        public CrawlResult()
        {
            dict = new Dictionary<string, CrawlResult>();
        }

        public CrawlResult this[string url]
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

        public List<string> Keys
        {
            get
            {
                return dict.Keys.ToList<string>();
            }
        }

        public string ToString()
        {
            return string.Empty;
        }
    }
}
