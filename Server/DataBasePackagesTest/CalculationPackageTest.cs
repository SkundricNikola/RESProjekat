using DataBasePackages;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBasePackagesTest
{
    [TestFixture]
    public class CalculationPackageTest
    {
        DateTime ispravanDatum;

        [SetUp]
        public void SetUp()
        {
            ispravanDatum = new DateTime(2020, 10, 28, 22, 45, 59);
        }
        [Test]
        [TestCase(255.43,VrstaProracuna.MAKSIMALNI)]
        [TestCase(323.15,VrstaProracuna.PROSECNI)]
        public void CalculationPackage_Konstruktor_DobriParametri(double vrednost, VrstaProracuna vrsta)
        {
            CalculationPackage cp = new CalculationPackage(ispravanDatum, vrednost, vrsta);
            Assert.AreEqual(vrednost, cp.Rezultat);
            Assert.AreEqual(vrsta, cp.VrstaProracuna);
            Assert.AreEqual(ispravanDatum, cp.VremeProracuna);
        }
        [Test]
        [TestCase(255.43, VrstaProracuna.MAKSIMALNI)]
        public void CalculationPackage_Konstruktor_NeispravanDatum(double vrednost, VrstaProracuna vrsta)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { 
                    CalculationPackage cp = new CalculationPackage(new DateTime(99, 99, 99, 99, 99, 99), vrednost, vrsta); 
            });
        }
        [Test]
        [TestCase(-235.23, VrstaProracuna.MAKSIMALNI)]
        public void CalculationPackage_Konstruktor_NeispravnaVrednost(double vrednost, VrstaProracuna vrsta)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                CalculationPackage cp = new CalculationPackage(ispravanDatum, vrednost, vrsta);
            });
        }
        [Test]
        [TestCase(-235.23, 7)]
        public void CalculationPackage_Konstruktor_NeispravnaVrsta(double vrednost, VrstaProracuna vrsta)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                CalculationPackage cp = new CalculationPackage(ispravanDatum, vrednost, vrsta);
            });
        }
        [Test]
        [TestCase("59/45/22/28/10/2020-59/45/22/28/10/2020-255.16-MAKSIMALNI")]
        public void CalculationPackage_FromString_Ispravan(string tekst)
        {
            CalculationPackage cpkontrolni = new CalculationPackage(ispravanDatum, 255.16, VrstaProracuna.MAKSIMALNI);
            cpkontrolni.PosVreme = ispravanDatum;
            CalculationPackage cptest = new CalculationPackage();
            cptest.FromString(tekst);
            Assert.AreEqual(cpkontrolni.PosVreme,cptest.PosVreme);
            Assert.AreEqual(cpkontrolni.VremeProracuna, cptest.VremeProracuna);
            Assert.AreEqual(cpkontrolni.VrstaProracuna, cptest.VrstaProracuna);
            Assert.AreEqual(cpkontrolni.Rezultat, cptest.Rezultat);
        }
        [Test]
        [TestCase("ss/aa/44/35/226/ee-iss/22/13/9/10/11-12-NEODREDJENI")]
        [TestCase("59/45/22/28/10/2020-aa/45/ss/28/10/eeee-255.16-MAKSIMALNI")]
        public void CalculationPackage_FromString_NeispravanDatumFormat(string tekst)
        {
            Assert.Throws<ArgumentNullException>(() => {
                CalculationPackage cp = new CalculationPackage();
                cp.FromString(tekst);
            });
        }
        [Test]
        public void CalculationPackage_FromString_PrazanString()
        {
            Assert.Throws<IndexOutOfRangeException>(() => {
                CalculationPackage cp = new CalculationPackage();
                cp.FromString(" ");
            });
        }

        [Test]
        [TestCase("99/99/99/99/99/9999-15/22/13/9/10/11-12.03-NEODREDJENI")]//NE VALJA PRVI DATUM
        [TestCase("59/45/22/28/10/2020-99/99/9922/28/10/2020-255.16-MAKSIMALNI")]//NE VALJA DRUGI DATUM
        [TestCase("59/45/22/28/10/2020-59/45/22/28/10/2020-255.16-NEPOSTOJECI")]// NE VALJA VRSTA
        [TestCase("59/45/22/28/10/2020-59/45/22/28/10/2020-sasasas-MAKSIMALNI")]//NE VALJA KONVERZIJA VREDNOSTI MERENJA
        public void CalculationPackage_FromString_NeispravanDatumBroj(string tekst)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                CalculationPackage cp = new CalculationPackage();
                cp.FromString(tekst);
            });
        }
        [Test]
        [TestCase("59/45/22/28/10/2020-59/45/22/28/10/2020-255.16-MAKSIMALNI")]
        public void CalculationPackage_ToString_Ispravan(string tekst)
        {
            CalculationPackage cp = new CalculationPackage(ispravanDatum, 255.16, VrstaProracuna.MAKSIMALNI);
            cp.PosVreme = ispravanDatum;
            Assert.AreEqual(tekst, cp.ToString());
        }
    }
}

