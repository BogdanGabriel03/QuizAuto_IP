/**************************************************************************
 *                                                                        *
 *  File:        Chestionar.cs                                            *
 *  Copyright:   (c) 2025, Ciausu Calin Ioan                              *
 *                                                                        *
 *  Description: Encapsulates the functionality of a questionnaire and    *
 *               provides methods for accessing and setting members.      *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/


using System.Windows.Markup;

namespace Model
{
    public class Chestionar
    {
        #region Private Member Variables
        /// <summary>
        /// intrebari = lista de întrebări
        /// _corect/_greșit = counter pentru numărul de răspunsuri corecte/greșite
        /// </summary>
        private Intrebare[] _intrebari;
        private int _corect, _gresit;
        #endregion

        #region Public Methods
        /// <summary>
        /// Setează valoarea inițială a contorilor și inițializează vectorul de întrebări
        /// </summary>
        /// <param name="intrebari"></param>
        public Chestionar(Intrebare[] intrebari)
        {
            this._intrebari = intrebari;
            this._corect = 0;
            this._gresit = 0;
        }
        /// <summary>
        /// modifică numărul de răspunsuri corecte/greșite
        /// </summary>
        public void IncrementCorect()
        {
            _corect++;
        }
        public void IncrementGresit()
        {
            _gresit++;
        }
        #endregion

        #region Getters / Setters

        /// <summary>
        /// Getter pentru lista de întrebări
        /// </summary>
        public Intrebare[] Intrebari
        {
            get { return _intrebari; }
        }
        /// <summary>
        /// Getter/Setter pentru numărul de răspunsuri corecte
        /// </summary>
        public int Corect
        {
            get { return _corect; }
            set { _corect = value; }
        }

        /// <summary>
        /// Getter/Setter pentru numărul de răspunsuri greșite
        /// </summary>
        public int Gresit
        {
            get { return _gresit; }
            set { _gresit = value; }
        }
        #endregion
    }
}

