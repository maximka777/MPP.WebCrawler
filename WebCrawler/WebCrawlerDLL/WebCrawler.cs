using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerDLL
{
    public class WebCrawler : ISimpleWebCrawler
    {
        public int Depth { get; set; }

        public async Task<CrawlResult> PerformCrawlingAsync(string[] urls)
        {
            return await Task.Run(() =>
            {
                return PerformCrawlingAsync(urls);
            });
        }

        private CrawlResult PerformCrawling(string[] urls)
        {
            var result = new CrawlResult();
            foreach(string url in urls)
            {
                result[url] = HandleUrl(url, 0);
            }
        }

        private CrawlResult HandleUrl(string pageUrl, int depth)
        {
            var result = new CrawlResult();
            if (Depth <= depth)
            {
                result = null;
            }
            else
            {
                result = new CrawlResult();
                foreach(string url in PageUrlScanner.ScanPageOnUrls(pageUrl))
                {
                    result[url] = HandleUrl(url, depth + 1);
                }
            }
            return result;
        }
    }
}
