using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Controller
{
    public class ChestionarController
    {
        private Chestionar chestionar;
        private int indexIntrebareCurenta;
        private string caleFisier = "intrebari.txt";
        private Random rand;
        private List<int> generated;
        public ChestionarController()
        {
            Intrebare[] intrebari = CitesteIntrebariDinFisier(caleFisier);
            chestionar = new Chestionar(intrebari);
            rand = new Random();
            generated = new List<int>();
            indexIntrebareCurenta = rand.Next(53);
            //generated.Add(indexIntrebareCurenta);
        }

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
                chestionar.IncrementScor();
            }

            indexIntrebareCurenta = NextQuestion();
        }

        public int GetScor()
        {
            return chestionar.Scor;
        }

        public bool EsteTerminat()
        {
            return generated.Count > 26;
        }

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

        private int NextQuestion()
        {
            int x;
            do
            {
                x = rand.Next(53);
            } while (generated.Contains(x));
            return x;
        }

        public void ClearList()
        {
            generated.Clear();
        }
    }
}
