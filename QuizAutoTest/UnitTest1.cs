/**************************************************************************
 *                                                                        *
 *  File:        Intrebare.cs                                             *
 *  Copyright:   (c) 2025, Popescu Andrei-Eduard                          *
 *                                                                        *
 *  Description: Test Unit                                                *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Controller;
using Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;


namespace QuizAutoTest
{
    [TestClass]
    public class ChestionarControllerTests
    {
        [TestMethod]
        public void TestIntrebareCurentaNuEsteNullLaStart()
        {
            var controller = new ChestionarController();
            var intrebare = controller.GetIntrebareCurenta();
            Assert.IsNotNull(intrebare);
        }

        [TestMethod]
        public void TestTrimiteRaspunsCorectCresteScorul()
        {
            var controller = new ChestionarController();
            var intrebare = controller.GetIntrebareCurenta();
            controller.TrimiteRaspuns(intrebare.VarianteCorecte);
            Assert.AreEqual(1, controller.GetCorect());
        }

        [TestMethod]
        public void TestTrimiteRaspunsGresitCresteGresit()
        {
            var controller = new ChestionarController();
            var intrebare = controller.GetIntrebareCurenta();

            int[] gresit = Enumerable.Range(0, intrebare.Variante.Length)
                                     .Where(i => !intrebare.VarianteCorecte.Contains(i))
                                     .ToArray();

            if (gresit.Length == 0)
                gresit = new int[] { (intrebare.VarianteCorecte.Max() + 1) % intrebare.Variante.Length };

            controller.TrimiteRaspuns(gresit);

            Assert.AreEqual(1, controller.GetGresit());
        }

        [TestMethod]
        public void TestInitialScorZero()
        {
            var controller = new ChestionarController();
            Assert.AreEqual(0, controller.GetCorect());
            Assert.AreEqual(0, controller.GetGresit());
        }

        [TestMethod]
        public void TestRaspunsIncorectNuAruncaExceptie()
        {
            var controller = new ChestionarController();
            try
            {
                controller.TrimiteRaspuns(new int[] { 99 });
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail("Răspunsul invalid nu ar trebui să arunce excepție.");
            }
        }

        [TestMethod]
        public void TestRaspunsDuplicatEsteInvalid()
        {
            var controller = new ChestionarController();
            var intrebare = controller.GetIntrebareCurenta();
            var duplicat = intrebare.VarianteCorecte.Concat(intrebare.VarianteCorecte).ToArray();
            controller.TrimiteRaspuns(duplicat);

            Assert.AreEqual(0, controller.GetCorect());
        }

        [TestMethod]
        public void TestToateIntrebarileAu3Variante()
        {
            var controller = new ChestionarController();
            while (!controller.EsteTerminat())
            {
                var intrebare = controller.GetIntrebareCurenta();
                Assert.AreEqual(3, intrebare.Variante.Length);
                controller.TrimiteRaspuns(intrebare.VarianteCorecte);
            }
        }

        [TestMethod]
        public void TestIntrebariUnice()
        {
            var controller = new ChestionarController();
            var enunturi = new HashSet<string>();
            while (!controller.EsteTerminat())
            {
                var intrebare = controller.GetIntrebareCurenta();
                enunturi.Add(intrebare.Text);
                controller.TrimiteRaspuns(intrebare.VarianteCorecte);
            }

            Assert.IsTrue(enunturi.Count > 1);
        }

        [TestMethod]
        public void TestIndexuriVarianteCorecteInInterval()
        {
            var controller = new ChestionarController();
            while (!controller.EsteTerminat())
            {
                var intrebare = controller.GetIntrebareCurenta();
                foreach (var index in intrebare.VarianteCorecte)
                {
                    Assert.IsTrue(index >= 0 && index < intrebare.Variante.Length);
                }
                controller.TrimiteRaspuns(intrebare.VarianteCorecte);
            }
        }

        [TestMethod]
        public void TestIntrebareCurentaDevineNullDupaTerminare()
        {
            var controller = new ChestionarController();
            while (!controller.EsteTerminat())
            {
                controller.TrimiteRaspuns(controller.GetIntrebareCurenta().VarianteCorecte);
            }

            Assert.IsNull(controller.GetIntrebareCurenta());
        }

        [TestMethod]
        public void TestIncrementGresitDacaRaspunsGresit()
        {
            var controller = new ChestionarController();
            var intrebare = controller.GetIntrebareCurenta();
            var gresit = new int[] { (intrebare.VarianteCorecte.Max() + 1) % 3 };

            int gresitInitial = controller.GetGresit();
            controller.TrimiteRaspuns(gresit);

            Assert.AreEqual(gresitInitial + 1, controller.GetGresit());
        }

        [TestMethod]
        public void TestGetCorectInitialZero()
        {
            var controller = new ChestionarController();
            Assert.AreEqual(0, controller.GetCorect());
        }

        [TestMethod]
        public void TestGetGresitInitialZero()
        {
            var controller = new ChestionarController();
            Assert.AreEqual(0, controller.GetGresit());
        }

        [TestMethod]
        public void TestRaspunsInvalidNegativ()
        {
            var controller = new ChestionarController();
            var intrebare = controller.GetIntrebareCurenta();

            try
            {
                controller.TrimiteRaspuns(new int[] { -1 });
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail("Răspunsul negativ nu trebuie să arunce excepții.");
            }
        }

        [TestMethod]
        public void TestClearListReseteazaIntrebari()
        {
            var controller = new ChestionarController();
            var intrebare1 = controller.GetIntrebareCurenta();
            controller.TrimiteRaspuns(new int[] { });

            controller.ClearList();
            var intrebare2 = controller.GetIntrebareCurenta();

            Assert.IsNotNull(intrebare2);
        }

        [TestMethod]
        public void TestToateIntrebarileAuEnunt()
        {
            var controller = new ChestionarController();
            while (!controller.EsteTerminat())
            {
                var intrebare = controller.GetIntrebareCurenta();
                Assert.IsFalse(string.IsNullOrWhiteSpace(intrebare.Text));
                controller.TrimiteRaspuns(new int[] { });
            }
        }

        [TestMethod]
        public void TestFisierContineIntrebari()
        {
            var controller = new ChestionarController();
            Assert.IsNotNull(controller.GetIntrebareCurenta());
        }

        [TestMethod]
        public void TestRaspunsPartialGresit()
        {
            var controller = new ChestionarController();
            var intrebare = controller.GetIntrebareCurenta();

            if (intrebare.VarianteCorecte.Length > 1)
            {
                controller.TrimiteRaspuns(new int[] { intrebare.VarianteCorecte[0] });
                Assert.AreEqual(0, controller.GetCorect());
                Assert.AreEqual(1, controller.GetGresit());
            }
        }

        [TestMethod]
        public void TestRaspunsRandomInvalidNuAfecteazaExceptii()
        {
            var controller = new ChestionarController();

            try
            {
                controller.TrimiteRaspuns(new int[] { 100 });
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail("TrimiteRaspuns nu trebuie să arunce excepție pentru index mare.");
            }
        }

        [TestMethod]
        public void TestGetIntrebareCurentaInainteDeStart()
        {
            var controller = new ChestionarController();
            var intrebare = controller.GetIntrebareCurenta();
            Assert.IsNotNull(intrebare);
        }

        [TestMethod]
        public void TestIntrebariAuMinimUnRaspunsCorect()
        {
            var controller = new ChestionarController();

            while (!controller.EsteTerminat())
            {
                var intrebare = controller.GetIntrebareCurenta();
                Assert.IsTrue(intrebare.VarianteCorecte.Length >= 1);
                controller.TrimiteRaspuns(new int[] { });
            }
        }

        [TestMethod]
        public void TestToateIntrebarileSuntDistincte()
        {
            var controller = new ChestionarController();
            var enunturi = new HashSet<string>();

            while (!controller.EsteTerminat())
            {
                var intrebare = controller.GetIntrebareCurenta();
                Assert.IsFalse(enunturi.Contains(intrebare.Text));
                enunturi.Add(intrebare.Text);
                controller.TrimiteRaspuns(new int[] { });
            }
        }

        [TestMethod]
        public void TestVarianteCorecteSuntIndexuriValide()
        {
            var controller = new ChestionarController();
            while (!controller.EsteTerminat())
            {
                var intrebare = controller.GetIntrebareCurenta();
                foreach (int idx in intrebare.VarianteCorecte)
                    Assert.IsTrue(idx >= 0 && idx < intrebare.Variante.Length);
                controller.TrimiteRaspuns(new int[] { });
            }
        }

        [TestMethod]
        public void TestFisierAreFormatValid()
        {
            var path = "intrebari.txt";
            var linii = File.ReadAllLines(path);
            Assert.IsTrue(linii.Length % 5 == 0, "Fișierul trebuie să conțină multipli de 5 linii.");
        }
        [TestMethod]
        public void TestIntrebareCurentaSeModificaDupaRaspuns()
        {
            var controller = new ChestionarController();
            var prima = controller.GetIntrebareCurenta();
            controller.TrimiteRaspuns(prima.VarianteCorecte);
            var aDoua = controller.GetIntrebareCurenta();

            Assert.AreNotEqual(prima.Text, aDoua?.Text);
        }

        [TestMethod]
        public void TestTrimiteRaspunsGolEsteConsideratGresit()
        {
            var controller = new ChestionarController();
            controller.TrimiteRaspuns(new int[0]);
            Assert.AreEqual(1, controller.GetGresit());
        }

        [TestMethod]
        public void TestVarianteCorecteNuAuDuplicate()
        {
            var controller = new ChestionarController();
            var intrebare = controller.GetIntrebareCurenta();
            var distincte = intrebare.VarianteCorecte.Distinct().Count();

            Assert.AreEqual(intrebare.VarianteCorecte.Length, distincte);
        }

        [TestMethod]
        public void TestToateVarianteleSuntDiferite()
        {
            var controller = new ChestionarController();
            var intrebare = controller.GetIntrebareCurenta();
            var variante = intrebare.Variante;

            Assert.AreEqual(variante.Length, variante.Distinct().Count());
        }

        [TestMethod]
        public void TestEnuntulNuEsteGolSauNull()
        {
            var controller = new ChestionarController();
            var intrebare = controller.GetIntrebareCurenta();

            Assert.IsFalse(string.IsNullOrWhiteSpace(intrebare.Text));
        }

        [TestMethod]
        public void TestNuSePotTrimiteIndexuriNegative()
        {
            var controller = new ChestionarController();
            controller.TrimiteRaspuns(new int[] { -1 });

            Assert.AreEqual(1, controller.GetGresit());
        }

        [TestMethod]
        public void TestLaFinalListaIntrebariSeTermina()
        {
            var controller = new ChestionarController();
            while (!controller.EsteTerminat())
                controller.TrimiteRaspuns(controller.GetIntrebareCurenta().VarianteCorecte);

            Assert.IsNull(controller.GetIntrebareCurenta());
        }

        [TestMethod]
        public void TestIntrebareCurentaEsteNullDupaTerminare()
        {
            var controller = new ChestionarController();
            while (!controller.EsteTerminat())
                controller.TrimiteRaspuns(controller.GetIntrebareCurenta().VarianteCorecte);

            Assert.IsNull(controller.GetIntrebareCurenta());
        }

        [TestMethod]
        public void TestTrimiteRaspunsDupaFinalNuAfecteazaScorul()
        {
            var controller = new ChestionarController();
            while (!controller.EsteTerminat())
                controller.TrimiteRaspuns(controller.GetIntrebareCurenta().VarianteCorecte);

            int scor = controller.GetCorect();
            controller.TrimiteRaspuns(new int[] { 0 });
            Assert.AreEqual(scor, controller.GetCorect());
        }

        [TestMethod]
        public void TestTrimiteRaspunsCuIndexMareNuArunca()
        {
            var controller = new ChestionarController();

            try
            {
                controller.TrimiteRaspuns(new int[] { 100 });
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail("TrimiteRaspuns nu trebuie să arunce excepție pentru index mare.");
            }
        }

        [TestMethod]
        public void TestTrimiteRaspunsCuIndexNegativNuArunca()
        {
            var controller = new ChestionarController();

            try
            {
                controller.TrimiteRaspuns(new int[] { -10 });
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail("TrimiteRaspuns nu trebuie să arunce excepție pentru index negativ.");
            }
        }

        [TestMethod]
        public void TestTrimiteRaspunsCuIndexInvalidNuCresteScorul()
        {
            var controller = new ChestionarController();
            controller.TrimiteRaspuns(new int[] { 5 }); // invalid
            Assert.AreEqual(0, controller.GetCorect());
        }

        [TestMethod]
        public void TestConstructorFaraParametruIncarcaImplicit()
        {
            var controller = new ChestionarController();
            Assert.IsNotNull(controller.GetIntrebareCurenta());
        }

        [TestMethod]
        public void TestIntrebareAreMinimUnRaspunsCorect()
        {
            var controller = new ChestionarController();
            var intrebare = controller.GetIntrebareCurenta();
            Assert.IsTrue(intrebare.VarianteCorecte.Length >= 1);
        }

        [TestMethod]
        public void TestVarianteSuntPopulareDiferite()
        {
            var controller = new ChestionarController();
            var variante = controller.GetIntrebareCurenta().Variante;
            Assert.AreEqual(3, variante.Distinct().Count());
        }

        [TestMethod]
        public void TestRaspunsPartialGresitEsteGresit()
        {
            var controller = new ChestionarController();
            var intrebare = controller.GetIntrebareCurenta();

            if (intrebare.VarianteCorecte.Length > 1)
            {
                var partial = intrebare.VarianteCorecte.Take(1).ToArray();
                controller.TrimiteRaspuns(partial);
                Assert.AreEqual(1, controller.GetGresit());
            }
            else
            {
                Assert.Inconclusive("Întrebarea nu are suficiente răspunsuri corecte pentru test.");
            }
        }


    }
}

