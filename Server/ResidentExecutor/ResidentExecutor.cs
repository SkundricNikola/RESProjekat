using System;
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
            TcpClient clientMinimum = new TcpClient("127.0.0.1", 10001);
            TcpClient clientAverage = new TcpClient("127.0.0.1", 10002);
            TcpClient clientMaximum = new TcpClient("127.0.0.1", 10003);
            NetworkStream streamMinimum = clientMinimum.GetStream();
            NetworkStream streamAverage = clientAverage.GetStream();
            NetworkStream streamMaximum = clientMaximum.GetStream();
            bool sendminimum;
            bool sendaverage;
            bool sendmaximum;
            //TREBA IMPLEMENTIRATI LOGIKU ZA MENJANJE OVIH PARAMETARA, ZAKOMENTARISATI/OBRISATI DODELU NAKON STO SE ZAVRSI
            sendminimum = true;
            sendaverage = true;
            sendmaximum = true;
            while(true)
            {
                Thread.Sleep(triggerTime);
                //SEND TRIGGER FUNCTION;
                //USLOVI ZA POKRETANJE SU ZAKOMENTARISANI DOK SE NE SREDI LOGIKA
                //if () {
                    tf.SendTriggerFunction(streamMinimum, sendminimum);
                //}
                //if () {
                    tf.SendTriggerFunction(streamAverage, sendaverage);
                //} 
                //if () {
                    tf.SendTriggerFunction(streamMaximum, sendmaximum);
                //} 
            }
            streamMinimum.Close();
            streamAverage.Close();
            streamMaximum.Close();
            clientMinimum.Close();
            clientAverage.Close();
            clientMaximum.Close();
        }
    }
}
