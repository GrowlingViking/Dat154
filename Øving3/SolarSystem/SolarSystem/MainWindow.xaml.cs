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
        public delegate void timerDelegate(int time);
        public delegate void startDelegate();

        private System.Windows.Threading.DispatcherTimer t;
        private Planet p;
        private Star s;
        int time = 0;

        public event timerDelegate MoveIt;

        public MainWindow()
        {
            InitializeComponent();

            start.Click += startSim;

            s = new Star("Sola", 200, 150);
            s.shape = sola;

            p = new Planet("Planet", s.xPos, s.yPos);
            p.shape = planet;
            p.orbital_radius = 100;
            p.orbital_speed = 2;
            MoveIt += p.calcPos;

        }

        void startSim(object sender, EventArgs e)
        {
            t = new System.Windows.Threading.DispatcherTimer();
            t.Interval = new TimeSpan(200000);
            t.Tick += t_Tick;
            t.Start();

            
        }

        void t_Tick(object sender, EventArgs e)
        {
            MoveIt(time++);

            drawPlanets();
        }

        void drawPlanets()
        {
            Canvas c = canvas;
            Canvas.SetLeft(s.shape, s.xPos);
            Canvas.SetTop(s.shape, s.yPos);

            Canvas.SetLeft(p.shape, p.xPos);
            Canvas.SetTop(p.shape, p.yPos);
        }
    }
}
