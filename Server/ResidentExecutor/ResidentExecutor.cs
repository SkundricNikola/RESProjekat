using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml;
using System.IO;

namespace ResidentExecutor
{
    class ResidentExecutor
    {
        static void Main(string[] args)
        {
            //TRENUTNO JE VREME POZIVANJA ZAKUCANO
            TriggerFunction tf = new TriggerFunction();
            TcpClient clientCalc = new TcpClient("127.0.0.1", 10003);
            NetworkStream streamCalc = clientCalc.GetStream();
            TimeSpan triggerTime = new TimeSpan();
            //TREBA IMPLEMENTIRATI LOGIKU ZA MENJANJE OVIH PARAMETARA, ZAKOMENTARISATI/OBRISATI DODELU NAKON STO SE ZAVRSI
            Queue<string> funkcije = new Queue<string>();
            string fileName = "Res_Exe.xml";
            string fullPath;
            string str = "";
            int vreme;

            fullPath = Path.GetFullPath(fileName);
            
            while(true)
            {
                if(funkcije.Count == 0)
                {
                    //citanje iz xml datoteke Res_Exe i dodavanje indentifikatora u queue funkcije i triggerTime staviri na procitanu vrednost
                    XmlDocument doc = new XmlDocument();
                    doc.Load(fullPath);

                    XmlNode node = doc.DocumentElement.SelectSingleNode("/resident_info/time");
                    Int32.TryParse(node.InnerText, out vreme);
                    triggerTime = new TimeSpan(0, 0, vreme);

                    node = doc.DocumentElement.SelectSingleNode("/resident_info/functions");
                    foreach(XmlNode nod in doc.DocumentElement.ChildNodes)
                    {
                        funkcije.Enqueue(nod.InnerText);
                    }
                }

                str = funkcije.Dequeue();

                tf.SendTriggerFunction(streamCalc, str);

                //Verovatno treba dodati neku vrstu ACK potvrde od Calculation Handlera
                Thread.Sleep(triggerTime);
                
            }
            streamCalc.Close();
            clientCalc.Close();
        }
    }
}
