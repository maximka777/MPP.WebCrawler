using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerDLL
{
    class PageUrlScanner
    {
        private static readonly PageUrlScanner instance = new PageUrlScanner();

        public static PageUrlScanner Instance
        {
            get
            {
                return instance;
            }
        }

        private string GetHtml(string pageUrl)
        {
            string result = string.Empty;
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(pageUrl);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            using (StreamReader streamReader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8))
            {
                result = streamReader.ReadToEnd();
                myResponse.Close();
            }
            return result;
        }

        private List<string> GetUrlsFromHtml(string html, string rootUrl)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            HtmlNodeCollection links = document.DocumentNode.SelectNodes("//a");
            var result = new List<string>();
            foreach (var link in links)
            {
                if (link.Attributes.Contains("href"))
                {
                    var href = link.Attributes["href"].Value;
                    if (href != null)
                    {
                        if (!href.StartsWith("http"))
                        {
                            href = rootUrl + href;
                        }
                        result.Add(href);
                    }
                }
            }
            return result;
        }

        public List<string> ScanPageOnUrls(string pageUrl)
        {
            string html = GetHtml(pageUrl);
            return GetUrlsFromHtml(html, pageUrl);
        }
    }
}
