/**************************************************************************
 *                                                                        *
 *  File:        Intrebare.cs                                             *
 *  Copyright:   (c) 2025, Ciausu Calin-Ioan                              *
 *                                                                        *
 *  Description: Stores information for driving school type of quiz       *
 *               questions                                                *
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

namespace Model
{
    /// <summary>
    /// Clasă responsabilă cu păstrarea informațiilor despre fiecare întrebare în parte
    /// </summary>
    public class Intrebare
    {
        #region Private Member Variables
        /// <summary>
        /// Membrii privați ai clasei:
        ///     - text = textul întrebării
        ///     - variante = vector de stringuri ce conține variantele de răspuns
        ///     - varianteCorecte = vector ce conține indecșii răspunsurilor corecte
        /// </summary>
        private string _text;
        private string[] _variante;
        private int[] _varianteCorecte;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructorul clasei, cu rol de a seta informațiile private
        /// </summary>
        /// <param name="text">string = Textul intrebarii</param>
        /// <param name="variante">string[] = Variantele de răspuns</param>
        /// <param name="varianteCorecte">int[] = Indecșii variantelor corecte de răspuns</param>
        public Intrebare(string text, string[] variante, int[] varianteCorecte)
        {
            this._text = text;
            this._variante = variante;
            this._varianteCorecte = varianteCorecte;
        }
        #endregion

        #region Getters / Setters
        /// <summary>
        /// Getters pentru fiecare dintre variabilele private
        /// </summary>
        public string Text
        {
            get { return _text; }
        }

        public string[] Variante
        {
            get { return _variante; }
        }

        public int[] VarianteCorecte
        {
            get { return _varianteCorecte; }
        }
        #endregion
    }
}
