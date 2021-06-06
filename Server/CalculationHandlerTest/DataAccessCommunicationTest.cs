using CalculationFunctions;
using DataBase;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CalculationHandlerTest
{
    [TestFixture]
    class DataAccessCommunicationTest
    {
        DateTime ispravanDatum;
        IPAddress properip;
        [SetUp]
        public void SetUp()
        {
            ispravanDatum = new DateTime(2020, 10, 28, 22, 45, 59);
            DataAccessCommunication.IPAddress = IPAddress.Parse("127.0.0.1");
            DataAccessCommunication.Port = 13000;
            properip = IPAddress.Parse("127.0.0.1");
        }
        [Test]
        [TestCase("2020/10/28/22/45/59")]
        public void DataAccessCommunication_FormatirajDatum_Ispravan(string teststring)
        {
            Assert.AreEqual(teststring, DataAccessCommunication.FormatirajDatum(ispravanDatum));
        }
        [Test]
        public void DataAccessCommunication_FormatirajDatum_Neispravan_OutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => DataAccessCommunication.FormatirajDatum(new DateTime(99, 99, 99, 99, 99, 99)));
        }
        [Test]
        public void DataAccessCommunication_Konstruktor_Ispravan()
        {
            Assert.AreEqual(DataAccessCommunication.Port, 13000);
            Assert.AreEqual(DataAccessCommunication.IPAddress, IPAddress.Parse("127.0.0.1"));
        }
        [Test]
        public void DataAccessCommunication_Konstruktor_Neispravan_PortNegativan()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { new DataAccessCommunication(-295, properip); });
        }
        [Test]
        public void DataAccessCommunication_SendMessage_Ispravan_SlanjePaketaDa()
        {

        }
        [Test]
        public void DataAccessCommunication_SendMessage_Ispravan_SlanjePaketaNe()
        {

        }
        [Test]
        public void DataAccessCommunication_SendMessage_Neispravan_NemaKonekcije()
        {
            Assert.Throws<SocketException>(() => { DataBaseHandler. });
        }
    }
}
