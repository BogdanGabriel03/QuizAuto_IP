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
        private ChestionarController _controller;
        public FinalForms(ChestionarController controller)
        {
            InitializeComponent();
            _controller = controller;
            AfiseazaRezultat();
        }

        private void AfiseazaRezultat() 
        {
            this.Controls.Clear();
            this.InitializeComponent();

            Label lblRezultat = new Label();
            lblRezultat.Font = new Font("Times New Roman", 20, FontStyle.Bold);
            lblRezultat.Location = new Point(10, 25);
            lblRezultat.AutoSize = true;
            if(_controller.GetGresit()>4)
                lblRezultat.Text = "Din pacate esti respins pentru ca ai gresit la "+ _controller.GetGresit() + " intrebari.";
            else
               lblRezultat.Text = "Felicitari, esti admis obtinand " + _controller.GetCorect() + " raspunsuri corecte.";
            this.Controls.Add(lblRezultat);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            HomePageForms f4 = new HomePageForms(_controller);
            f4.Show();
            this.Close();
        }

        private void FinalForms_Load(object sender, EventArgs e)
        {

        }
    }
}
