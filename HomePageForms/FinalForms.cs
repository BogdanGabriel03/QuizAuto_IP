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
    public partial class FinalForms : Form
    {
        public FinalForms()
        {
            InitializeComponent();
        }

        private void IntrebariForms_Load(object sender, EventArgs e)
        {
            IntreabriForms f3 = new IntreabriForms();
            f3.Show();
            this.Close();
        }
    }
}
