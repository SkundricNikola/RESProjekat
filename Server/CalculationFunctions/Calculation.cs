using DataBasePackages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalculationFunctions
{
    public class Calculation
    {
        public List<ClientPackage> RetrieveCalculations(bool slanje_paketa,CalculationPackage packetOut)
        {
            List<ClientPackage> kalkulacije = new List<ClientPackage>();
            Int32 portDataAccess = 10010;
            DataAccessCommunication dac = new DataAccessCommunication(portDataAccess, IPAddress.Parse("127.0.0.1"));
            DateTime datum = new DateTime();
            var th1 = new Thread(() => DataAccessCommunication.SendMessage(ref kalkulacije, datum,slanje_paketa,packetOut));
            th1.IsBackground = true;
            th1.Start();
            return kalkulacije;
        }
        static void Main(string[] args)
        {
            Calculation c = new Calculation();
            TcpListener server = null;
            Int32 port = 10003;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            try
            {
                server = new TcpListener(localAddr, port);
                // Start listening for client requests.
                server.Start();
                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;
                bool trigger = false;
                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");
                    // Perform a blocking call to accept requests.
                    TcpClient client = server.AcceptTcpClient();
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
                        trigger = true;
                        if (trigger)
                        {
                            //primamo string formatiran unutar override metode toString klase ClientPackage
                            List<double> listaMerenja = new List<double>();
                            CalculationPackage paketOut = new CalculationPackage();
                            List<ClientPackage> unosi_baza = c.RetrieveCalculations(false, paketOut);

                            List<string> unosi = new List<string>(unosi_baza.Count);
                            foreach (ClientPackage cp in unosi_baza)
                            {
                                unosi.Add(cp.ToString());
                            }

                            int k = 0;
                            while (listaMerenja.Count != unosi.Count)
                            {
                                string[] delovi = unosi[k].Split('-');
                                //radimo sa DateTime delom
                                string[] datumpodaci = delovi[0].Split('/');
                                int sekunde = 0, minuti = 0, sati = 0, dani = 0, meseci = 0, godine = 0;
                                Int32.TryParse(datumpodaci[0], out sekunde);
                                Int32.TryParse(datumpodaci[1], out minuti);
                                Int32.TryParse(datumpodaci[2], out sati);
                                Int32.TryParse(datumpodaci[3], out dani);
                                Int32.TryParse(datumpodaci[4], out meseci);
                                Int32.TryParse(datumpodaci[0], out godine);
                                DateTime datum = new DateTime(godine, meseci, dani, sati, minuti, sekunde);
                                paketOut.VremeProracuna = datum;
                                //radimo sa vrednosti potrosnje
                                double potrosnja = 0;
                                Double.TryParse(delovi[2], out potrosnja);
                                listaMerenja.Add(potrosnja);
                                paketOut.PosVreme = DateTime.Now;
                                if (data.Equals("min"))
                                {
                                    paketOut.Rezultat = listaMerenja.Min();
                                    paketOut.VrstaProracuna = VrstaProracuna.MINIMALNI;
                                }
                                //Maximum
                                if (data.Equals("max"))
                                {
                                    paketOut.Rezultat = listaMerenja.Max();
                                    paketOut.VrstaProracuna = VrstaProracuna.MAKSIMALNI;
                                }
                                //Prosek
                                if (data.Equals("average"))
                                {
                                    paketOut.Rezultat = listaMerenja.Average();
                                    paketOut.VrstaProracuna = VrstaProracuna.PROSECNI;
                                }
                                k++;
                                if (k == unosi.Count) { k = 0; }
                            }
                            //Slanje paketOut-a na upis u bazu podataka
                            unosi_baza = c.RetrieveCalculations(true, paketOut);
                            trigger = false;
                        }
                    
                    }
                    
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
        
    }  
}
