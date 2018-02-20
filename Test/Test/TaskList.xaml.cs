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
        private List<ServiceTask> TaskSource;
        public TaskList()
        {
            this.InitializeComponent();
            Header.Text = MainPage.state;
            TaskSource = Controller.GetTasks(MainPage.state);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TaskPage));
        }

        private void Tasks_ItemClick(object sender, ItemClickEventArgs e)
        {
            ServiceTask activeTask = (ServiceTask)e.ClickedItem;
            
            Controller.SetActiveTasks(activeTask);

            this.Frame.Navigate(typeof(TaskPage));
        }
    }
}
