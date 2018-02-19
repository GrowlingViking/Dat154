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
using SpaceObjects;

namespace Oblig2
{
    public partial class MainWindow : Window
    {
        public SpaceObject active;
        public Star sun;
        public double t = 0;
        public double speed = 0.001;
        public bool show_orbits = false;
        public Func<double, double> dsf = (a) => { return Math.Log(a / 10000 > 2 ? a / 10000 : 2, 2) * ds; };
        public Func<double, double> rsf = (a) => { return Math.Log(a / 100 > 2 ? a / 100 : 2, 2) * rs; };
        public static double ds = 0.5;
        public static double rs = 0.5;
        public Dictionary<String, Ellipse> ellipse = new Dictionary<string, Ellipse>();
        public Dictionary<String, Ellipse> orbits = new Dictionary<string, Ellipse>();
        public System.Windows.Threading.DispatcherTimer dispatcherTimer;

        public MainWindow()
        {
            InitializeComponent();
            sun = new Star("Sun", new List<Planet>());
            active = sun;
            sun.load(@"C:\Users\Kenneth\planets\");
            connectSpaceObject(sun,true);
            foreach (Planet p in sun.planets)
            {
                connectSpaceObject(p,true);
                foreach (Moon m in p.moons)
                    connectSpaceObject(m, false);
            }
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            uppdateInfo();
        }

        private void connectSpaceObject(SpaceObject sp, bool visible)
        {
            sp.move += updateSpaceObjectPos;
            sp.paint += updateSpaceObject;
            Ellipse el = new Ellipse();
            Ellipse orbit = new Ellipse();
            orbit.Stroke = Brushes.White;
            orbit.Visibility = Visibility.Hidden;
            myCanvas.Children.Add(orbit);
            myCanvas.Children.Add(el);
            ellipse.Add(sp.name, el);
            orbits.Add(sp.name, orbit);
            el.Name = sp.name;
            el.MouseLeftButtonDown += El_MouseLeftButtonDown;
            if (!visible)
            {
                el.Visibility = Visibility.Hidden;
                orbit.Visibility = Visibility.Hidden;
            }
            else
                sp.Draw();
        }

        private void El_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse el = (Ellipse)sender;
            SpaceObject newActive = sun.planets.Find(x => x.name == el.Name);
            if (el.Name != "Sun" && newActive != null)
            {
                if (active is Planet)
                    foreach (Moon m in ((Planet)active).moons)
                    {
                        ellipse[m.name].Visibility = Visibility.Hidden;
                        orbits[m.name].Visibility = Visibility.Hidden;
                    }
                if (newActive is Planet)
                    foreach (Moon m in ((Planet)newActive).moons)
                    {
                        ellipse[m.name].Visibility = Visibility.Visible;
                        if (show_orbits)
                            orbits[m.name].Visibility = Visibility.Visible;
                        m.Draw();
                    }
                active = newActive;
            }
            if (el.Name == "Sun")
            {
                if (active is Planet)
                    foreach (Moon m in ((Planet)active).moons)
                    {
                        ellipse[m.name].Visibility = Visibility.Hidden;
                        orbits[m.name].Visibility = Visibility.Hidden;
                    }
                active = sun;

            }
            sun.set_position(t, dsf, rsf, active);
            sun.signalDraw(active);
        }

        private void uppdateInfo()
        {
            Namel.Content = "Name: " + active.name;
            Timel.Content = "Time: " + t.ToString("0.0" + " days");
            Sizel.Content = "Size: " + active.object_radius + "km";
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            t += speed;
            uppdateInfo();
            sun.set_position(t, dsf, rsf, active);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            t = 0;
            uppdateInfo();
            sun.set_position(t, dsf, rsf, active);
        }

        public void updateSpaceObjectPos(object sender)
        {
            SpaceObject sp = (SpaceObject)sender;
            Ellipse el = ellipse[sp.name];
            Ellipse orbit = orbits[sp.name];
            double w = Space.ActualWidth - 100;
            double h = Space.ActualHeight;
            Thickness margin = el.Margin;
            margin.Left = (w / 2) + (sp.x_position-active.x_position) - rsf(sp.object_radius);
            margin.Top = (h / 2) + (sp.y_position-active.y_position) - rsf(sp.object_radius);
            orbit.Height = Math.Sqrt(Math.Pow(sp.x_position,2) + Math.Pow(sp.y_position,2))*2;
            orbit.Width = orbit.Height;
            if (sp is Moon)
            {
                orbit.Height = Math.Sqrt(Math.Pow(sp.x_position-active.x_position, 2) + Math.Pow(sp.y_position-active.y_position, 2)) * 2;
                orbit.Width = orbit.Height;
                orbit.Margin = new Thickness((w / 2) - orbit.Height / 2, (h / 2) - orbit.Height / 2, 0, 0);
            }
            else
                orbit.Margin = new Thickness((w / 2) - orbit.Height / 2 - active.x_position, (h / 2) - orbit.Height / 2 - active.y_position, 0, 0);
            el.Margin = margin;
        }

        public void updateSpaceObject(object sender)
        {
            SpaceObject sp = (SpaceObject)sender;
            Ellipse el = ellipse[sp.name];
            el.Height = rsf(sp.object_radius) * 2;
            el.Width = rsf(sp.object_radius) * 2;
            el.Fill = sp.object_color;
            updateSpaceObjectPos(sender);
        }

        private void size_change(object sender, SizeChangedEventArgs e)
        {
            sun.set_position(t, dsf, rsf, active);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ds = e.NewValue + 0.5;
            sun.set_position(t, dsf, rsf, active);
        }

        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            rs = e.NewValue + 0.5;
            sun.set_position(t, dsf, rsf, active);
            sun.signalDraw(active);
        }

        private void Slider_ValueChanged_2(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            speed = (e.NewValue/10) + 0.001;
            sun.set_position(t, dsf, rsf, active);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (Planet p in sun.planets)
                orbits[p.name].Visibility = Visibility.Visible;
            if (active is Planet)
                foreach (Moon m in ((Planet)active).moons)
                    orbits[m.name].Visibility = Visibility.Visible;
            show_orbits = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (Planet p in sun.planets)
                orbits[p.name].Visibility = Visibility.Hidden;
            if (active is Planet)
                foreach (Moon m in ((Planet)active).moons)
                    orbits[m.name].Visibility = Visibility.Hidden;
            show_orbits = false;
        }
    }
}
