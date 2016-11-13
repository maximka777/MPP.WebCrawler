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

        private string GenerateTabSequence(int count)
        {
            return new StringBuilder().Append('\t', count).ToString();
        }

        public string ToString(int depth)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in Keys)
            {
                sb.Append(GenerateTabSequence(depth)).Append(key).Append('\n');
                var value = this[key];
                if (value != null) {
                    sb.Append(value.ToString(depth + 1));
                }
            }
            return sb.ToString();
        }

        public string ToString()
        {
            return ToString(0);
        }
    }
}
