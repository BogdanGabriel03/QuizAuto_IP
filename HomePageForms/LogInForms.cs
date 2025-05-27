/**************************************************************************
 *                                                                        *
 *  File:        Intrebare.cs                                             *
 *  Copyright:   (c) 2025, Ciausu Calin-Ioan                              *
 *                                                                        *
 *  Description: Login page displays the common login textboxes for users *
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
    public partial class LogInForms : Form
    {
        #region Private Variable Members
        private ChestionarController _controller;
        private HomePageForms _homePage;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor(ChestionarController controller, HomePageForms homePage)
        /// </summary>
        /// <param name="controller">Instanta controller-ului</param>
        /// <param name="homePage">Instanta paginii de start parinte</param>
        public LogInForms(ChestionarController controller, HomePageForms homePage)
        {
            InitializeComponent();
            _controller = controller;
            _homePage = homePage;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Log in button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string parola = textBox2.Text;

            if (username == "B" && parola == "1234")
            {
                _controller.Accept(new LoggedInVisitor());
                _controller.LoggedIn = true;
                _controller.Username = username;
                _homePage.UpdateUI(username);
                this.Close();
            }
            else
            {
                MessageBox.Show("Nume de utilizator/parola invalide", "Autentificare esuata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
