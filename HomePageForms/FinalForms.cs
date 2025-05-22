using Controller;
using Model;
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
            AfiseazaRezultat();
        }

        private void AfiseazaRezultat() 
        {
            var chestionarController = GlobalController.Controller;
            this.Controls.Clear();
            this.InitializeComponent();

            Label lblRezultat = new Label();
            lblRezultat.Font = new Font("Times New Roman", 20, FontStyle.Bold);
            lblRezultat.Location = new Point(10, 25);
            lblRezultat.AutoSize = true;
            if(chestionarController.GetScor()>1)
                lblRezultat.Text = "Felicitari, esti admis obtinand " + chestionarController.GetScor() + " puncte.";
            else
                lblRezultat.Text = "Din pacate ai obtinut doar " + chestionarController.GetScor() + " puncte, mai invata!";
            this.Controls.Add(lblRezultat);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            IntrebariForms f4 = new IntrebariForms();
            f4.Show();
            this.Close();
        }
    }
}
