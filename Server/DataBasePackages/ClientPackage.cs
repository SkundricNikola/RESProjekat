using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBasePackages
{
    public class ClientPackage
    {//Klasa ClientPackage sluzi kao forma koja ce se koristiti pri cuvanju u bazi podataka
        private DateTime datum;
        private string region;
        private double potrosnja;
        
        public DateTime Datum
        {
            get { return datum; }
            set { datum = value; }
        }

        public string Region
        {
            get { return region; }
            set { region = value; }
        }

        public double Potrosnja
        {
            get { return potrosnja; }
            set { potrosnja = value; }
        }

        public override string ToString()
        {
            string str = string.Format("{0}/{1}/{2}/{3}/{4}/{5}-{6}-{7}",
                datum.Second,
                datum.Minute,
                datum.Hour,
                datum.Day,
                datum.Month,
                datum.Year,
                region,
                potrosnja);
            return str;
        }
        public void FromString(string text)
        {
            string[] parsed = text.Split('/');
            string[] secondparse = parsed[5].Split('-');
            int godina, mesec, dan, sat, minut, sekund;
            double ptr;
            bool[] tryparsesuccess = new bool[6];
            bool trydoubleparse = new bool();
            try
            {
                tryparsesuccess[0] = Int32.TryParse(parsed[0], out sekund);
                tryparsesuccess[1] = Int32.TryParse(parsed[1], out minut);
                tryparsesuccess[2] = Int32.TryParse(parsed[2], out sat);
                tryparsesuccess[3] = Int32.TryParse(parsed[3], out dan);
                tryparsesuccess[4] = Int32.TryParse(parsed[4], out mesec);
                tryparsesuccess[5] = Int32.TryParse(secondparse[0], out godina);
                trydoubleparse = Double.TryParse(secondparse[2], out ptr);
                foreach (var tp in tryparsesuccess)
                {
                    if (tp == false)
                    {
                        throw new ArgumentNullException("Nemoguca konverzija u datum");
                    }
                }
                if (sekund < 0 || sekund > 59 || minut < 0 || minut > 59 || sat < 0 || sat > 23 || dan < 1 || dan > 31 || mesec < 1 || mesec > 12 || godina < 1900 || godina > 2021)
                {
                    throw new ArgumentOutOfRangeException("Neispravne vrednosti parametara datuma");
                }
                if(trydoubleparse == false)
                {
                    throw new ArgumentNullException("Nemoguca konverzija u vrednost");
                }
                if(ptr < 0)
                {
                    throw new ArgumentOutOfRangeException("Nemoguca negativna potrosnja");
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            datum = new DateTime(godina, mesec, dan, sat, minut, sekund);
            Potrosnja = ptr;
            region = secondparse[1];
            if(region.Trim() == "")
            {
                throw new ArgumentNullException("Nije uneta vrednost za region");               
            }
        }
    }
}
