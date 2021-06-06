using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataBase
{
    public class DataBaseHandler
    {
        public static void Main(string[] args)
        {
            
            var th1 = new Thread(() => DataAccessCommunication.ReceiveMessage_t1());
            var th3 = new Thread(() => DataAccessCommunication.ReceiveMessage_t3());
            var th2 = new Thread(() => DataAccessCommunication.ReceiveMessage_t2());

            th1.Start();
            th2.Start();
            th3.Start();

        }
    }
}
