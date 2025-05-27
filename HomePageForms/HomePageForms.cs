/**************************************************************************
 *                                                                        *
 *  File:        HomePageForms.cs                                         *
 *  Copyright:   (c) 2025, Ioan Bogdan-Gabriel                            *
 *                                                                        *
 *  Description: Displays the main page of the user interface and the     *
 *               controls to connect to other views                       *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/

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
using ConcreteVisitors;

namespace HomePageForms
{
    public partial class HomePageForms : Form
    {
        #region Private Member variables
        private ChestionarController _controller;
        #endregion

        #region Public Methods
        /// <summary>
        /// Constructor HomePageForms ( ChestionarController controller )
        /// </summary>
        /// <param name="controller"> Instanțiază controller-ul</param>
        public HomePageForms(ChestionarController controller)
        {
            InitializeComponent();
            _controller = controller;
            UpdateUI(_controller.Username);
        }
        /// <summary>
        /// Schimbă interfața cu utilizatorul dacă acesta este conectat/deconectat
        /// </summary>
        public void UpdateUI(string username)
        {
            if(_controller.LoggedIn)
            {
                label3.Text = "Logged in as " + username;
                button2.Text = "Log Out";
            }
            else
            {
                label3.Text = "Not logged in";
                button2.Text = "Log in";
            }
        }
        #endregion

        #region Private Methods
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Funcția de callback a butonului de începere a chestionarului
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if(_controller.NrAccesari > 0 || _controller.NrAccesari == -1)
            {
                if(_controller.NrAccesari>0) _controller.NrAccesari = _controller.NrAccesari - 1;
                IntrebariForms f2 = new IntrebariForms(_controller);
                f2.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Ai rămas fără încercări! Intră în cont pentru a beneficia de un număr nelimitat de încercări!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

         /// <summary>
         /// Funcția de callback a butonului de login
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if(!_controller.LoggedIn)
            {
                LogInForms f5 = new LogInForms(_controller, this);
                f5.Show();
            }
            else
            {
                _controller.LoggedIn = false;
                UpdateUI("");
                _controller.Accept(new LoggedOutVisitor());
            }
        }

        /// <summary>
        /// Funcția de callback a butonului help
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "help.chm");
        }
        #endregion

        private void label3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
