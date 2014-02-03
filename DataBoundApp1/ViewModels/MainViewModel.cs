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

namespace DataBoundApp1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private String url = "http://api.feedzilla.com/v1/categories.json";

        public MainViewModel()
        {
            this.Categories = new ObservableCollection<Category>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<Category> Categories { get; private set; }

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

        public bool IsDataLoaded { get;  set; }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public async Task LoadData()
        {
            // Sample data; replace with real data
            Categories = await LeesCategories();

            this.IsDataLoaded = true;

        }

        private async Task<ObservableCollection<Category>> LeesCategories()
        {
            string jsonText = await HttpReader.GetHttpResponse(url);


            ObservableCollection<Category> categories =
                JsonConvert.DeserializeObject<ObservableCollection<Category>>(jsonText);
            return new ObservableCollection<Category>(categories.Where(x => x.display_category_name != "").OrderBy(x=>x.display_category_name));

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