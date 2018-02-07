using System;
using System.Collections.Generic;
using System.Linq;
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
using SpaceSim;

namespace SolarSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void delg(int time);

        private System.Windows.Threading.DispatcherTimer t;
        private Planet p;
        private Star s;
        int time = 0;
        public event delg MoveIt;

        public MainWindow()
        {
            InitializeComponent();

            t = new System.Windows.Threading.DispatcherTimer();
            t.Interval = new TimeSpan(200000);
            t.Tick += t_Tick;
            t.Start();

            s = new Star("Sola");
            s.shape = sola;

            p = new Planet("Planet", )
            
        }

        void t_Tick(object sender, EventArgs e)
        {
            MoveIt(time++);
        }
    }
}
