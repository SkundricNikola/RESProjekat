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
        public void Provera_PrintList_Obrada()
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
    }
}
