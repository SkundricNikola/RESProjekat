using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataBase
{
    class DataBaseHandler
    {
        static void Main(string[] args)
        {
            var th1 = new Thread(() => DataAccessCommunication.ReceiveMessage());
            th1.IsBackground = true;
            th1.Start();

        }

        void CreateNewDataBaseElement(string format)
        {

        }

        void UpdateDataBaseElement(string format, int id)
        {

        }

        void ReadDataBaseElement(string format, int code)
        {

        }

        void DeleteDataBaseElement(string format, int code)
        {

        }
    }
}
