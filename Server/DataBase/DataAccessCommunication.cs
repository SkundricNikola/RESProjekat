using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    class DataAccessCommunication
    {
        private static TcpListener dac;
        public DataAccessCommunication()
        {

        }
        public static void ReceiveMessage()
        {
            try
            {
                dac = new TcpListener(IPAddress.Parse("127.0.0.1"), 10011);
                dac.Start();
                Byte[] bytes = new Byte[256];
                String data = null;
                DateTime datumprovere;
                while (true)
                {
                    Console.Write("Waiting for a connection... ");
                    // Perform a blocking call to accept requests.
                    TcpClient client = dac.AcceptTcpClient();
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
                        //DATA ACCESS PITA ZA LISTU TAKO STO SALJE DATUM
                        int godina, mesec, dan, sat, minut, sekund;
                        string[] par = data.Split('/');
                        Int32.TryParse(par[0],out godina);
                        Int32.TryParse(par[1],out mesec);
                        Int32.TryParse(par[2],out dan);
                        Int32.TryParse(par[3],out sat);
                        Int32.TryParse(par[4],out minut);
                        Int32.TryParse(par[5],out sekund);
                        datumprovere = new DateTime(godina, mesec, dan, sat, minut, sekund);
                        /*----------------------------------------------------------
                         * LOGIKA ZA DOBAVLJANJE LISTE, DODATI KAD SE FUNKCIJA ZA CITANJE IZ BAZE IMPLEMENTIRA
                         * TRENUTNO SLEDECA STRANA OCEKUJE ODGOVOR U FORMATU godina/mesec/dan/sat/minut/sekund/rezultat;godina/mesec.....
                         * UKOLIKO SE PROMENI FORMAT LISTE, POTREBNO JE PROMENITI I PARSIRANJE SA DRUGE STRANE
                         ---------------------------------------------------------------------------*/
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
                dac.Stop();
            }
        }
    }
}
