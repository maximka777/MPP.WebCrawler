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
        private readonly WebCrawlerDLL.WebCrawler webCrawler = new WebCrawlerDLL.WebCrawler();
        private string stringCrawlResult;

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

        public event PropertyChangedEventHandler PropertyChanged;

        private async void Crawl()
        {
            CrawlResult crawlResult = await webCrawler.PerformCrawlingAsync(new string[0]);
            StringCrawlResult = "Max";
        }

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
