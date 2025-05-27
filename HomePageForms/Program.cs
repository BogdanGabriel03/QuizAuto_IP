/**************************************************************************
 *                                                                        *
 *  File:        Chestionar.cs                                            *
 *  Copyright:   (c) 2025, Ciausu Calin Ioan                              *
 *                                                                        *
 *  Description: This file serves as the entry point for the Windows Forms*
 *               application.                                             *
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
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomePageForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            HomePageForms f1= new HomePageForms(new ChestionarController());
            f1.Show();
            Application.Run();
        }
    }
}
