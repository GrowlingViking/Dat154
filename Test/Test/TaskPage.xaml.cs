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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TaskPage : Page
    {
        private ServiceTask activeTask;
        private List<String> notes;
        public TaskPage()
        {
            this.InitializeComponent();
            activeTask = Controller.GetActiveTask();
            Header.Text = "Room nr: " + activeTask.RoomNr;
            SetStatusOnButtons(activeTask.Status);
            Notes.ItemsSource = activeTask.Notes;
        }

        private void ChangeStatus(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            activeTask.Status = (String) rb.Tag;
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TaskList));
        }

        private void SetStatusOnButtons(string status)
        {
            switch (status)
            {
                case "In Progress":
                    InProgress.IsChecked = true;
                    break;
                case "Finished":
                    Finished.IsChecked = true;
                    break;
                default:
                    New.IsChecked = true;
                    break;
            }
        }
    }
}
