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
        private string text;
        private string[] variante;
        private int[] varianteCorecte;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructorul clasei, cu rol de a seta informațiile private
        /// </summary>
        /// <param name="text"></param>
        /// <param name="variante"></param>
        /// <param name="varianteCorecte"></param>
        public Intrebare(string text, string[] variante, int[] varianteCorecte)
        {
            this.text = text;
            this.variante = variante;
            this.varianteCorecte = varianteCorecte;
        }
        #endregion

        #region Getters / Setters
        /// <summary>
        /// Getters pentru fiecare dintre variabilele private
        /// </summary>
        public string Text
        {
            get { return text; }
        }

        public string[] Variante
        {
            get { return variante; }
        }

        public int[] VarianteCorecte
        {
            get { return varianteCorecte; }
        }
        #endregion
    }
}
