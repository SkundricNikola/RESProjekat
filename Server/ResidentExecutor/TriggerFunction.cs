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
        public void SendTriggerFunction(NetworkStream ns,string code)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(string.Format("{0}",code));
            
            ns.Write(data,0,data.Length);
            
        }
    }
}
