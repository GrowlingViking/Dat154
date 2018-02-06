using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;


namespace MVCDemo1
{
    public partial class FormGui : Form, IView
    {
        public void UpdateView(int updateV)
        {
            List<int> a = Controller.History();
            int from = Math.Max(0, a.Count - 50);
            chart1.Series["Series1"].Points.Clear();
            for (int i = from; i < a.Count; i++)
                chart1.Series["Series1"].Points.AddXY(i, a[i]);
        }
        public FormGui()
        {
            InitializeComponent();
            Controller.Register(this);
            Show();
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        // The boss may add orders
        private void addOrderClick(object sender, EventArgs e)
        {
            int val = Controller.GetValue() + 1;
            Controller.Update(val);
         }
    }
}
