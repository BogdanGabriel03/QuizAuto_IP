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
        /// Getters pentru toți membrii privați și setters pentru contoare
        /// </summary>
        public Intrebare[] Intrebari
        {
            get { return _intrebari; }
        }

        public int Corect
        {
            get { return _corect; }
            set { _corect = value; }
        }

        public int Gresit
        {
            get { return _gresit; }
            set { _gresit = value; }
        }
        #endregion
    }
}

