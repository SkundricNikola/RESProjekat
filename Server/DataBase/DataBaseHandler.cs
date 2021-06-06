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
            
                var th1 = new Thread(() => DataAccessCommunication.ReceiveMessage());
                //th1.IsBackground = true;
                th1.Start();
            
        }
    }
}
