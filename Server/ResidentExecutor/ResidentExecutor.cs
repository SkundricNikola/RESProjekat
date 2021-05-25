﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ResidentExecutor
{
    class ResidentExecutor
    {
        static void Main(string[] args)
        {
            //TRENUTNO JE VREME POZIVANJA ZAKUCANO
            TriggerFunction tf = new TriggerFunction();
            TimeSpan triggerTime = new TimeSpan(0, 0, 10);
            TcpClient clientCalc = new TcpClient("127.0.0.1", 10003);
            NetworkStream streamCalc = clientCalc.GetStream();
            //TREBA IMPLEMENTIRATI LOGIKU ZA MENJANJE OVIH PARAMETARA, ZAKOMENTARISATI/OBRISATI DODELU NAKON STO SE ZAVRSI
            Queue<string> funkcije = new Queue<string>();
            string str = "";
            while(true)
            {
                if(funkcije.Count == 0)
                {
                    //citanje iz xml datoteke Res_Exe i dodavanje indentifikatora u queue funkcije i triggerTime staviri na procitanu vrednost
                }

                str = funkcije.Dequeue();

                tf.SendTriggerFunction(streamCalc, str);

                Thread.Sleep(triggerTime);

                
            }
            streamCalc.Close();
            clientCalc.Close();
        }
    }
}
