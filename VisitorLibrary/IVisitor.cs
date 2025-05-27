/**************************************************************************
 *                                                                        *
 *  File:        IVisitor.cs                                              *
 *  Copyright:   (c) 2025, Ioan Bogdan-Gabriel                            *
 *                                                                        *
 *  Description: Most general interface for the visitors                  *
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

namespace VisitorLibrary
{
    /// <summary>
    /// Interfața genrală pentru oboectele de tip vizitator
    /// </summary>
    public interface IVisitor
    {
        void Visit(IVisitable controller);
    }
}
