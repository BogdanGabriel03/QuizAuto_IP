/**************************************************************************
 *                                                                        *
 *  File:        Intrebare.cs                                             *
 *  Copyright:   (c) 2025, Ciașu Călin Ioan                               *
 *                                                                        *
 *  Description: Dsiplays the result of the quiz to the user, whether it  *
 *               was ended by an ending condition or by a succseful       *
 *               attempt                                                  *
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
    public partial class FinalForms : Form
    {
        #region Private Member Variables
        /// <summary>
        /// Instanță a controller-ului
        /// </summary>
        private ChestionarController _controller;
        #endregion

        #region Public Methods
        /// <summary>
        /// Constructorul clasei FinalForms inițializează variabila privată, componentele de pe pagina și afișează prima întrebare
        /// </summary>
        /// <param name="controller">ChestionarController = instanța controllerului</param>
        public FinalForms(ChestionarController controller)
        {
            InitializeComponent();
            _controller = controller;
            AfiseazaRezultat();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Funcție responsabilă de afișarea rezultatului obșinut de utilizator
        /// </summary>
        private void AfiseazaRezultat() 
        {
            this.Controls.Clear();
            this.InitializeComponent();

            Label lblRezultat = new Label();
            lblRezultat.Font = new Font("Times New Roman", 20, FontStyle.Bold);
            lblRezultat.Location = new Point(10, 25);
            lblRezultat.AutoSize = true;
            if(_controller.TimeUp)
            {
                lblRezultat.Text = "Din pacate a expirat timpul!";
                _controller.TimeUp = false;
            }
            else if(_controller.GetGresit()>4)
                lblRezultat.Text = "Din pacate esti respins pentru ca ai gresit la "+ _controller.GetGresit() + " intrebari.";
            else
               lblRezultat.Text = "Felicitari, esti admis obtinand " + _controller.GetCorect() + " raspunsuri corecte.";
            this.Controls.Add(lblRezultat);
        }
        /// <summary>
        /// Funcția de callback a butonului care îl va redirecționa pe utilizator pe pagina acasă, și resetează scorul
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            HomePageForms f4 = new HomePageForms(_controller);
            _controller.ClearScore();
            f4.Show();
            this.Close();
        }

        private void FinalForms_Load(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
