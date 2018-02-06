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
    // All views must implement this interface
    interface IView
    {
        void UpdateView(int v);
    }

    public partial class Form1 : Form, IView
    {
        public void UpdateView(int v)
        {
             textBox1.Text = v.ToString(); 
        }
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = Controller.GetValue().ToString();
            Controller.Register(this);
            Show();
        }

        // Add a new order
        private void addOrderClick(object sender, EventArgs e)
        {
            int i = Controller.GetValue() + 1;
            Controller.Update(i);
        }

        // Handle an order
        private void handleOrderClick(object sender, EventArgs e)
        {
            int i = Controller.GetValue() - 1;
            if (i > 0)
                Controller.Update(i);
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           // Controller.Update(Convert.ToInt32(textBox1.Text));
        }
    }
}
