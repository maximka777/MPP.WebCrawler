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
        private string stringCrawlResult;
        private Config config;

        public WebCrawlerViewModel()
        {
            config = new ConfigurationDataLoader().LoadGonfiguration();
            webCrawler = new WebCrawlerDLL.WebCrawler(config.MaxDepth);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string StringCrawlResult
        {
            get
            {
                return stringCrawlResult;
            }
            set
            {
                stringCrawlResult = value;
                RaisePropertyChangedEvent(nameof(StringCrawlResult));
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
            CrawlResult crawlResult = await webCrawler.PerformCrawlingAsync(config.Urls.ToArray());
            StringCrawlResult = crawlResult.ToString();
        }

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
