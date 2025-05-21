using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Intrebare
    {
        private string text;
        private string[] variante;
        private int[] varianteCorecte;

        public Intrebare(string text, string[] variante, int[] varianteCorecte)
        {
            this.text = text;
            this.variante = variante;
            this.varianteCorecte = varianteCorecte;
        }

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


    }
}
