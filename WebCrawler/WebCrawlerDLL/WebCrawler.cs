using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            CrawlResult result = new CrawlResult();
            foreach (string url in urls)
            {
                result[url] = await HandleUrl(url, 0);
            }
            return result;
        }

        private async Task<CrawlResult> HandleUrl(string url, int depth)
        {
            CrawlResult result;
            if (maxDepth <= depth)
            {
                result = null;
            }
            else
            {
                result = new CrawlResult();
                string html = await PageUrlScanner.Instance.DownloadHtmlString(url);
                foreach (string u in PageUrlScanner.Instance.GetUrlsFromHtmlString(html, url))
                {
                    result[u] = await HandleUrl(u, depth + 1);
                }
            }
            return result;
        }
    }
}
