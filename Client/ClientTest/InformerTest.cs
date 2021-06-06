using Client;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest
{
    [TestFixture]
    class InformerTest
    {
        [Test]

        public void Provera_PrvaPorukaUspesna_Ispravno()
        {
            Informer i = new Informer();
            string teststring = string.Format("-----------------------------------------------------{0}" +
                                             "\tPODACI USPESNO UPISANI U BAZU PODATAKA{0}" +
                                             "-----------------------------------------------------\r\n", Environment.NewLine);
            var output = new StringWriter();
            Console.SetOut(output);
            i.PrvaPorukaUspesna();
            Assert.That(output.ToString(), Is.EqualTo(teststring));
        }
        [Test]
        public void Provera_PrvaPorukaNeuspesna_Ispravno()
        {
            Informer i = new Informer();
            string teststring = string.Format("-----------------------------------------------------{0}" +
                                             "\tUPISIVANJE PODATAKA U BAZU NIJE USPELO{0}" +
                                             "\tMOLIMO POKUSAJTE PONOVO{0}" + 
                                             "-----------------------------------------------------\r\n", Environment.NewLine);
            var output = new StringWriter();
            Console.SetOut(output);
            i.PrvaPorukaNeuspesna();
            Assert.That(output.ToString(), Is.EqualTo(teststring));
        }
        [Test]
        public void Provera_ShowOptions_Ispravno()
        {
            Informer i = new Informer();
            string teststring = string.Format("Please type in the number of the operation you would like to conduct:{0}" +
                                             "1 - Write the usage of electric energy for certain region{0}" +
                                             "2 - Print all the function calculations{0}" +
                                             "IZADJI - Napustanje programa\r\n", Environment.NewLine);
            var output = new StringWriter();
            Console.SetOut(output);
            i.ShowOptions();
            Assert.That(output.ToString(), Is.EqualTo(teststring));
        }

        [Test]
        public void Provera_AskClient_UnosPodataka_DobarDijalog()
        {
            Informer i = new Informer();
            string teststring = string.Format("1;VOJVODINA;35/25/15/24/8/2020;326.15");
            var input = new StringReader("1\nVOJVODINA\n35/25/15/24/8/2020\n326.15");
            Console.SetIn(input);
            Assert.AreEqual(teststring,i.AskClient());
        }

        [Test]
        public void Provera_AskClient_UnosPodataka_Drugi_DobarDijalog()
        {
            Informer i = new Informer();
            string teststring = string.Format("2;25/2/2020");
            var input = new StringReader("2\n25/2/2020");
            Console.SetIn(input);
            Assert.AreEqual(teststring, i.AskClient());
        }
        [Test]
        public void Provera_AskClient_UnosPodataka_Drugi_LosFormatDatuma()
        {
            Informer i = new Informer();
            var input = new StringReader("2\n25/2020");
            Console.SetIn(input);
            Assert.Throws<ArgumentException>(() => i.AskClient());
        }
        [Test]
        public void Provera_AskClient_UnosPodataka_Drugi_NemogucaKonverzijaDatuma()
        {
            Informer i = new Informer();
            var input = new StringReader("2\n25/ssx/2020");
            Console.SetIn(input);
            Assert.Throws<ArgumentException>(() => i.AskClient());
        }
        [Test]
        public void Provera_AskClient_UnosPodataka_Drugi_BrojeviVanOpsegaZaDatum()
        {
            Informer i = new Informer();
            var input = new StringReader("2\n25/99/2020");
            Console.SetIn(input);
            Assert.Throws<ArgumentOutOfRangeException>(() => i.AskClient());
        }

        [Test]
        public void Provera_AskClient_UnosPodataka_LosRegion()
        {
            Informer i = new Informer();
            string teststring = string.Format("1;VOJVODINA;35/25/15/24/8/2020;326.15");
            var input = new StringReader("1\n\n35/25/15/24/8/2020\n326.15");
            Console.SetIn(input);
            Assert.Catch(() => i.AskClient(), "Neispravna vrednost za region");
        }
        [Test]
        public void Provera_AskClient_UnosPodataka_LosDatum_NemogucaKonverzija()
        {
            Informer i = new Informer();
            var input = new StringReader("1\nVOJVODINA\nDA/NE/MOZDA/24/8/2020\n326.15");
            Console.SetIn(input);
            Assert.Catch(() => i.AskClient(), "Pogresan format! Nije moguce pretvoriti neki od parametara u broj. ");
        }
        [Test]
        public void Provera_AskClient_UnosPodataka_LosDatum_PogresanBrojParametara()
        {
            Informer i = new Informer();
            var input = new StringReader("1\nVOJVODINA\n/24/8/2020\n326.15");
            Console.SetIn(input);
            Assert.Catch(() => i.AskClient(), "Pogresan format! Niste uneli neki parametar ili ste ih uneli previse.");
        }
        [Test]
        public void Provera_AskClient_UnosPodataka_LosDatum_BrojVanOpsega()
        {
            Informer i = new Informer();
            var input = new StringReader("1\nVOJVODINA\n78/99/23/24/8/2020\n326.15");
            Console.SetIn(input);
            Assert.Catch(() => i.AskClient(), "Pogresan format! Neki od podataka ima vrednost vecu/manju nego sto bi smeo da ima.");
        }
        [Test]
        public void Provera_AskClient_UnosPodataka_LosaIzmerenaVrednost()
        {
            Informer i = new Informer();
            var input = new StringReader("1\nVOJVODINA\n35/25/15/24/8/2020\na8b4");
            Console.SetIn(input);
            Assert.Throws<FormatException>(() => { i.AskClient(); }, "Pogresan format! Nije unet broj!");
        }

        /*[Test]
        public void Provera_PrintList_Obrada_DobriPodaci()
        {
            Informer i = new Informer();
            var output = new StringWriter();
            Console.SetOut(output);

            // SEKUND/MINUT/SAT/DAN/MESEC/GODINA-REZULTAT-VRSTA
            //{0}/{1}/{2}/{3}/{4}/{5}-{6}-{7}
            string lista = "06/09/22/15/11/2020-250.12-MINIMALNI;45/55/15/25/09/2020-355.55-MAKSIMALNI";
           
            string teststring = string.Format("---------------------------------------------------------------------{0}" +
                                              "DATUM\tVREDNOST PRORACUNA\tVRSTA PRORACUNA{0}" +
                                              "---------------------------------------------------------------------{0}" +
                                              "11/15/2020 10:09:06 PM\t250.12\tMINIMALNI{0}" +
                                              "9/25/2020 3:55:45 PM\t355.55\tMAKSIMALNI{0}" +
                                              "---------------------------------------------------------------------\r\n", Environment.NewLine);
            i.PrintList(lista);
            Assert.That(output.ToString(), Is.EqualTo(teststring));
        }
        [Test]
        public void Provera_PrintList_Obrada_LosFormatPoruke_Datum()
        {
            Informer i = new Informer();
            var output = new StringWriter();
            Console.SetOut(output);

            // SEKUND/MINUT/SAT/DAN/MESEC/GODINA-REZULTAT-VRSTA
            //{0}/{1}/{2}/{3}/{4}/{5}-{6}-{7}
            string lista = "06/a9/22/15/11/2020-250.12-MINIMALNI;55/c15/25/09/2020-355.55-MAKSIMALNI";

            string teststring = string.Format("---------------------------------------------------------------------{0}" +
                                              "DATUM\tVREDNOST PRORACUNA\tVRSTA PRORACUNA{0}" +
                                              "---------------------------------------------------------------------{0}" +
                                              "11/15/2020 10:09:06 PM\t250.12\tMINIMALNI{0}" +
                                              "9/25/2020 3:55:45 PM\t355.55\tMAKSIMALNI{0}" +
                                              "---------------------------------------------------------------------\r\n", Environment.NewLine);
            Assert.Catch(() => i.PrintList(lista));
        }
        public void Provera_PrintList_Obrada_LosFormatPoruke_Datum_FaliParametara()
        {
            Informer i = new Informer();
            var output = new StringWriter();
            Console.SetOut(output);

            // SEKUND/MINUT/SAT/DAN/MESEC/GODINA-REZULTAT-VRSTA
            //{0}/{1}/{2}/{3}/{4}/{5}-{6}-{7}
            string lista = "06/a9/22/15/11/2020-250.12-MINIMALNI;55/c15/09/2020-355.55-MAKSIMALNI";

            string teststring = string.Format("---------------------------------------------------------------------{0}" +
                                              "DATUM\tVREDNOST PRORACUNA\tVRSTA PRORACUNA{0}" +
                                              "---------------------------------------------------------------------{0}" +
                                              "11/15/2020 10:09:06 PM\t250.12\tMINIMALNI{0}" +
                                              "9/25/2020 3:55:45 PM\t355.55\tMAKSIMALNI{0}" +
                                              "---------------------------------------------------------------------\r\n", Environment.NewLine);
            Assert.Catch(() => i.PrintList(lista));
        }

        [Test]
        public void Provera_PrintList_Obrada_LosFormatPoruke_Rezultat()
        {
            Informer i = new Informer();
            var output = new StringWriter();
            Console.SetOut(output);
            // SEKUND/MINUT/SAT/DAN/MESEC/GODINA-REZULTAT-VRSTA
            //{0}/{1}/{2}/{3}/{4}/{5}-{6}-{7}
            string lista = "06/a9/22/15/11/2020-250.12-MINIMALNI;55/c15/14/25/09/2020-ak47.74-MAKSIMALNI";
           Assert.Throws<FormatException>(() => i.PrintList(lista),"Format unetog broja je neispravan, nemoguce ga je konvertovati");
        }
        [Test]
        public void Provera_PrintList_Obrada_LosFormatPoruke_Vrsta()
        {
            Informer i = new Informer();
            var output = new StringWriter();
            Console.SetOut(output);

            // SEKUND/MINUT/SAT/DAN/MESEC/GODINA-REZULTAT-VRSTA
            //{0}/{1}/{2}/{3}/{4}/{5}-{6}-{7}
            string lista = "06/a9/22/15/11/2020-250.12-MINIMALNI;55/15/9/25/09/2020-355.55-MALOSUTRA";
            Assert.Throws<FormatException>(() => i.PrintList(lista), "Format unete vrste merenja je neispravan!");
        }*/
    }
}
