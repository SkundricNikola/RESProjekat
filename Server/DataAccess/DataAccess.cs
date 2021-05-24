﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class DataAccess
    { 
        static void Main(string[] args)
        {
            DateTime datumuporuci;
            int sekunde = 0, minuti = 0, sati = 0, dani = 0, meseci = 0, godine = 0;
            string region = "";
            int vrstaporuke;
            double vrednostpotrosnje;
            TcpListener server = null;
            try
            {
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);
                // Start listening for client requests.
                server.Start();
                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;
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

                        string[] parametri = data.Split(';');
                        if(Int32.TryParse(parametri[0],out vrstaporuke) == false)
                        {
                            continue;
                        }
                        else if(vrstaporuke == 1)
                        {
                            region = parametri[1];
                            string[] datumpodaci = parametri[2].Split('/');
                            Int32.TryParse(datumpodaci[0], out sekunde);
                            Int32.TryParse(datumpodaci[1], out minuti);
                            Int32.TryParse(datumpodaci[2], out sati);
                            Int32.TryParse(datumpodaci[3], out dani);
                            Int32.TryParse(datumpodaci[4], out meseci);
                            Int32.TryParse(datumpodaci[0], out godine);
                            datumuporuci = new DateTime(godine, meseci, dani, sati, minuti, sekunde);
                            vrednostpotrosnje = Double.Parse(parametri[3]);
                            //PUNJENJE IZVUCENIH PODATAKA U FORMU
                            RespondToMessageOne();
                        }
                        else
                        {
                            RespondToMessageTwo();
                        }
 
                        // Send back a response.
 //                       stream.Write(msg, 0, msg.Length);
 //                       Console.WriteLine("Sent: {0}", data);
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
                server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }

        private static void RespondToMessageTwo()
        {
            //TO DO
            throw new NotImplementedException();
        }

        private static void RespondToMessageOne()
        {
            //TO DO
            throw new NotImplementedException();
        }
    }
}