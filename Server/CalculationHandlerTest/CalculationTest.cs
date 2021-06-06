using CalculationFunctions;
using DataBasePackages;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CalculationHandlerTest
{
    [TestFixture]
    public class CalculationTest
    {
        List<ClientPackage> ispravnalista;
        Calculation ch;
        DateTime ispravanDatum;
        CalculationPackage calcPack;
        [SetUp]
        public void SetUp()
        {
            calcPack = new CalculationPackage(ispravanDatum, 245.16, VrstaProracuna.MINIMALNI);
            ispravanDatum = new DateTime(2020, 10, 28, 22, 45, 59);
            ch = new Calculation();
            ClientPackage cp = new ClientPackage();
            cp.Datum = ispravanDatum;
            cp.Potrosnja = 256.36;
            cp.Region = "VOJVODINA";
            ispravnalista.Add(cp);
            cp = new ClientPackage();
            cp.Datum = new DateTime(2020, 6, 15, 15, 59, 59);
            cp.Potrosnja = 3636.36;
            cp.Region = "MACVA";
            ispravnalista.Add(cp);
        }
        [Test]

        public void Calculation_RetrieveCalculations_Ispravan()
        {
            List<ClientPackage> checklist = ch.RetrieveCalculations(true, calcPack);
            Assert.AreEqual(checklist, ispravnalista);
        }
        
    }
}
