using DataAccess;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessTest
{
    [TestFixture]
    class DataBaseCommunicationTest
    {
        DateTime ispravanDatum;
        [SetUp]
        public void SetUp()
        {
            ispravanDatum = new DateTime(2020, 11, 15, 23, 50, 59);
        }
        [Test]
        public void DataBaseCommunication_FormatirajDatum_Ispravno()
        {
            Assert.AreEqual("2020/11/15/23/50/59", DataBaseCommunication.FormatirajDatum(ispravanDatum));
        }

    }
}
