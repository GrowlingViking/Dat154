using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SpaceObjects
{
    public delegate void Del(object sender);

    public class SpaceObject
    {
        public event Del move;
        public event Del paint;
        public String name { get; set; }
        public double orbital_radius { get; set; }
        public double x_position { get; set; }
        public double y_position { get; set; }
        public double orbital_period { get; set; }
        private double Object_radius;
        public double object_radius
        {
            get { return this.Object_radius; }
            set
            {
                this.Object_radius = value;
                paint?.Invoke(this);
            }
        }
        private Brush Object_color;
        public Brush object_color
        {
            get { return this.Object_color; }
            set
            {
                this.Object_color = value;
                paint?.Invoke(this);
            }
        }

        public SpaceObject(String name)
        {
            this.name = name;
            this.orbital_radius = 0;
            this.x_position = 0;
            this.y_position = 0;
            this.orbital_period = 0;
            this.object_radius = 1390000;
            this.object_color = Brushes.White;
        }

        public SpaceObject(String name, double or_r, double or_p, double ob_r, Brush color)
        {
            this.name = name;
            this.orbital_radius = or_r;
            this.x_position = 0;
            this.y_position = 0;
            this.orbital_period = or_p;
            this.object_radius = ob_r;
            this.object_color = color;
        }
        public double DTR(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public void Move()
        {
            move?.Invoke(this);
        }

        public void Draw()
        {
            paint?.Invoke(this);
        }
    }
}
