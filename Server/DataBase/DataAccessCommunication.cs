using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace DataBase
{
    public class DataAccessCommunication
    {
        private XmlDocument doc;
        private XmlTextWriter xtw;

        private static TcpListener dac;
        public DataAccessCommunication()
        {
            doc = new XmlDocument();
            xtw = new XmlTextWriter("DataBase", Encoding.UTF8);
            xtw.WriteStartDocument();
            xtw.WriteStartElement("Data");
            xtw.WriteEndElement();
            xtw.Close();
            string id = "";
            int i = 0;
            while (i != 3)
            {

                FileStream lfile = new FileStream("DataBase", FileMode.Open);
                doc.Load(lfile);
                switch (i)
                {
                    case 0:
                        id = "MINIMALNI";
                        break;
                    case 1:
                        id = "MAKSIMALNI";
                        break;
                    case 2:
                        id = "PROSECNI";
                        break;
                    default:
                        break;
                }
                
                XmlElement cl = doc.CreateElement("Kalkulacija");
                cl.SetAttribute("Tip", id);
                XmlElement na = doc.CreateElement("Forma");
                XmlText natext = doc.CreateTextNode("");
                na.AppendChild(natext);
                cl.AppendChild(na);
                doc.DocumentElement.AppendChild(cl);
                lfile.Close();
                doc.Save("DataBase");
                i++;
            }
        }
        public static void ReceiveMessage()
        {
            try
            {
                dac = new TcpListener(IPAddress.Parse("127.0.0.1"), 10011);
                dac.Start();
                Byte[] bytes = new Byte[256];
                String data = null;
                //DateTime datumprovere;
                DataAccessCommunication dataAccessCom = new DataAccessCommunication();
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
                        string[] tip = data.Split(';');
                        string tipa = "";
                        tipa = tip[0];
                        if (tipa.Equals("Client_Insert"))
                        {
                            bool done = dataAccessCom.CreateNewDataBaseElement(tip[1]);
                            string dobar = "";
                            if (done)
                            {
                                dobar = "true";

                            }
                            else
                            {
                                dobar = "false";
                            }

                            bytes = System.Text.Encoding.ASCII.GetBytes(dobar);
                            stream.Write(bytes, 0, bytes.Length);
                        }
                        else if (tipa.Equals("Calculation_Update"))
                        {
                           dataAccessCom.UpdateDataBaseElement(tip[1]);
                        }
                        else if(tipa.Equals("Read_Calculation"))
                        {
                            //DATA ACCESS PITA ZA LISTU TAKO STO SALJE DATUM
                            
                            bytes = System.Text.Encoding.ASCII.GetBytes(dataAccessCom.ReadDataBaseElement(tip[1], false));
                            stream.Write(bytes, 0, bytes.Length);
                        }
                        else//Read_Client
                        {
                            bytes = System.Text.Encoding.ASCII.GetBytes(dataAccessCom.ReadDataBaseElement(tip[1], true));
                            stream.Write(bytes, 0, bytes.Length);
                        }
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

        public bool CreateNewDataBaseElement(string format)
        {
            FileStream lfile = new FileStream("DataBase", FileMode.Open);
            doc.Load(lfile);
            string[] delovi_cp = format.Split('-');
            string[] dateTime = delovi_cp[0].Split('/');
            string id = dateTime[3] + dateTime[4] + dateTime[5];
            string data = format;
            XmlElement cl = doc.CreateElement("Unos");
            cl.SetAttribute("Date", id);
            XmlElement na = doc.CreateElement("Forma");
            XmlText natext = doc.CreateTextNode(data);
            na.AppendChild(natext);
            cl.AppendChild(na);
            doc.DocumentElement.AppendChild(cl);
            lfile.Close();
            doc.Save("DataBase");
            return true;
        }

        public void UpdateDataBaseElement(string format)
        {
            FileStream up = new FileStream("DataBase", FileMode.Open);
            doc.Load(up);
            XmlNodeList list = doc.GetElementsByTagName("Kalkulacija");
            string[] parts = format.Split('-');
            string id = parts[3];
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cu = (XmlElement)doc.GetElementsByTagName("Kalkulacija")[i];
                XmlElement add = (XmlElement)doc.GetElementsByTagName("Forma")[i];
                if (cu.GetAttribute("Tip") == id)
                {
                    add.InnerText = format;
                    break;
                }
            }
            up.Close();
            doc.Save("DataBase");
        }

        public string  ReadDataBaseElement(string format, bool ispis)
        {
            FileStream rfile = new FileStream("DataBase", FileMode.Open);
            doc.Load(rfile);
            if (ispis)
            {
                string address = "------------Unosi klijenta----------";
                string[] delovi = format.Split('/');
                string id = delovi[0] + delovi[1] + delovi[2];
                XmlNodeList list = doc.GetElementsByTagName("Unos");
                for (int i = 0; i < list.Count; i++)
                {
                    XmlElement cl = (XmlElement)doc.GetElementsByTagName("Unos")[i];
                    XmlElement add = (XmlElement)doc.GetElementsByTagName("Forma")[i];
                    if (cl.GetAttribute("Date") == id)
                    {
                        address += "\nUnos:\t"+add.InnerText + '\n';
                    }
                }
                list = doc.GetElementsByTagName("Kalkulacija");
                address += "---------Vrednosti kalkulacija---------\n";
                for (int i = 0; i < 3; i++)
                {
                    XmlElement cu = (XmlElement)doc.GetElementsByTagName("Kalkulacija")[i];
                    XmlElement add = (XmlElement)doc.GetElementsByTagName("Forma")[i];
                        address += "\nKalkulacija:\t" + add.InnerText + '\n';
                        
                }

                rfile.Close();
                return address;
            }
            else//ispis false znaci da treba lista za CalculationHandler
            {
                string address = "";
                string[] delovi = format.Split('/');
                string id = delovi[0] + delovi[1] + delovi[2];
                XmlNodeList list = doc.GetElementsByTagName("Unos");
                for (int i = 0; i < list.Count; i++)
                {
                    XmlElement cl = (XmlElement)doc.GetElementsByTagName("Unos")[i];
                    XmlElement add = (XmlElement)doc.GetElementsByTagName("Forma")[i];
                    if (cl.GetAttribute("Date") == id)
                    {
                        address += add.InnerText + ';';
                    }
                }
                rfile.Close();
                return address;
            }
        }

        public void DeleteDataBaseElement(string format)
        {
            FileStream rfile = new FileStream("DataBase", FileMode.Open);
            doc.Load(rfile);
            string[] delovi = format.Split('/');
            string id = delovi[0] + delovi[1] + delovi[2];
            XmlNodeList list = doc.GetElementsByTagName("Unos");
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cl = (XmlElement)doc.GetElementsByTagName("Unos")[i];
                if (cl.GetAttribute("Date") == id)
                {
                    doc.DocumentElement.RemoveChild(cl);
                }
            }
            rfile.Close();
            doc.Save("DataBase");
        }
    }
}
