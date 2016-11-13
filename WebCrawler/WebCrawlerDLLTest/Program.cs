using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawlerDLL;

namespace WebCrawlerDLLTest
{
    class Program
    {
        static void Main(string[] args)
        {
            WebCrawler crawler = new WebCrawler(2);
            string[] urls = new string[1];
            urls[0] = "https://www.training.by";
            var crawlResult = crawler.PerformCrawling(urls);
            Console.Write(crawlResult.ToString());
            Console.Read();
        }
    }
}
