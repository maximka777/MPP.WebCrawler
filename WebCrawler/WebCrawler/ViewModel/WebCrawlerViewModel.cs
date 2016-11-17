using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebCrawlerDLL;

namespace WebCrawler.ViewModel
{
    class WebCrawlerViewModel : INotifyPropertyChanged
    {
        private readonly WebCrawlerDLL.WebCrawler webCrawler;
        private CrawlResult crawlResult;
        private Config config;
        private int progress;
        private int Progress {
            get
            {
                return progress;
            }
            set
            {
                progress = value;
                RaisePropertyChangedEvent(nameof(Progress));
            }
        }

        public WebCrawlerViewModel()
        {
            config = new ConfigurationDataLoader().LoadGonfiguration();
            webCrawler = new WebCrawlerDLL.WebCrawler(config.MaxDepth);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CrawlResult CrawlResult
        {
            get
            {
                return crawlResult;
            }
            set
            {
                crawlResult = value;
                RaisePropertyChangedEvent(nameof(CrawlResult));
            }
        }

        public ICommand CrawlCommand
        {
            get
            {
                return new Command(Crawl);
            }
        }

        private async void Crawl()
        {
            Progress = 0;
            CrawlResult crawlResult = await webCrawler.PerformCrawlingAsync(config.Urls.ToArray());
            CrawlResult = crawlResult;
            Progress = 100;
        }

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
