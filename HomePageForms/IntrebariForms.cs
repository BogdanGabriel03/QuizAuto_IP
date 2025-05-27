/**************************************************************************
 *                                                                        *
 *  File:        IntrebariForms.cs                                        *
 *  Copyright:   (c) 2025, Ioan Bogdan-Gabriel                            *
 *                                                                        *
 *  Description: Responsible for the quiz pages and the user interactions *
 *                                                                        *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/
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
        #region Private Member variables
        /// <summary>
        /// _controller = instanță a controller-ului
        /// _timer = control de tip timer
        /// _remainingTIme = textul pentru a fi afișat pe interfață
        /// </summary>
        private ChestionarController _controller;
        private Timer _timer;
        private int _remainingTime;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructorul instanțiază controller-ul, creează controlul de tip timer și îl pornește
        /// </summary>
        /// <param name="controller">Chestionar Controller = Instanța controller-ului</param>
        public IntrebariForms(ChestionarController controller)
        {
            _controller = controller;
            AfiseazaIntrebari("00:10");
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += timer1_Tick;
            _timer.Start();
        }
        #endregion

        #region Private Methods
        private void IntrebariForms_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Afișează următoarea întrebare și setează valoarea timerului
        /// </summary>
        /// <param name="timerValue">string = Valoarea afișată pe interfață pentru timer</param>
        private void AfiseazaIntrebari(string timerValue)
        {
            this.Controls.Clear();
            this.InitializeComponent();
            label2.Text = timerValue;
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
        /// <summary>
        /// Trimite răspunsurile și verifică dacă utilizatorul a trecut de ultima întrebare
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                AfiseazaIntrebari(label2.Text);
            }
            if (_controller.EsteTerminat() || _controller.GetGresit() > 4)
            {
                _controller.ClearList();
                FinalForms f3 = new FinalForms(_controller);
                f3.Show();
                this.Close();
            }
        }
        /// <summary>
        /// Funcția de callback a timerului se apelează la o secundă și verifică de ficare dată dacă timpul a expirat, caz în care setează un flag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            string Time = label2.Text;
            try
            {
                string[] ms = Time.Split(':');
                _remainingTime = int.Parse(ms[0]) * 60 + int.Parse(ms[1]);
                _remainingTime--;
                label2.Text = _remainingTime / 60 + ":" + _remainingTime % 60;
                if (_remainingTime <= 0)
                {
                    _controller.TimeUp = _remainingTime <= 0;
                    _controller.ClearList();
                    FinalForms f3 = new FinalForms(_controller);
                    f3.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                label2.Text = ex.Message;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void IntrebariForms_Load_1(object sender, EventArgs e)
        {

        }
        #endregion
    }
}