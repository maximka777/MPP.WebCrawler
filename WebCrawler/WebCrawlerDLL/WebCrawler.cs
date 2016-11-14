using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerDLL
{
    public class HandleUrlParam
    {
        public string Url { get; set; }
        public int Depth { get; set; }

        public HandleUrlParam(string url, int depth)
        {
            Url = url;
            Depth = depth;
        }
    }

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
            List<Task<CrawlResult>> tasks = new List<Task<CrawlResult>>();
            foreach (string url in urls)
            {
                Task<CrawlResult> task = new Task<CrawlResult>(HandleUrlByTask, new HandleUrlParam(url, 0));
                task.Start();
                tasks.Add(task);
            }
            result = GetResultFromTasks(tasks, new List<string>(urls));
            return result;
        }

        private List<Task<CrawlResult>> PutEachUrlHandlingOnTaskAndGetTaskList(List<string> urls, int depth)
        {
            List<Task<CrawlResult>> tasks = new List<Task<CrawlResult>>();
            Task<CrawlResult> task;
            foreach (string url in urls)
            {
                if (depth > -1)
                {
                    task = new Task<CrawlResult>(HandleUrl,
                        new HandleUrlParam(url, depth + 1));
                }
                else
                {
                    task = new Task<CrawlResult>(HandleUrlByTask,
                        new HandleUrlParam(url, depth + 1));
                }
                task.Start();
                tasks.Add(task);
            }
            return tasks;
        }

        private CrawlResult GetResultFromTasks(List<Task<CrawlResult>> tasks, List<string> urls)
        {
            CrawlResult result = new CrawlResult();
            for (int i = 0; i < urls.Count; i++)
            {
                result[urls[i]] = tasks[i].Result;
            }return result;
        }

        private CrawlResult HandleUrlByTask(object param)
        {
            CrawlResult result;
            HandleUrlParam handleUrlParam = (HandleUrlParam)param;
            if (maxDepth <= handleUrlParam.Depth)
            {
                result = null;
            }
            else
            {
                result = new CrawlResult();
                List<string> urls = PageUrlScanner.Instance.ScanPageOnUrls(handleUrlParam.Url);
                List<Task<CrawlResult>> tasks = PutEachUrlHandlingOnTaskAndGetTaskList(urls, handleUrlParam.Depth);
                result = GetResultFromTasks(tasks, urls);
            }
            return result;
        }

        private CrawlResult HandleUrl(object param)
        {
            CrawlResult result;
            HandleUrlParam handleUrlParam = (HandleUrlParam)param;
            if (maxDepth <= handleUrlParam.Depth)
            {
                result = null;
            }
            else
            {
                result = new CrawlResult();
                foreach (string url in PageUrlScanner.Instance.ScanPageOnUrls(handleUrlParam.Url))
                {
                    result[url] = HandleUrl(new HandleUrlParam(url, handleUrlParam.Depth + 1));
                }
            }
            return result;
        }
    }
}
