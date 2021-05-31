using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBasePackages
{//Klasa CalculationPackage sluzi za cuvanje rezultata tj. kao format za cuvanje rezultata operacija kalkulacije
    public class CalculationPackage
    {
        private DateTime vremeProracuna;
        //private DateTime posVreme; dok nam jos nije jasno kako da se ovo koristi nek ostane ovako
        private double rezultat;

        public DateTime VremeProracuna
        {
            get { return vremeProracuna; }
            set { vremeProracuna = value; }
        }

        public double Rezultat
        {
            get { return rezultat; }
            set { rezultat = value; }
        }

        public CalculationPackage()
        {
            vremeProracuna = new DateTime();
        }

        public CalculationPackage(DateTime datum, double vrednost)
        {
            vremeProracuna = new DateTime(datum.Year,datum.Month,datum.Day,datum.Hour,datum.Minute,datum.Second);
            rezultat = vrednost;
        }
        public override string ToString()
        {
            string str = string.Format("{0}/{1}/{2}/{3}/{4}/{5}-{6}",
                vremeProracuna.Second,
                vremeProracuna.Minute,
                vremeProracuna.Hour,
                vremeProracuna.Day,
                vremeProracuna.Month,
                vremeProracuna.Year,
                rezultat);
            return str;
        }
        public void FromString(string s)
        {
            double rez;
            string[] parseddata = s.Split('/');
            int sekund = 0, minut = 0, sat = 0, dan = 0, mesec = 0, godina = 0;
            Int32.TryParse(parseddata[0], out sekund);
            Int32.TryParse(parseddata[1], out minut);
            Int32.TryParse(parseddata[2], out sat);
            Int32.TryParse(parseddata[3], out dan);
            Int32.TryParse(parseddata[4], out mesec);
            string[] secondparse = parseddata[5].Split('-');
            Int32.TryParse(secondparse[0], out godina);
            Double.TryParse(secondparse[1], out rez);
            DateTime vreme = new DateTime(godina, mesec, dan, sat, minut, sekund);
            vremeProracuna = vreme;
            rezultat = rez;
        }
    }
}
