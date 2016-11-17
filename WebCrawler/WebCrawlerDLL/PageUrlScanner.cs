using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Diagnostics;
using System;
using log4net;
using log4net.Config;
using System.Threading.Tasks;
using System.Net.Http;

namespace WebCrawlerDLL
{
    class PageUrlScanner
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PageUrlScanner).Name);
        
        static PageUrlScanner()
        {
            BasicConfigurator.Configure();
        }

        private static readonly PageUrlScanner instance = new PageUrlScanner();

        public static PageUrlScanner Instance
        {
            get
            {
                return instance;
            }
        }

        public List<string> GetUrlsFromHtmlString(string html, string rootUrl)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            HtmlNodeCollection links = document.DocumentNode.SelectNodes("//a");
            List<string> result = new List<string>();
            Uri baseUri = new Uri(rootUrl);
            foreach (var link in links)
            {
                if (link.Attributes.Contains("href"))
                {
                    string href = link.Attributes["href"].Value;
                    {
                        try
                        {
                            result.Add(new Uri(baseUri, href).AbsoluteUri);
                        }
                        catch (Exception exc)
                        {
                            log.Error(exc);
                        }
                    }
                }
            }
            return result;
        }

        public async Task<string> DownloadHtmlString(string url)
        {
            string result = string.Empty;
            HttpClient httpClient = new HttpClient();
            try
            {
                result = await httpClient.GetStringAsync(url);
            }
            catch (HttpRequestException exc)
            {
                log.Error(exc);
            }
            return result;
        }
    }
}
