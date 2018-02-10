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
        private Moon m;
        int time = 0;

        public event timerDelegate MoveIt;

        public MainWindow()
        {
            InitializeComponent();

            start.Click += startSim;

            Canvas ca = canvas;

            ColumnDefinitionCollection columns = grid.ColumnDefinitions;
            ColumnDefinition column = columns.ElementAt(0);
            double middleX = column.ActualWidth;
    
            s = new Star("Sola", 215, 145);
            s.shape = sola;

            p = new Planet("Planet", s);
            p.shape = planet;
            p.orbital_radius = 100;
            p.orbital_speed = 2;
            MoveIt += p.calcPos;

            m = new Moon("Månen", p);
            m.shape = måne;
            m.orbital_radius = 25;
            m.orbital_speed = 4;
            MoveIt += m.calcPos;
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

            s.xPos = c.ActualWidth / 2;
            s.yPos = c.ActualHeight / 2;
            Canvas.SetLeft(s.shape, s.xPos - 25);
            Canvas.SetTop(s.shape, s.yPos - 25);

            Canvas.SetLeft(p.shape, p.xPos - 12.5);
            Canvas.SetTop(p.shape, p.yPos - 12.5);

            Canvas.SetLeft(m.shape, m.xPos - 5);
            Canvas.SetTop(m.shape, m.yPos - 5);
        }
    }
}
