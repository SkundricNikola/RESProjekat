using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ClientTest
{
    [TestFixture]
    public class ClientTest
    {
        [Test]
        public void Testiranje_NeuspesnoPovezivanje()
        {
            Assert.Throws<SocketException>(() => Client.Client.Main(new string[] { }));
        }
    }
}
