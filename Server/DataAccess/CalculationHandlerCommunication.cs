using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using DataBasePackages;
using System.Threading;

namespace DataAccess
{
    //SERVER/LISTENER
    class CalculationHandlerCommunication
    {
        static TcpListener clh;
        public CalculationHandlerCommunication()
        {
            clh = null;
        }
        public static void ReceiveMessage()
        {
            try
            {
                DataBaseCommunication dbc = new DataBaseCommunication();
                clh = new TcpListener(IPAddress.Parse("127.0.0.1"), 10010);
                clh.Start();
                Byte[] bytes = new Byte[256];
                String data = null;
                DateTime datum = new DateTime();
                while (true)
                {
                    Console.Write("Waiting for a connection... ");
                    // Perform a blocking call to accept requests.
                    TcpClient client = clh.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    data = null;
                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();
                    int i;
                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);
                        //5/1/2008 6:32:06 PM
                        string[] vreme = data.Split('/');
                        //vreme[0] - godina, vreme[1] - mesec, vreme[2] - dan, vreme[3] - sat, vreme[4] - minut, vreme[5] - sekund;
                        datum = new DateTime(Int32.Parse(vreme[0]), Int32.Parse(vreme[1]), Int32.Parse(vreme[2]), Int32.Parse(vreme[3]), Int32.Parse(vreme[4]), Int32.Parse(vreme[5]));
                        string odgovor = "";
                        //IZDVOJEN DATUM, OVDE TREBA DA SE POZIVA FUNKCIJA KOJA CE DA RETRIEVE LISTU PO OVOM DATUMU
                        var t1 = new Thread(() => DataBaseCommunication.AskForList(datum, ref odgovor, false));
                        t1.Start();
                        //VRACANJE LISTE PRETABANE U STRING
                        bytes = System.Text.Encoding.ASCII.GetBytes(odgovor);
                        stream.Write(bytes, 0, bytes.Length);
                    }
                    // Shutdown and end connection
                    client.Close();
                }


            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                clh.Stop();
            }
        }
    }
}
