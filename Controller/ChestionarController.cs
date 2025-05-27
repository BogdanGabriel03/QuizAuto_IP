/**************************************************************************
 *                                                                        *
 *  File:        ChestionarController.cs                                  *
 *  Copyright:   (c) 2025, Ciausu Calin-Ioan                              *
 *                                                                        *
 *  Description: Manages quiz flow: loads questions, provides the current * 
 *               question, verifies answers, tracks score, determines     *
 *               quiz end, and supports the Visitor pattern. Reads        *
 *               questions from a file and handles quiz state.            *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/

using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VisitorLibrary;

namespace Controller
{
    /// <summary>
    /// Controller class based on the MVC model
    /// </summary>
    public class ChestionarController : IChestionarVisitable
    {
        #region Private Member Variables
        private Chestionar chestionar;
        private int indexIntrebareCurenta;
        private string caleFisier = "intrebari.txt";
        private Random rand;
        private List<int> generated;
        private bool _timeUp;
        private int _nrAccesari;
        private bool _loggedIn;
        private string _username;
        #endregion

        #region Construcotrs
        /// <summary>
        /// Constructor
        /// </summary>
        public ChestionarController()
        {
            Intrebare[] intrebari = CitesteIntrebariDinFisier(caleFisier);
            chestionar = new Chestionar(intrebari);
            rand = new Random();
            generated = new List<int>();
            indexIntrebareCurenta = rand.Next(53);
            _nrAccesari = 2;
            _loggedIn = false;
            //generated.Add(indexIntrebareCurenta);
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Permite obținerea întrebării curente
        /// </summary>
        /// <returns> Intrebare sau null dacă quiz-ul s-a terminat, din orice motiv</returns>
        public Intrebare GetIntrebareCurenta()
        {
            if (!EsteTerminat())
            {
                generated.Add(indexIntrebareCurenta);
                return chestionar.Intrebari[indexIntrebareCurenta];
            }
            else
                return null;
        }
        /// <summary>
        /// Trimite răspunsul, îl verifică și incremetează contorul corespunzător
        /// </summary>
        /// <param name="varianteSelectate"> int[]; vector cu indecșii răspunsurilor selectate de utilizator</param>
        public void TrimiteRaspuns(int[] varianteSelectate)
        {
            if (EsteTerminat())
            {
                return;
            }

            Intrebare intrebare = chestionar.Intrebari[indexIntrebareCurenta];

            // Compara listele (ordinea nu contează)
            var corecte = intrebare.VarianteCorecte.OrderBy(x => x);
            var selectate = varianteSelectate.OrderBy(x => x);

            if (corecte.SequenceEqual(selectate))
            {
                chestionar.IncrementCorect();
            }
            else
            {
                chestionar.IncrementGresit();
            }

            indexIntrebareCurenta = NextQuestion();
        }
        /// <summary>
        /// Returnează numărul de răspunsuri corecte
        /// </summary>
        /// <returns></returns>
        public int GetCorect()
        {
            return chestionar.Corect;
        }
        /// <summary>
        /// Retunrează numărul de răspunsuri greșite
        /// </summary>
        /// <returns></returns>
        public int GetGresit()
        {
            return chestionar.Gresit;
        }
        /// <summary>
        /// Verifică dacă chestionarul s-a terminat prin bifarea ultimei întrebări
        /// </summary>
        /// <returns></returns>
        public bool EsteTerminat()
        {
            return generated.Count > 26;
        }
        /// <summary>
        /// Resetează lista de indecși generați random
        /// </summary>
        public void ClearList()
        {
            generated.Clear();
        }
        /// <summary>
        /// Resetează scorul
        /// </summary>
        public void ClearScore()
        {
            chestionar.Corect = 0;
            chestionar.Gresit = 0;
        }
        /// <summary>
        /// Accept visitor
        /// </summary>
        /// <param name="visitor"> type of visitor </param>
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Citește întrebările dintr-un fișier și returnează informația sub formă de vector de intrebări ( Intrebare[] )
        /// </summary>
        /// <param name="cale"></param>
        /// <returns></returns>
        private Intrebare[] CitesteIntrebariDinFisier(string cale)
        {
            var listaIntrebari = new List<Intrebare>();
            var linii = File.ReadAllLines(cale);

            for (int i = 0; i < linii.Length; i += 5)
            {
                string enunt = linii[i];
                string[] variante = new string[] { linii[i + 1], linii[i + 2], linii[i + 3] };

                // Parsează răspunsuri multiple (ex: "0,2")
                var raspunsuriCorecte = linii[i + 4].Split(',').Select(int.Parse).ToArray();

                listaIntrebari.Add(new Intrebare(enunt, variante, raspunsuriCorecte));
            }

            return listaIntrebari.ToArray();
        }
        /// <summary>
        /// Logica de selectare a următoarei întrebări aleatorii
        /// </summary>
        /// <returns></returns>
        private int NextQuestion()
        {
            int x;
            do
            {
                x = rand.Next(53);
            } while (generated.Contains(x));
            return x;
        }
        #endregion

        #region Getter / Setters
        /// <summary>
        /// Getter/Setter pentru flagul de timp expirat
        /// </summary>
        public bool TimeUp { get { return _timeUp; } set { _timeUp = value; } }
        /// <summary>
        /// Getter/Setter pentru numărul posibil de accesări a chestionarului
        /// </summary>
        public int NrAccesari { get { return _nrAccesari; } set { _nrAccesari = value; } }
    
        public bool LoggedIn 
        { 
            get { return _loggedIn; } 
            set 
            { 
                if(_loggedIn != value)
                {
                    _loggedIn = value;
                } 
            } 
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
        #endregion
    }
}
