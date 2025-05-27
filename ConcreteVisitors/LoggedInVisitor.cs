/**************************************************************************
 *                                                                        *
 *  File:        LoggedInVisitor.cs                                       *
 *  Copyright:   (c) 2025, Ioan Bogdan-Gabriel                            *
 *                                                                        *
 *  Description: Concrete visitor that helps create the log in benefits   *
 *               for the users; a logged in user can attempt the quiz     *
 *               as many times as possible                                *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorLibrary;

namespace ConcreteVisitors
{
    /// <summary>
    /// Clasă concretă pentru log in
    /// </summary>
    public class LoggedInVisitor : IVisitor
    {
        /// <summary>
        /// Metoda care schimbă numărul de accesări ale quiz-ului
        /// </summary>
        /// <param name="visitable"></param>
        public void Visit(IVisitable visitable)
        {
            if (visitable is IChestionarVisitable controller)
            {
                controller.NrAccesari = -1;
            }
        }
    }
}
