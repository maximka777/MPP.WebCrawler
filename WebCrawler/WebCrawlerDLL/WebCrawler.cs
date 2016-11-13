using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerDLL
{
    class WebCrawler : ISimpleWebCrawler
    {
        public async Task<CrawlResult> PerformCrawlingAsync(string[] urls)
        {
            return await Task.Run(() =>
            {
                return PerformCrawlingAsync(urls);
            });
        }

        private CrawlResult PerformCrawling(string[] urls)
        {
            return new CrawlResult();
        }
    }
}
