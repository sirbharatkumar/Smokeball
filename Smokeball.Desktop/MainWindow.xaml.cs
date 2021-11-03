using Smokeball.Common.RequestModel;
using Smokeball.Common.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Configuration;
using Smokeball.Desktop.Connector;
using log4net;

namespace Smokeball.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(MainWindow));

        public MainWindow()
        {
            _log.Info("Smokeball Web Search Application started !!");
            InitializeComponent();
            PerformWebSearch();
        }

        /// <summary>
        /// Button Search click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWebSearch_Click(object sender, RoutedEventArgs e)
        {
            _log.Info("Seach button clicked started !!");
            PerformWebSearch();
            _log.Info("Seach button clicked finished !!");
        }

        private void PerformWebSearch()
        {
            try
            {
                _log.Info("Seach text and determine website ranking started !!");
                btnWebSearch.IsEnabled = false;
                lblProcess.Text = "Processing....";
                using (var connector = new HttpConnector())
                {
                    lblResult.Content = "";
                    lblContent.Visibility = Visibility.Hidden;

                    //Get the response from the API call
                    SearchResult searchResult = connector.GetSearchResult(txtSearchText.Text, txtSearchUrlInResult.Text);

                    //Set html snippet by iterating the html list and bind to webbrowser control
                    StringBuilder sb = new StringBuilder();
                    foreach (var result in searchResult.ResultLinks)
                    {
                        sb.AppendLine(result.Html.Replace("<a ", "<p ").Replace("</a>", "</p>"));
                    }
                    wbSearchContent.NavigateToString(sb.ToString());
                    //Set the ranking result to the label
                    lblResult.Content = string.Join(", ", searchResult.Rankings.Select(s => s));
                    _log.InfoFormat("Search Result ranking: {0}", lblResult.Content);

                    lblContent.Text = "DONE";
                }
            }
            catch (Exception ex)
            {
                //Incase of any exception
                wbSearchContent.Navigate("about:blank");
                lblContent.Text = "Something went wrong !!";
            }
            finally
            {
                lblProcess.Text = "";
                lblContent.Visibility = Visibility.Visible;
                btnWebSearch.IsEnabled = true;
                _log.Info("Seach text and determine website ranking finished !!");
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            lblProcess.Text = "";
            lblResult.Content = "";
            lblContent.Text = "";
            wbSearchContent.Navigate("about:blank");
            txtSearchText.Text = "conveyancing software";
            txtSearchUrlInResult.Text = "www.smokeball.com.au";
        }
    }
}
