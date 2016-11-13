using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Diagnostics;
using System;

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
            try {
                WebResponse myResponse = myRequest.GetResponse();
                using (StreamReader streamReader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8))
                {
                    result = streamReader.ReadToEnd();
                    myResponse.Close();
                }
            }
            catch(WebException exc)
            {

            }
            return result;
        }

        private List<string> GetUrlsFromHtml(string html, string rootUrl)
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
                    if (href != null)
                    {
                        result.Add(new Uri(baseUri, href).AbsoluteUri);
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
