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

        public double starXPos { get; set; }
        public double starYPos { get; set; }

        public Planet(String name, double starXPos, double starYPos) : base(name)
        {
            this.starXPos = starXPos;
            this.starYPos = starYPos;
        }

		public override void Draw() {
			Console.Write("Planet: ");
			base.Draw();
		}
        public void calcPos(int time)
        {
            xPos = starXPos + (int)(Math.Cos(time * orbital_speed * 3.1416 / 180) * orbital_radius);
            yPos = starYPos + (int)(Math.Sin(time * orbital_speed * 3.1416 / 180) * orbital_radius);
        }
	}
}