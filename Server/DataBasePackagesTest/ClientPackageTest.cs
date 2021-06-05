using DataBasePackages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBasePackagesTest
{
    class ClientPackageTest
    {
        DateTime ispravanDatum;

        [SetUp]
        public void SetUp()
        {
            ispravanDatum = new DateTime(2020, 10, 28, 22, 45, 59);
        }

        //ISPRAVAN TO STRING
        [Test]
        [TestCase("59/45/22/28/10/2020-VOJVODINA-323.23")]
        public void ClientPackage_ToString_Ispravan(string tekst)
        {
            ClientPackage cpctrl = new ClientPackage();
            cpctrl.Datum = ispravanDatum;
            cpctrl.Region = "VOJVODINA";
            cpctrl.Potrosnja = 323.23;

            Assert.AreEqual(cpctrl.ToString(), tekst);
        }
        //ISPRAVAN FROM STRING
        [Test]
        [TestCase("59/45/22/28/10/2020-VOJVODINA-323.23")]
        public void ClientPackage_FromString_Ispravan(string tekst)
        {
            ClientPackage cpctrl = new ClientPackage();
            cpctrl.Datum = ispravanDatum;
            cpctrl.Region = "VOJVODINA";
            cpctrl.Potrosnja = 323.23;
            ClientPackage cptest = new ClientPackage();
            cptest.FromString(tekst);
            Assert.AreEqual(cpctrl.Datum, cptest.Datum);
            Assert.AreEqual(cpctrl.Region, cptest.Region);
            Assert.AreEqual(cpctrl.Potrosnja, cptest.Potrosnja);
        }
        //NEISPRAVAN FROM STRING - NE VALJA DATUM - KONVERZIJA
        [Test]
        [TestCase("59/aa/bb/28/10/2020-VOJVODINA-323.23")]
        public void ClientPackage_FromString_Neispravan_DatumKonverzija(string tekst)
        {
            Assert.Throws<ArgumentNullException>(() => {
                ClientPackage cptest = new ClientPackage();
                cptest.FromString(tekst);
            });
        }
        //NEISPRAVAN FROM STRING - NE VALJA DATUM - VREDNOSTI
        [Test]
        [TestCase("59/99/99/28/10/2020-VOJVODINA-323.23")]
        public void ClientPackage_FromString_Neispravan_DatumVrednosti(string tekst)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                ClientPackage cptest = new ClientPackage();
                cptest.FromString(tekst);
            });
        }
        //NEISPRAVAN FROM STRING - NE VALJA VREDNOST - KONVERZIJA
        [Test]
        [TestCase("59/aa/bb/28/10/2020-VOJVODINA-ababab")]
        public void ClientPackage_FromString_Neispravan_VrednostKonverzija(string tekst)
        {
            Assert.Throws<ArgumentNullException>(() => {
                ClientPackage cptest = new ClientPackage();
                cptest.FromString(tekst);
            });
        }
        //NEISPRAVAN FROM STRING - NE VALJA VRSTA - KONVERZIJA
        [Test]
        [TestCase("59/aa/bb/28/10/2020-  -ababab")]
        public void ClientPackage_FromString_Neispravan_VrstaKonverzija(string tekst)
        {
            Assert.Throws<ArgumentNullException>(() => {
                ClientPackage cptest = new ClientPackage();
                cptest.FromString(tekst);
            });
        }
    }
}
