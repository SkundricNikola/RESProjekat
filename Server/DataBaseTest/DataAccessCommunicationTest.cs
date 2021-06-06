using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseTest
{
    [TestFixture]
    class DataAccessCommunicationTest
    {
        [Test]
        public void DataBase_Create_Dobar()
        {
            DataBase.DataAccessCommunication dac = new DataBase.DataAccessCommunication();
            Assert.AreEqual(true, dac.CreateNewDataBaseElement("2020/11/25/15/30/45-VOJVODINA-245.15"));
        }
    }
}
