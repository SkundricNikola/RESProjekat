using CalculationFunctions;
using DataAccess;
using DataBase;
using DataBasePackages;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalculationHandlerTest
{
    [TestFixture]
    class DataAccessCommunicationTest
    {
        //Thread th1;
        DateTime ispravanDatum;
        IPAddress properip;
        [SetUp]
        public void SetUp()
        {
            ispravanDatum = new DateTime(2020, 10, 28, 22, 45, 59);
            CalculationFunctions.DataAccessCommunication.IPAddress = IPAddress.Parse("127.0.0.1");
            CalculationFunctions.DataAccessCommunication.Port = 13000;
            properip = IPAddress.Parse("127.0.0.1");
        }
        [Test]
        [TestCase("2020/10/28/22/45/59")]
        public void DataAccessCommunication_FormatirajDatum_Ispravan(string teststring)
        {
            Assert.AreEqual(teststring, CalculationFunctions.DataAccessCommunication.FormatirajDatum(ispravanDatum));
        }
        [Test]
        public void DataAccessCommunication_FormatirajDatum_Neispravan_OutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CalculationFunctions.DataAccessCommunication.FormatirajDatum(new DateTime(99, 99, 99, 99, 99, 99)));
        }
        [Test]
        public void DataAccessCommunication_Konstruktor_Ispravan()
        {
            Assert.AreEqual(CalculationFunctions.DataAccessCommunication.Port, 13000);
            Assert.AreEqual(CalculationFunctions.DataAccessCommunication.IPAddress, IPAddress.Parse("127.0.0.1"));
        }
        [Test]
        public void DataAccessCommunication_Konstruktor_Neispravan_PortNegativan()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { new CalculationFunctions.DataAccessCommunication(-295, properip); });
        }
        [Test]
        public void DataAccessCommunication_SendMessage_Ispravan_SlanjePaketaDa()
        {
            List<ClientPackage> lista = new List<ClientPackage>();
            CalculationPackage paket = new CalculationPackage();
            //public static void SendMessage(ref List<ClientPackage> compack,DateTime dat,bool slanje_paketa,CalculationPackage packetOut)
            string teststring = "Poruka uspesno poslata";
            var output = new StringWriter();
            Console.SetOut(output);
            CalculationFunctions.DataAccessCommunication.SendMessage(ref lista, ispravanDatum, true, paket);
            Assert.AreEqual(output.ToString(), teststring);
        }
        [Test]
        public void DataAccessCommunication_SendMessage_Ispravan_SlanjePaketaNe()
        {

        }
    }
}
