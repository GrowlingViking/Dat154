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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // Contains the type of staff
        public static string state;

        public MainPage()
        {
            this.InitializeComponent();
            Controller.GetAllTasksFromTheWeb();
        }

        private void Cleaning_Click(object sender, RoutedEventArgs e)
        {
            state = "Cleaning";
            this.Frame.Navigate(typeof(TaskList));
        }

        private void Maintenance_Click(object sender, RoutedEventArgs e)
        {
            state = "Maintenance";
            this.Frame.Navigate(typeof(TaskList));
        }

        private void Service_Click(object sender, RoutedEventArgs e)
        {
            state = "Service";
            this.Frame.Navigate(typeof(TaskList));
        }
    }
}
