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
                Console.WriteLine("Unesite region za koji unosite vrednost");
                ninfo = ar.AskForText();
                poruka += ninfo;
                do
                {
                    Console.WriteLine("Unesite vreme za koje unosite vrednost u formatu ss/mm/SS/dd/MM/GG");
                    ninfo = ar.AskForText();
                    string[] iscorrectformat = ninfo.Split('/');
                    if (iscorrectformat.Length != 6)
                    {
                        Console.WriteLine("Pogresan format! Niste uneli neki parametar ili ste ih uneli previse.");
                        continue;
                    }
                    int sekund = -1, minut = -1, sat = -1, dan = -1, mesec = -1, godina = -1;
                    if (Int32.TryParse(iscorrectformat[0], out sekund) == false || Int32.TryParse(iscorrectformat[1], out minut) == false || Int32.TryParse(iscorrectformat[2], out sat) == false || Int32.TryParse(iscorrectformat[3], out dan) == false || Int32.TryParse(iscorrectformat[4], out mesec) == false || Int32.TryParse(iscorrectformat[5], out godina) == false)
                    {
                        Console.WriteLine("Pogresan format! Nije moguce pretvoriti neki od parametara u broj. ");
                        continue;
                    }
                    if (sekund > 59 || sekund < 0 || minut > 59 || minut < 0 || sat > 23 || sat < 0 || dan > 31 || dan < 0 || mesec > 12 || mesec < 0 || godina > 2021 || godina < 2000)
                    {
                        Console.WriteLine("Pogresan format! Neki od podataka ima vrednost vecu/manju nego sto bi smeo da ima.");
                        continue;
                    }
                    iscorrect = true;
                }
                while (!iscorrect);
                poruka += ";" + ninfo;
                iscorrect = false;
                do
                {
                    Console.WriteLine("Unesite potrosnju (unos sa dve decimalne cifre)");
                    ninfo = ar.AskForText();
                    double pokusaj;
                    if (Double.TryParse(ninfo, out pokusaj) == false)
                    {
                        Console.WriteLine("Pogresan format!");
                        continue;
                    }
                    iscorrect = true;
                    poruka += ";" + ninfo;
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
        public void PrintList(string lista)
        {
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("DATUM\tVREDNOST PRORACUNA\tVRSTA PRORACUNA");
            Console.WriteLine("---------------------------------------------------------------------");
            string[] objekti = lista.Split(';');
            // SEKUND/MINUT/SAT/DAN/MESEC/GODINA-REZULTAT-VRSTA
            //{0}/{1}/{2}/{3}/{4}/{5}-{6}-{7}
            foreach (var objekat in objekti)
            {
                string[] delovi = objekat.Split('/');
                int sekund = 0, minut = 0, sat = 0, dan = 0, mesec = 0, godina = 0;
                double rezultat = 0.0;
                string priprema = "";
                Int32.TryParse(delovi[0], out sekund);
                Int32.TryParse(delovi[1], out minut);
                Int32.TryParse(delovi[2], out sat);
                Int32.TryParse(delovi[3], out dan);
                Int32.TryParse(delovi[4], out mesec);
                string[] delovi2 = delovi[5].Split('-');
                Int32.TryParse(delovi2[0], out godina);
                Double.TryParse(delovi2[1], out rezultat);
                DateTime vreme = new DateTime(godina,mesec,dan,sat,minut,sekund);
                priprema += vreme.ToString() + "\t" + rezultat.ToString() + "\t" + delovi2[2];
                Console.WriteLine(priprema);
            }
            Console.WriteLine("---------------------------------------------------------------------");
        }
    }
}
