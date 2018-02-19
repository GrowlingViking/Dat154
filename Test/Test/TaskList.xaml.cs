using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TaskList : Page
    {
        public TaskList()
        {
            this.InitializeComponent();
            Header.Text = MainPage.state;
            GetTaskListAsync();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        HttpClient client = new HttpClient();
        Uri uri = new Uri("TODO");

        private async void GetTaskListAsync()
        {
            string result;
            try
            {
                result = await client.GetStringAsync(uri);
            }
            catch (global::System.Exception)
            {

                throw;
            }
                Tasks.ItemsSource = JsonConvert.DeserializeObject<List<ServiceTask>>(result);
            
        }
        
        
    }

    public class ServiceTask
    {
        public int ID { get; set; }
        public string ServiceType { get; set; }
        public string Status { get; set; }
    }
}
