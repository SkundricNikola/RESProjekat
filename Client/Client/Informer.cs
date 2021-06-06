using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class AnswerReader : IReader
    {
        public string AskForText()
        {
            return Console.ReadLine();
        }
    }
    public class Informer
    {
        public Informer()
        {

        }

        public void PrvaPorukaUspesna()
        {
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("\tPODACI USPESNO UPISANI U BAZU PODATAKA");
            Console.WriteLine("-----------------------------------------------------");
        }
        public void PrvaPorukaNeuspesna()
        {
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("\tUPISIVANJE PODATAKA U BAZU NIJE USPELO");
            Console.WriteLine("\tMOLIMO POKUSAJTE PONOVO");
            Console.WriteLine("-----------------------------------------------------");
        }


        //FUNKCIJA ZA ISPISIVANJE OPCIJA
        public void ShowOptions()
        {
            Console.WriteLine("Please type in the number of the operation you would like to conduct:");
            Console.WriteLine("1 - Write the usage of electric energy for certain region");
            Console.WriteLine("2 - Print all the function calculations");
            Console.WriteLine("IZADJI - Napustanje programa");
        }
        //FUNKCIJA KOJA REALIZUJE KOMUNIKACIJU KORISNIKA SA KLIJENT APLIKACIJOM
        public string AskClient()
        {
            AnswerReader ar = new AnswerReader();
            string readtext = "";
            int num;
            string ninfo = "";
            string poruka = "";
            bool iscorrect = false;
            do
            {
                ShowOptions();
                readtext = ar.AskForText();
                if (readtext == "IZADJI")
                {
                    Console.WriteLine("");
                    return "exit";
                }
                Int32.TryParse(readtext, out num);
            } while (num != 1 && num != 2);
            if (num == 1)
            {
                poruka += "1;";
                Console.WriteLine("Odrabrali ste unos podataka za određeni region i određeni datum");
                do
                {
                    Console.WriteLine("Unesite region za koji unosite vrednost");
                    try
                    {
                        ninfo = ar.AskForText();
                        ninfo = ninfo.Trim();
                        ninfo = ninfo.ToUpper();
                        if (ninfo == "")
                        {
                            throw new FormatException("Neispravna vrednost za region");
                        }
                        else
                        {
                            poruka += ninfo;
                        }
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine(e.Message);
                        throw;
                        return e.Message;
                    }
                } while (ninfo == "");
                do
                {
                    Console.WriteLine("Unesite vreme za koje unosite vrednost u formatu sekunde/minuti/sati/dani/meseci/godine");
                    try
                    {
                        ninfo = ar.AskForText();
                        string[] iscorrectformat = ninfo.Split('/');
                        if (iscorrectformat.Length != 6)
                        {
                            throw new FormatException("Pogresan format! Niste uneli neki parametar ili ste ih uneli previse.");
                        }
                        int sekund = -1, minut = -1, sat = -1, dan = -1, mesec = -1, godina = -1;
                        if (Int32.TryParse(iscorrectformat[0], out sekund) == false || Int32.TryParse(iscorrectformat[1], out minut) == false || Int32.TryParse(iscorrectformat[2], out sat) == false || Int32.TryParse(iscorrectformat[3], out dan) == false || Int32.TryParse(iscorrectformat[4], out mesec) == false || Int32.TryParse(iscorrectformat[5], out godina) == false)
                        {
                            throw new FormatException("Pogresan format! Nije moguce pretvoriti neki od parametara u broj. ");
                       }
                        if (sekund > 59 || sekund < 0 || minut > 59 || minut < 0 || sat > 23 || sat < 0 || dan > 31 || dan < 0 || mesec > 12 || mesec < 0 || godina > 2021 || godina < 2000)
                        {
                            throw new FormatException("Pogresan format! Neki od podataka ima vrednost vecu/manju nego sto bi smeo da ima.");
                        }
                        else { iscorrect = true; }
                    }
                    catch(FormatException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                while (!iscorrect);
                poruka += ";" + ninfo;
                iscorrect = false;
                do
                {
                    Console.WriteLine("Unesite potrosnju (unos sa dve decimalne cifre)");
                    try
                    {
                        ninfo = ar.AskForText();
                        double pokusaj;
                        if (Double.TryParse(ninfo, out pokusaj) == false)
                        {
                            throw new FormatException("Pogresan format! Nije unet broj!");
                        }
                        else
                        {
                            iscorrect = true;
                            poruka += ";" + ninfo;
                        }
                    }
                    catch(FormatException e)
                    {
                        Console.WriteLine(e.Message);
                        throw;
                    }
                } while (!iscorrect);
            }
            else
            {
                poruka += "2; ";
                do
                {
                    Console.WriteLine("Unesite vreme za koje zelite da vidite potrosnju u formatu DD/MM/YYYY");
                    ninfo = Console.ReadLine();
                    string[] iscorrectformat = ninfo.Split('/');
                    if (iscorrectformat.Length != 3)
                    {
                        Console.WriteLine("Pogresan format! Niste uneli neki parametar ili ste ih uneli previse.");
                        continue;
                    }
                    int dan = -1, mesec = -1, godina = -1;
                    if (Int32.TryParse(iscorrectformat[0], out dan) == false || Int32.TryParse(iscorrectformat[1], out mesec) == false || Int32.TryParse(iscorrectformat[2], out godina) == false)
                    {
                        Console.WriteLine("Pogresan format! Nije moguce pretvoriti neki od parametara u broj. ");
                        continue;
                    }
                    if (dan > 31 || dan < 0 || mesec > 12 || mesec < 0 || godina > 2021 || godina < 2000)
                    {
                        Console.WriteLine("Pogresan format! Neki od podataka ima vrednost vecu/manju nego sto bi smeo da ima.");
                        continue;
                    }
                    iscorrect = true;
                }
                while (!iscorrect);
                poruka += ninfo;
            }
            return poruka;
        }
        /*public void PrintList(string lista)
        {
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("DATUM\tVREDNOST PRORACUNA\tVRSTA PRORACUNA");
            Console.WriteLine("---------------------------------------------------------------------");
            string[] objekti = lista.Split(';');
            // SEKUND/MINUT/SAT/DAN/MESEC/GODINA-REZULTAT-VRSTA
            //{0}/{1}/{2}/{3}/{4}/{5}-{6}-{7}
            foreach (var objekat in objekti)
            {
                string[] delovi2 = {"","","","","","" };
                string[] delovi = objekat.Split('/');
                int sekund = 0, minut = 0, sat = 0, dan = 0, mesec = 0, godina = 0;
                bool[] issucess = new bool[6];
                double rezultat = 0.0;
                try { delovi2 = delovi[5].Split('-'); }
                catch(IndexOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                string priprema = "";
                try
                {
                    issucess[0] = Int32.TryParse(delovi[0], out sekund);
                    issucess[1] = Int32.TryParse(delovi[1], out minut);
                    issucess[2] = Int32.TryParse(delovi[2], out sat);
                    issucess[3] = Int32.TryParse(delovi[3], out dan);
                    issucess[4] = Int32.TryParse(delovi[4], out mesec);
                    issucess[5] = Int32.TryParse(delovi2[0], out godina);
                    foreach (var vrednost in issucess)
                    {
                        if (vrednost == false)
                        {
                            throw new FormatException("Format unetih brojeva nije validan i nije moguce konvertovati ga u DateTime.");
                        }
                    }
                }
                catch(FormatException e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                try
                {
                    if (Double.TryParse(delovi2[1], out rezultat) == false)
                    {
                        throw new Exception("Format unetog broja je neispravan, nemoguce ga je konvertovati");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                try
                {
                    if (!(delovi2[2] == "MINIMALNI" || delovi2[2] == "PROSECNI" || delovi2[2] == "MAKSIMALNI"))
                    {
                        throw new Exception("Format unete vrste merenja je neispravan!");
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                DateTime vreme = new DateTime(godina,mesec,dan,sat,minut,sekund);
                priprema += vreme.ToString() + "\t" + rezultat.ToString() + "\t" + delovi2[2];
                Console.WriteLine(priprema);
            }
            Console.WriteLine("---------------------------------------------------------------------");
        }*/
    }
}
