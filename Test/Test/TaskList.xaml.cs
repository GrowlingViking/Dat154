using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static ServiceTask ActiveTask { get; set; }
        public ObservableCollection<ServiceTask> TaskSource { get; set; }
        public TaskList()
        {
            this.InitializeComponent();
            Header.Text = MainPage.state;
            HttpClient client = new HttpClient();
            Uri uri = new Uri("http://localhost:50094/api/Task?type=" + MainPage.state);
            GetTaskListAsync(client, uri);
        }

        private async void GetTaskListAsync(HttpClient client, Uri uri)
        {
            string result;
            try
            {
                result = await client.GetStringAsync(uri);
            }
            catch (Exception e)
            {

                throw e;
            }

            TaskSource = JsonConvert.DeserializeObject<ObservableCollection<ServiceTask>>(result);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TaskPage));
        }
    }

    public class ServiceTask
    {
        public int ID { get; set; }
        public string ServiceType { get; set; }
        public string Status { get; set; }
        public int RoomNr { get; set; }
    }
}
