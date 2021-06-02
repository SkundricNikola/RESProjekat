using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Client
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the client application, you will be connected shortly...");
            Informer i = new Informer();
            Int32 port = 13000;
            TcpClient client = new TcpClient("127.0.0.1", port);
            NetworkStream stream = client.GetStream();
            string poruka = "";
            while (true)
            {
                poruka = i.AskClient();
                if (poruka == "exit")
                {
                    break;
                }
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(poruka);
                stream.Write(data, 0, data.Length);

                Console.WriteLine("\n----------------------------------------------------------\n");
                Console.WriteLine("----------------PORUKA USPESNO POSLATA!-------------------");
                Console.WriteLine("\n----------------------------------------------------------\n");
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                //ODGOVOR NA PRVU PORUKU
                if(responseData == "WRITTEN")
                {
                    i.PrvaPorukaUspesna();
                }
                else if(responseData == "NOTWRITTEN")
                {
                    i.PrvaPorukaNeuspesna();
                }
                else 
                {
                    string[] odgovor = responseData.Split(';');
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.WriteLine("");
                    Console.WriteLine("---------------------------------------------------------------------");

                    Console.WriteLine("---------------------------------------------------------------------");
                }
            }
            stream.Close();
            client.Close();
            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}
