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
    public partial class IntreabriForms : Form
    {
        public IntreabriForms()
        {
            InitializeComponent();
            AfiseazaIntrebari();
            
        }

        private void FinalForms_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void AfiseazaIntrebari()
        {
            ChestionarController chestionarController = new ChestionarController();
            Intrebare intrebare = chestionarController.GetIntrebareCurenta();
            int y = 10;

            Label lblIntrebare = new Label();
            lblIntrebare.Text = intrebare.Text;
            lblIntrebare.Location = new Point(10, y);
            lblIntrebare.AutoSize = true;
            this.Controls.Add(lblIntrebare);
            y += 25;


            foreach (var varianta in intrebare.Variante)
            {
                RadioButton rb = new RadioButton
                {
                    Text = varianta,
                    Location = new Point(30, y),
                    AutoSize = true
                };
                this.Controls.Add(rb);
                y += 25;
            }
            y += 15;
        }
    }
}
