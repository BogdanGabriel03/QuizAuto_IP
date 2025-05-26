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
    public partial class IntrebariForms : Form
    {
        private ChestionarController _controller;
        private int nrIntrebare = 0;
        public IntrebariForms(ChestionarController controller)
        {
            InitializeComponent();
            _controller = controller;
            AfiseazaIntrebari();
        }

        private void IntrebariForms_Load(object sender, EventArgs e)
        {

        }

        private void AfiseazaIntrebari()
        {
            this.Controls.Clear();
            this.InitializeComponent();
            Intrebare intrebare = _controller.GetIntrebareCurenta();
            int y = 25;

            if (_controller.EsteTerminat() == false)
            {
                Label lblIntrebare = new Label();
                lblIntrebare.Text = intrebare.Text;
                lblIntrebare.Font = new Font("Times New Roman", 20, FontStyle.Bold);
                lblIntrebare.Location = new Point(10, y);
                lblIntrebare.AutoSize = true;
                this.Controls.Add(lblIntrebare);
                y += 100;

                int index = 0;
                foreach (var varianta in intrebare.Variante)
                {
                    CheckBox cb = new CheckBox
                    {
                        Text = varianta,
                        Font = Font = new Font("Times New Roman", 16),
                        Location = new Point(30, y),
                        AutoSize = true,
                        Tag = index
                    };
                    this.Controls.Add(cb);
                    y += 25;
                    index++;
                }
                y += 15;
            }
        }
        // submit answer
        private void button2_Click(object sender, EventArgs e)
        {
            if (!_controller.EsteTerminat())
            {
                int count = 0;
                foreach (Control control in this.Controls)
                {
                    if (control is CheckBox cb && cb.Checked)
                        count++;
                }
                int[] indiciSelectati = new int[count];

                int index = 0;
                foreach (Control control in this.Controls)
                {
                    if (control is CheckBox cb && cb.Checked)
                    {
                        indiciSelectati[index] = (int)cb.Tag;
                        index++;
                    }
                }
                _controller.TrimiteRaspuns(indiciSelectati);
                nrIntrebare++;
                AfiseazaIntrebari();
            }
            if(_controller.EsteTerminat())
            {
                _controller.ClearList();
                FinalForms f3 = new FinalForms(_controller);
                f3.Show();
                this.Close();
            }
        }
    }
}