/**************************************************************************
 *                                                                        *
 *  File:        IVisitable.cs                                            *
 *  Copyright:   (c) 2025, Ioan Bogdan-Gabriel                            *
 *                                                                        *
 *  Description: Interface for the visitable objects that are also        *
 *               ChestionarController                                     *
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
    /// Interfața pentru obiecte de tip ChestionarController care sunt și vizitabile
    /// </summary>
    public interface IChestionarVisitable : IVisitable
    {
        int NrAccesari { get; set; }
    }
}
