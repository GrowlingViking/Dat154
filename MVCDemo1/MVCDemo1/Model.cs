using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;


namespace MVCDemo1
{
    class Model
    {
        static public int value;
        static public List<int> a = new List<int>();

        public Model()
        {
            Timer timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick); // Everytime timer ticks, timer_Tick will be called
            timer.Interval = 10000;
            timer.Start(); // Simulate extern update
        }

        static public void Update(int updateV) {
            value = updateV;
            a.Add(value);
            Controller.Notify();
        }
        static public int GetValue() { return value; }


        void timer_Tick(object sender, EventArgs e)
        {
            Random r = new Random();
            Update(value + r.Next(1, 10));
        }
    }
}
