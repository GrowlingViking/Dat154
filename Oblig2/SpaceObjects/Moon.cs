using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SpaceObjects
{
    public class Moon : SpaceObject
    {
        public Moon(String name, double or_r, double or_p, double ob_r, Brush color)
            : base(name, or_r, or_p, ob_r, color)
        {
        }
        public void set_position(double time, double planet_x, double planet_y, Func<double, double> ds, Func<double, double> rs, double offset)
        {
            x_position = Math.Cos(DTR(((time % orbital_period) / orbital_period) * 360.0)) * (ds(orbital_radius) + offset + rs(object_radius));
            y_position = Math.Sin(DTR(((time % orbital_period) / orbital_period) * 360.0)) * (ds(orbital_radius) + offset + rs(object_radius));
            x_position += planet_x;
            y_position += planet_y;
            Move();
        }
    }
}
