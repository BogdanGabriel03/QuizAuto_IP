﻿using Model;
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

        public ChestionarController()
        {
            Intrebare[] intrebari = CitesteIntrebariDinFisier(caleFisier);
            chestionar = new Chestionar(intrebari);
            indexIntrebareCurenta = 0;
        }

        public Intrebare GetIntrebareCurenta()
        {
            if (!EsteTerminat())
                return chestionar.Intrebari[indexIntrebareCurenta];
            else
                return null;
        }

        public void TrimiteRaspuns(int[] varianteSelectate)
        {
            if (EsteTerminat()) return;

            Intrebare intrebare = chestionar.Intrebari[indexIntrebareCurenta];

            // Compara listele (ordinea nu contează)
            var corecte = intrebare.VarianteCorecte.OrderBy(x => x);
            var selectate = varianteSelectate.OrderBy(x => x);

            if (corecte.SequenceEqual(selectate))
            {
                chestionar.IncrementScor();
            }

            indexIntrebareCurenta++;
        }

        public int GetScor()
        {
            return chestionar.Scor;
        }

        public bool EsteTerminat()
        {
            return indexIntrebareCurenta >= chestionar.Intrebari.Length;
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
    }
}
