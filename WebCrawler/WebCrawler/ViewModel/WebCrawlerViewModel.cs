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
    class Command : ICommand
    {
        private readonly Action action;

        #pragma warning disable 67
        public event EventHandler CanExecuteChanged;
        #pragma warning restore 67

        public Command(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action();
        }
    }

    class WebCrawlerViewModel : INotifyPropertyChanged
    {
        private readonly WebCrawlerDLL.WebCrawler webCrawler = new WebCrawlerDLL.WebCrawler(2);
        private string stringCrawlResult;

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
                RaisePropertyChangedEvent("StringCrawlResult");
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
            string[] urls = new string[1];
            urls[0] = "https://www.training.by";
            CrawlResult crawlResult = await webCrawler.PerformCrawlingAsync(urls);
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
