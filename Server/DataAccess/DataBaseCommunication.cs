﻿using DataBasePackages;
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
        public static void SendInfoToInsert_Client(ClientPackage cp,ref bool dobar)
        {
            string responseData = "";
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10011);
            TcpClient client = new TcpClient(iPEndPoint);
            NetworkStream ns = client.GetStream();

            string poruka = "Client_Insert;"+ cp.ToString();
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(poruka);
            ns.Write(data, 0, data.Length);

            Int32 bytes = ns.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            if(responseData == "true")
            {
                dobar = true;
            }
            else { dobar = false; }
        }
        public static void SendInfoToInsert_Calculation(CalculationPackage cp)
        {
            string responseData = "";
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10011);
            TcpClient client = new TcpClient(iPEndPoint);
            NetworkStream ns = client.GetStream();

            string poruka = "Calculation_Update;" + cp.ToString();
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(poruka);
            ns.Write(data, 0, data.Length);

            Int32 bytes = ns.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            
        }
        public static void AskForList(DateTime datum,ref string odgovorporuka,bool ispis)
        {
            string responseData = "";
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10011);
            TcpClient client = new TcpClient(iPEndPoint);
            NetworkStream ns = client.GetStream();
            string poruka = "";
            //FORMATIRATI PORUKU KOJA SE SALJE (poruka)
            if (!ispis)
            {
                poruka = "Read_Calculation;" + FormatirajDatum(datum);
            }
            else
            {
                poruka = "Read_Client;" + FormatirajDatum(datum);
            }
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(poruka);
            ns.Write(data, 0, data.Length);
            //-----------------------------------------------
            //PRIHVATANJE ODGOVORA
            //-----------------------------------------------
            data = new Byte[256];
            // String to store the response ASCII representation.
            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = ns.Read(data, 0, data.Length);
            if (!ispis)
            {
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                odgovorporuka = responseData;
            }
            else
            {
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                odgovorporuka = responseData;

            }
            ns.Close();
            client.Close();
        }
    }
}
