using System;

namespace SpaceSim {
	public class SpaceObject {
		
		protected String name;

        public int xPos { get; set; }
        public int yPos { get; set; }

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
		public Star(String name) : base(name) {}
		public override void Draw() {
			Console.Write("Star : ");
			base.Draw();
		}
	}
	
	public class Planet : SpaceObject {

        public int starXPos { get; set; }
        public int starYPos { get; set; }

        public Planet(String name, int starXPos, int starYPos) : base(name)
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
            xPos = xPosStar + orbital_radius + (int)(Math.Cos(time * orbital_speed * 3.1416 / 180) * orbital_radius);
            yPos = yPosStar + orbital_radius + (int)(Math.Cos(time * orbital_speed * 3.1416 / 180) * orbital_radius);

            Canvas c = (Canvas)shape.Parent;
            c.SetLeft(shape, xPos);
            c.SetTop(shape, yPos);
        }
	}
}