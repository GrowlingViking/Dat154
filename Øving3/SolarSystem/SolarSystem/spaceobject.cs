using System;
using System.Windows.Shapes;

namespace SpaceSim {
	public class SpaceObject {
		
		protected String name;

        public double xPos { get; set; }
        public double yPos { get; set; }

        public int orbital_radius { get; set; }
        public int orbital_period { get; set; }
        public int orbital_speed { get; set; }

        public Ellipse shape { get; set; }
		
		public SpaceObject(String name) {
			this.name = name;
		}
		public virtual void Draw() {
			Console.WriteLine(name);
		}
	}
	
	public class Star : SpaceObject {
		public Star(String name, double xpos, double ypos) : base(name)
        {
            this.xPos = xpos;
            this.yPos = ypos;
        }
		public override void Draw() {
			Console.Write("Star : ");
			base.Draw();
		}
	}
	
	public class Planet : SpaceObject {

        public Star sol { get; set; }

        public Planet(String name, Star sol) : base(name) {
            this.sol = sol;
        }

		public override void Draw() {
			Console.Write("Planet: ");
			base.Draw();
		}
        public void calcPos(int time)
        {
            xPos = sol.xPos + (int)(Math.Cos(time * orbital_speed * 3.1416 / 180) * orbital_radius);
            yPos = sol.yPos + (int)(Math.Sin(time * orbital_speed * 3.1416 / 180) * orbital_radius);
        }
	}

    public class Moon : SpaceObject {

        public Planet planet { get; set; }

        public Moon(String name, Planet planet) : base(name) {
            this.planet = planet;
        }

        public override void Draw() {
            Console.Write("Moon: ");
            base.Draw();
        }

        public void calcPos(int time) {
            xPos = planet.xPos + (int)(Math.Cos(time * orbital_speed * 3.1416 / 180) * orbital_radius);
            yPos = planet.yPos + (int)(Math.Sin(time * orbital_speed * 3.1416 / 180) * orbital_radius);
        }
    }
}