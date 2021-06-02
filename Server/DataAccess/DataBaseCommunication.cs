using DataBasePackages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class DataBaseCommunication
    {
        private static string FormatirajDatum(DateTime d)
        {
            string formatirano = d.Year + "/" + d.Month + "/" + d.Day + "/" + d.Hour + "/" + d.Minute + "/" + d.Second;
            return formatirano;
        }
        public static void SendInfoToInsert(ClientPackage cp,ref bool dobar)
        {
            string responseData = "";
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10011);
            TcpClient client = new TcpClient(iPEndPoint);
            NetworkStream ns = client.GetStream();

            string poruka = "1;"+ cp.ToString();
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(poruka);
            ns.Write(data, 0, data.Length);

            Int32 bytes = ns.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            if(responseData == "dobar")
            {
                dobar = true;
            }
            else { dobar = false; }
        }
        public static void AskForList(DateTime datum,ref List<CalculationPackage>lista)
        {
            string responseData = "";
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10011);
            TcpClient client = new TcpClient(iPEndPoint);
            NetworkStream ns = client.GetStream();
            //FORMATIRATI PORUKU KOJA SE SALJE (poruka)
            string poruka = "2;"+ FormatirajDatum(datum);
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(poruka);
            ns.Write(data, 0, data.Length);
            //-----------------------------------------------
            //PRIHVATANJE ODGOVORA
            //-----------------------------------------------
            data = new Byte[256];
            // String to store the response ASCII representation.
            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = ns.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            string[] splitdata = responseData.Split(';');
            foreach(string s in splitdata)
            {
                int godina, mesec, dan, sat, minut, sekund;
                double vrednost;
                VrstaProracuna vp = new VrstaProracuna();
                string[] podaci = s.Split('/');
                Int32.TryParse(podaci[0], out godina);
                Int32.TryParse(podaci[1], out mesec);
                Int32.TryParse(podaci[2], out dan);
                Int32.TryParse(podaci[3], out sat);
                Int32.TryParse(podaci[4], out minut);
                Int32.TryParse(podaci[5], out sekund);
                Double.TryParse(podaci[6], out vrednost);
                if(podaci[7] == "MINIMALNI")
                {
                    vp = VrstaProracuna.MINIMALNI;
                }
                else if(podaci[7] == "PROSECNI")
                {
                    vp = VrstaProracuna.PROSECNI;
                }
                else if(podaci[7] == "MAKSIMALNI")
                {
                    vp = VrstaProracuna.MAKSIMALNI;
                }
                else { vp = VrstaProracuna.NEODREDJENI; }
                DateTime tempdatum = new DateTime(godina, mesec, dan, sat, minut, sekund);
                CalculationPackage cp = new CalculationPackage(tempdatum,vrednost,vp);
                lista.Add(cp);
            }
            ns.Close();
            client.Close();
        }
    }
}
