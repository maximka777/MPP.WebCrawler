using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerDLL
{
    public class WebCrawler : ISimpleWebCrawler
    {
        public int maxDepth;

        public WebCrawler(int maxDepth)
        {
            this.maxDepth = maxDepth;
        }

        public async Task<CrawlResult> PerformCrawlingAsync(string[] urls)
        {
            return await Task.Run(() =>
            {
                return PerformCrawling(urls);
            });
        }

        public CrawlResult PerformCrawling(string[] urls)
        {
            var result = new CrawlResult();
            foreach(string url in urls)
            {
                result[url] = HandleUrl(url, 0);
            }
            return result;
        }

        private CrawlResult HandleUrl(string pageUrl, int depth)
        {
            CrawlResult result;
            if (maxDepth <= depth)
            {
                result = null;
            }
            else
            {
                result = new CrawlResult();
                foreach(string url in PageUrlScanner.Instance.ScanPageOnUrls(pageUrl))
                {
                    result[url] = HandleUrl(url, depth + 1);
                }
            }
            return result;
        }
    }
}
