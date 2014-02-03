using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DataBoundApp1.Models;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DataBoundApp1.Resources;
using Microsoft.Phone.Tasks;

namespace DataBoundApp1
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        private String urlPartOne = "http://api.feedzilla.com/v1/categories/";
        private String urlPartTwo = "/articles.json?count=100";
        // Constructor
        public DetailsPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // When page is navigated to set data context to selected item in list
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext == null)
            {
                string selectedIndex = "";
                if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                {
                    int index = int.Parse(selectedIndex);

                    App.DetailsViewModel.Category = App.MainViewModel.Categories.Single(x => x.category_id == index);
                    
                     if (!App.DetailsViewModel.IsDataLoaded)
                     {
                         SystemTray.ProgressIndicator = new ProgressIndicator();

                         SetProgressIndicator(true);
                         SystemTray.ProgressIndicator.Text = "Loading data...";
                         await App.DetailsViewModel.LoadData();

                     }
                     SetProgressIndicator(false);
                    TitlePanel.DataContext = App.DetailsViewModel.Category;
                    
                    DetailLongListSelector.ItemsSource = App.DetailsViewModel.NewsItems;
                }
            }
        }
        private static void SetProgressIndicator(bool isVisible)
        {
            SystemTray.ProgressIndicator.IsIndeterminate = isVisible;
            SystemTray.ProgressIndicator.IsVisible = isVisible;
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
        private void DetailLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WebBrowserTask webBrowserTask = new WebBrowserTask();

            webBrowserTask.Uri = new Uri((DetailLongListSelector.SelectedItem as Article).url, UriKind.Absolute);

            webBrowserTask.Show();
        }
    }
}