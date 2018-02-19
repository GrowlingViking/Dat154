using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpaceObjects
{
    public class Star : SpaceObject
    {
        public List<Planet> planets { get; set; }
        public Star(String name, List<Planet> planets) : base(name)
        {
            this.planets = planets;
        }
        public void set_position(double time, Func<double, double> ds, Func<double,double> rs, SpaceObject active)
        {
            double offset = rs(object_radius);
            double last = 0;
            foreach (Planet p in planets)
            {
                if (active == p)
                {
                    foreach (Moon m in p.moons)
                        offset += rs(m.object_radius) * 2;
                    p.set_position(time, ds, rs, offset, last, true);
                    foreach (Moon m in p.moons)
                        offset += rs(m.object_radius) * 2;
                }
                else
                    p.set_position(time, ds, rs, offset, last, false);
                offset += rs(p.object_radius) * 2 + ds(p.orbital_radius - last);
                last = p.orbital_radius;
            }
            Move();
        }
        public void signalDraw(SpaceObject active)
        {
            Draw();
            foreach (Planet p in planets)
            {
                if (active == p)
                    foreach (Moon m in p.moons)
                        m.Draw();
                p.Draw();
            }
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
                        Planet p = new Planet(words[0], or_r, or_p, ob_r, new ImageBrush(new BitmapImage(
                            new Uri(path + words[0] + ".jpg", UriKind.Relative))),
                            new List<Moon>());
                        planets.Add(p);
                        p.load(path);
                    }
                }
            }
            file.Close();
        }
    }
}
