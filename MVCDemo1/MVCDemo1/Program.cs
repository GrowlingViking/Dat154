using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MVCDemo1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static FormBase fBase;
        [STAThread]
        static void Main()
        {
            // Application.EnableVisualStyles();
            // Application.SetCompatibleTextRenderingDefault(false);
            fBase = new FormBase();

            fBase.IsMdiContainer = true;

            Controller c = new Controller();

            Form1 f1 = new Form1();
            f1.MdiParent = fBase;
      
            Form1 f2 = new Form1();
            f2.MdiParent = fBase;
     
            FormGui f3 = new FormGui();
            f3.MdiParent = fBase;

            FormDemo f4 = new FormDemo();
            f4.MdiParent = fBase;
            Model m = new Model();

            Controller.Update(1);
         
            Application.Run(fBase);
        }
    }
}
