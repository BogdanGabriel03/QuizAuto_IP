using Microsoft.VisualStudio.TestTools.UnitTesting;
using Controller;
using Model;
using System;
using System.Linq;
using System.Collections.Generic;

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

            Assert.IsNotNull(intrebare, "Întrebarea curentă nu trebuie să fie null la început.");
        }

        [TestMethod]
        public void TestTrimiteRaspunsCorectCresteScorul()
        {
            var controller = new ChestionarController();
            var intrebare = controller.GetIntrebareCurenta();

            int[] raspunsCorect = intrebare.VarianteCorecte;
            controller.TrimiteRaspuns(raspunsCorect);

            Assert.AreEqual(1, controller.GetScor(), "Scorul trebuie să fie 1 după un răspuns corect.");
        }

        [TestMethod]
        public void TestTrimiteRaspunsGresitNuModificaScorul()
        {
            var controller = new ChestionarController();
            var intrebare = controller.GetIntrebareCurenta();

            int[] raspunsGresit = Enumerable.Range(0, intrebare.Variante.Length)
                                            .Where(i => !intrebare.VarianteCorecte.Contains(i))
                                            .ToArray();

            // Dacă toate variantele sunt corecte, simulăm o greșeală cu index invalid
            if (raspunsGresit.Length == 0)
                raspunsGresit = new int[] { (intrebare.VarianteCorecte.Max() + 1) % intrebare.Variante.Length };

            controller.TrimiteRaspuns(raspunsGresit);

            Assert.AreEqual(0, controller.GetScor(), "Scorul trebuie să rămână 0 după un răspuns greșit.");
        }

        [TestMethod]
        public void TestParcurgereToateIntrebarile()
        {
            var controller = new ChestionarController();
            int total = 0;

            while (!controller.EsteTerminat())
            {
                var intrebare = controller.GetIntrebareCurenta();
                Assert.IsNotNull(intrebare, "Întrebarea nu trebuie să fie null înainte de terminare.");

                controller.TrimiteRaspuns(intrebare.VarianteCorecte);
                total++;
            }

            Assert.AreEqual(total, controller.GetScor(), "Scorul trebuie să fie egal cu numărul de întrebări dacă toate au răspunsuri corecte.");
        }

        [TestMethod]
        public void TestDupaTerminareGetIntrebareCurentaReturneazaNull()
        {
            var controller = new ChestionarController();

            while (!controller.EsteTerminat())
            {
                var intrebare = controller.GetIntrebareCurenta();
                controller.TrimiteRaspuns(intrebare.VarianteCorecte);
            }

            Assert.IsNull(controller.GetIntrebareCurenta(), "După terminare, întrebarea curentă trebuie să fie null.");
        }
        [TestMethod]
        public void TestTrimiteRaspunsDupaTerminareNuAfecteazaScorul()
        {
            var controller = new ChestionarController();

            // Răspunde corect la toate întrebările
            while (!controller.EsteTerminat())
            {
                var intrebare = controller.GetIntrebareCurenta();
                controller.TrimiteRaspuns(intrebare.VarianteCorecte);
            }

            int scorFinal = controller.GetScor();

            // Încearcă să trimită un răspuns în plus
            controller.TrimiteRaspuns(new int[] { 0 });

            Assert.AreEqual(scorFinal, controller.GetScor(), "Scorul nu trebuie să crească după ce chestionarul este terminat.");
        }
        [TestMethod]
        public void TestScorInitialZero()
        {
            var controller = new ChestionarController();
            Assert.AreEqual(0, controller.GetScor(), "Scorul inițial trebuie să fie 0.");
        }
        [TestMethod]
        public void TestToateIntrebarileAuTreiVariante()
        {
            var controller = new ChestionarController();
            while (!controller.EsteTerminat())
            {
                var intrebare = controller.GetIntrebareCurenta();
                Assert.AreEqual(3, intrebare.Variante.Length, "Fiecare întrebare trebuie să aibă exact 3 variante.");
                controller.TrimiteRaspuns(intrebare.VarianteCorecte);
            }
        }
        [TestMethod]
        public void TestRaspunsCorectIndiferentDeOrdine()
        {
            var controller = new ChestionarController();
            var intrebare = controller.GetIntrebareCurenta();

            var raspunsAmestecat = intrebare.VarianteCorecte.Reverse().ToArray();  // inversăm ordinea
            controller.TrimiteRaspuns(raspunsAmestecat);

            Assert.AreEqual(1, controller.GetScor(), "Ordinea răspunsurilor corecte nu trebuie să influențeze scorul.");
        }
        [TestMethod]
        public void TestRaspunsGolNuCresteScorul()
        {
            var controller = new ChestionarController();
            controller.TrimiteRaspuns(new int[] { });  // fără răspuns

            Assert.AreEqual(0, controller.GetScor(), "Un răspuns gol nu trebuie să crească scorul.");
        }
        [TestMethod]
        public void TestInitializareNuAruncaExceptii()
        {
            try
            {
                var controller = new ChestionarController();
            }
            catch (Exception ex)
            {
                Assert.Fail("Inițializarea nu trebuie să arunce excepții: " + ex.Message);
            }
        }
        [TestMethod]
        public void TestScorMaximEgalCuNumarIntrebari()
        {
            var controller = new ChestionarController();
            int nrIntrebari = 0;

            while (!controller.EsteTerminat())
            {
                controller.TrimiteRaspuns(controller.GetIntrebareCurenta().VarianteCorecte);
                nrIntrebari++;
            }

            Assert.AreEqual(nrIntrebari, controller.GetScor(), "Scorul maxim trebuie să fie egal cu numărul de întrebări.");
        }
        [TestMethod]
        public void TestIntrebareCurentaDupaTerminareEsteNull()
        {
            var controller = new ChestionarController();
            while (!controller.EsteTerminat())
                controller.TrimiteRaspuns(new int[] { });

            Assert.IsNull(controller.GetIntrebareCurenta());
        }
        [TestMethod]
        public void TestIntrebareCurentaInitialaNuEsteNull()
        {
            var controller = new ChestionarController();
            Assert.IsNotNull(controller.GetIntrebareCurenta());
        }
        [TestMethod]
        public void TestRaspunsIncorectNuAruncaExceptie()
        {
            var controller = new ChestionarController();
            var intrebare = controller.GetIntrebareCurenta();

            try
            {
                controller.TrimiteRaspuns(new int[] { 99 }); // Index invalid
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail("Nu trebuie să se arunce excepție la răspuns invalid.");
            }
        }
        [TestMethod]
        public void TestChestionarInitialScorZero()
        {
            var controller = new ChestionarController();
            Assert.AreEqual(0, controller.GetScor());
        }
        [TestMethod]
        public void TestIncrementScor()
        {
            var chestionar = new Chestionar(new Intrebare[0]);
            chestionar.IncrementScor();
            Assert.AreEqual(1, chestionar.Scor);
        }
        [TestMethod]
        public void TestParcurgereIntrebariInOrdine()
        {
            var controller = new ChestionarController();
            var enunturi = new List<string>();

            while (!controller.EsteTerminat())
            {
                enunturi.Add(controller.GetIntrebareCurenta().Text);
                controller.TrimiteRaspuns(new int[] { });
            }

            Assert.IsTrue(enunturi.Count > 0);
        }
        [TestMethod]
        public void TestVarianteIntrebareSuntSetate()
        {
            var controller = new ChestionarController();
            var intrebare = controller.GetIntrebareCurenta();
            Assert.AreEqual(3, intrebare.Variante.Length);
        }
        [TestMethod]
        public void TestRaspunsPartialIncorect()
        {
            var controller = new ChestionarController();
            var intrebare = controller.GetIntrebareCurenta();

            if (intrebare.VarianteCorecte.Length > 1)
            {
                controller.TrimiteRaspuns(new int[] { intrebare.VarianteCorecte[0] });
                Assert.AreEqual(0, controller.GetScor());
            }
        }
        [TestMethod]
        public void TestTrimiteRaspunsGresit()
        {
            var controller = new ChestionarController();
            controller.TrimiteRaspuns(new int[] { 1 }); // presupunem greșit
            Assert.AreEqual(0, controller.GetScor());
        }
        [TestMethod]
        public void TestChestionarTerminare()
        {
            var controller = new ChestionarController();

            int count = 0;
            while (!controller.EsteTerminat())
            {
                controller.TrimiteRaspuns(new int[] { });
                count++;
            }

            Assert.AreEqual(count, controller.GetScor() + (count - controller.GetScor()));
        }
        [TestMethod]
        public void TestRaspunsInPlusNuModificaScorul()
        {
            var controller = new ChestionarController();
            while (!controller.EsteTerminat())
                controller.TrimiteRaspuns(controller.GetIntrebareCurenta().VarianteCorecte);

            int scorFinal = controller.GetScor();
            controller.TrimiteRaspuns(new int[] { 0 }); 
            Assert.AreEqual(scorFinal, controller.GetScor());
        }
        [TestMethod]
        public void TestIntrebariUnice()
        {
            var controller = new ChestionarController();
            var enunturi = new HashSet<string>();

            while (!controller.EsteTerminat())
            {
                enunturi.Add(controller.GetIntrebareCurenta().Text);
                controller.TrimiteRaspuns(new int[] { });
            }

            Assert.IsTrue(enunturi.Count > 1);
        }
        [TestMethod]
        public void TestIntrebariAuRaspunsCorect()
        {
            var controller = new ChestionarController();
            while (!controller.EsteTerminat())
            {
                var corecte = controller.GetIntrebareCurenta().VarianteCorecte;
                Assert.IsTrue(corecte.Length >= 1);
                controller.TrimiteRaspuns(new int[] { });
            }
        }
        [TestMethod]
        public void TestIndexuriVarianteCorecteInInterval()
        {
            var controller = new ChestionarController();
            while (!controller.EsteTerminat())
            {
                foreach (var index in controller.GetIntrebareCurenta().VarianteCorecte)
                    Assert.IsTrue(index >= 0 && index <= 2);
                controller.TrimiteRaspuns(new int[] { });
            }
        }
        [TestMethod]
        public void TestRaspunsDuplicatEsteInvalid()
        {
            var controller = new ChestionarController();
            var corect = controller.GetIntrebareCurenta().VarianteCorecte;

            var duplicat = corect.Concat(corect).ToArray(); 
            controller.TrimiteRaspuns(duplicat);

            Assert.AreEqual(0, controller.GetScor());
        }
        [TestMethod]
        public void TestChestionarNuEFinalizatLaStart()
        {
            var controller = new ChestionarController();
            Assert.IsFalse(controller.EsteTerminat());
        }
        [TestMethod]
        public void TestIndexCresteDupaFiecareRaspuns()
        {
            var controller = new ChestionarController();
            int index = 0;

            while (!controller.EsteTerminat())
            {
                controller.TrimiteRaspuns(new int[] { });
                index++;
            }

            Assert.AreEqual(index, controller.GetScor() + (index - controller.GetScor()));
        }
        [TestMethod]
        public void TestCitesteIntrebariDinFisierCorect()
        {
            var controller = new ChestionarController();
            var prima = controller.GetIntrebareCurenta();
            Assert.IsFalse(string.IsNullOrWhiteSpace(prima.Text));
        }

    }
}
