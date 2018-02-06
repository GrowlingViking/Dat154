using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCDemo1
{
    public partial class FormDemo : Form, IView
    {
        public FormDemo()
        {
            InitializeComponent();
            Controller.Register(this);
            Show();
        }

        public void UpdateView(int updateV)
        {
            progressBar1.Value = updateV  % progressBar1.Maximum;
        }

        private void resetOrderClick(object sender, EventArgs e)
        {
            Controller.Update(0);
        }
    }
}
