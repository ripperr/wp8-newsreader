using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataBoundApp1.Http;
using DataBoundApp1.Models;
using DataBoundApp1.Resources;
using System.Collections.Generic;
using Microsoft.Phone.Reactive;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataBoundApp1.ViewModels
{
    public class DetailsViewModel : INotifyPropertyChanged
    {
        private String urlPartOne = "http://api.feedzilla.com/v1/categories/";
        private String urlPartTwo = "/articles.json?count=100";
        public Category Category { get; set; }
        public DetailsViewModel()
        {
            this.NewsItems = new ObservableCollection<Article>();
            
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<Article> NewsItems { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";

        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get { return _sampleProperty; }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        /// <summary>
        /// Sample property that returns a localized string
        /// </summary>
        public string LocalizedSampleProperty
        {
            get { return AppResources.SampleProperty; }
        }

        public bool IsDataLoaded { get; set; }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public async Task LoadData()
        {
            // Sample data; replace with real data
            NewsItems = await LeesNewsItems();

            this.IsDataLoaded = true;

        }

        private async Task<ObservableCollection<Article>> LeesNewsItems()
        {
            string jsonText = await HttpReader.GetHttpResponse(urlPartOne+Category.category_id+urlPartTwo);
            Articles articles = JsonConvert.DeserializeObject<Articles>(jsonText);

            
            
            return new ObservableCollection<Article>(articles.articles);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}