using Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomePageForms
{
    public partial class HomePageForms : Form
    {
        private ChestionarController _controller;
        public HomePageForms(ChestionarController controller)
        {
            InitializeComponent();
            _controller = controller;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///  Load chestionar button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            IntrebariForms f2 = new IntrebariForms(_controller);
            f2.Show();
            this.Close();
        }

         
        private void button2_Click(object sender, EventArgs e)
        {
            LogInForms f5 = new LogInForms();
            f5.Show();
        }

        /// Help button - 
        private void button1_Click_1(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "help.chm");
        }
    }
}
