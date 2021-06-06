using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using DataBasePackages;

namespace CalculationFunctions
{
    public class DataAccessCommunication
    {
        public static string FormatirajDatum(DateTime datum)
        {
            string retval = datum.Year.ToString() + "/" + datum.Month.ToString() + "/" + datum.Day.ToString() + "/" + datum.Hour.ToString() + "/" + datum.Minute.ToString() + "/" + datum.Second.ToString();
            return retval;
        }
        static int port;
        static IPAddress iPAddress;
        static String responseData;
        public DataAccessCommunication(int porta, IPAddress iPAddressa)
        {
            if(porta < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            Port = porta;
            IPAddress = iPAddressa;
        }

        public static string ResponseData { get => responseData; set => responseData = value; }
        public static int Port { get => port; set => port = value; }
        public static IPAddress IPAddress { get => iPAddress; set => iPAddress = value; }

        public static void SendMessage(ref List<ClientPackage> compack,DateTime dat,bool slanje_paketa,CalculationPackage packetOut)
        {
            NetworkStream ns;
            TcpClient client = new TcpClient();
            try
            {
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress, Port);
                client = new TcpClient(iPEndPoint);
                ns = client.GetStream();
            }
            catch(SocketException e)
            {
                Console.WriteLine("Neuspela konekcija, poruka greske: " + e.Message);
                throw;
            }
            string poruka = "", poruka2 = "";
            if (slanje_paketa)
            {
                poruka = FormatirajDatum(dat);
                //FORMATIRATI PORUKU KOJA SE SALJE (poruka)
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(poruka);
                ns.Write(data, 0, data.Length);
                //-----------------------------------------------
                //PRIHVATANJE ODGOVORA
                //-----------------------------------------------
                data = new Byte[256];
                // String to store the response ASCII representation.
                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = ns.Read(data, 0, data.Length);
                ResponseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                poruka2 = data.ToString();
                string[] temps = poruka2.Split(';');
                foreach (string s in temps)
                {
                    ClientPackage calc = new ClientPackage();
                    calc.FromString(s);
                    compack.Add(calc);
                }
                ns.Close();
                client.Close();
            }
            else
            {
                poruka = "Kalkulacija;" + packetOut.ToString();
                
                //FORMATIRATI PORUKU JER SALJEMO KALKULACIJU NA UPIS (poruka)
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(poruka);
                ns.Write(data, 0, data.Length);
                //-----------------------------------------------
                //PRIHVATANJE ODGOVORA
                //-----------------------------------------------
                data = new Byte[256];
                // String to store the response ASCII representation.
                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = ns.Read(data, 0, data.Length);
                ResponseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                foreach(ClientPackage c in compack)
                {
                    compack.Remove(c);
                }
                ns.Close();
                client.Close();
            }
        }

    }
}
