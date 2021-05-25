using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ResidentExecutor
{
    class TriggerFunction
    {
        public TriggerFunction()
        {

        }
        public void SendTriggerFunction(NetworkStream ns, bool isactive)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes("+");
            if (isactive == true)
            {
                ns.Write(data,0,data.Length);
            }
            else
            {
                return;
            }
        }
    }
}
