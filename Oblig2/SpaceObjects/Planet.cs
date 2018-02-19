using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpaceObjects
{
    public class Planet : SpaceObject
    {
        public List<Moon> moons { get; set; }
        public Planet(String name, double or_r, double or_p, double ob_r, Brush color, List<Moon> moons)
            : base(name, or_r, or_p, ob_r, color)
        {
            this.moons = moons;
        }

        public void load(String path)
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(path + "planets.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] words = line.Split(' ').Where(c => c != "").ToArray();
                if (words.Length >= 6 && words[2] == name)
                {
                    double or_r, or_p, ob_r;
                    if (double.TryParse(words[3], out or_r) && double.TryParse(words[4], out or_p)
                        && double.TryParse(words[5], out ob_r))
                    {
                        Moon m = new Moon(words[0], or_r, or_p, ob_r, new ImageBrush(new BitmapImage(
                            new Uri(path + "Moon.jpg", UriKind.Relative))));
                        moons.Add(m);
                    }
                }
            }
            file.Close();
        }

        public void set_position(double time, Func<double, double> ds, Func<double, double> rs, double offset, double last, bool moon)
        {
            x_position = Math.Cos(DTR(((time % orbital_period) / orbital_period) * 360.0)) * (ds(orbital_radius-last) + offset + rs(object_radius));
            y_position = Math.Sin(DTR(((time % orbital_period) / orbital_period) * 360.0)) * (ds(orbital_radius-last) + offset + rs(object_radius));
            if (moon)
            {
                double pOffset = rs(object_radius);
                foreach (Moon m in moons)
                {
                    m.set_position(time, x_position, y_position, ds, rs, pOffset);
                    pOffset += rs(m.object_radius) * 2;
                }
            }
            Move();
        }
    }
}
