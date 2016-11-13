using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.Model;

namespace WebCrawler.ViewModel
{
    class WebCrawlerViewModel : INotifyPropertyChanged
    {
        private readonly WebCrawlerModel webCrawlerModel = new WebCrawlerModel();

        

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
