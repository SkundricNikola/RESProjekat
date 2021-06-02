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
            Int32.TryParse(parsed[0], out sekund);
            Int32.TryParse(parsed[1], out minut);
            Int32.TryParse(parsed[2], out sat);
            Int32.TryParse(parsed[3], out dan);
            Int32.TryParse(parsed[4], out mesec);
            Int32.TryParse(secondparse[0], out godina);
            Double.TryParse(secondparse[2], out ptr);
            datum = new DateTime(godina, mesec, dan, sat, minut, sekund);
            Potrosnja = ptr;
            region = secondparse[1];
        }
    }
}
