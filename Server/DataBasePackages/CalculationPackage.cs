using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBasePackages
{//Klasa CalculationPackage sluzi za cuvanje rezultata tj. kao format za cuvanje rezultata operacija kalkulacije
    public enum VrstaProracuna
    {
        MINIMALNI,
        PROSECNI,
        MAKSIMALNI,
        NEODREDJENI
    };
    public class CalculationPackage
    {
        private DateTime vremeProracuna;
        //private DateTime posVreme; dok nam jos nije jasno kako da se ovo koristi nek ostane ovako
        private double rezultat;
        VrstaProracuna vrstaProracuna;
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

        public CalculationPackage(DateTime datum, double vrednost,VrstaProracuna vrsta)
        {
            vremeProracuna = new DateTime(datum.Year,datum.Month,datum.Day,datum.Hour,datum.Minute,datum.Second);
            rezultat = vrednost;
            vrstaProracuna = vrsta;
        }
        public override string ToString()
        {
            string stringvrsta = "";
            if(vrstaProracuna == VrstaProracuna.MAKSIMALNI)
            {
                stringvrsta = "MAKSIMALNI";
            }
            else if(vrstaProracuna == VrstaProracuna.PROSECNI)
            {
                stringvrsta = "PROSECNI";
            }
            else if (vrstaProracuna == VrstaProracuna.MINIMALNI)
            {
                stringvrsta = "MINIMALNI";
            }
            else { stringvrsta = "NEODREDJENI"; }
            string str = string.Format("{0}/{1}/{2}/{3}/{4}/{5}-{6}-",
                vremeProracuna.Second,
                vremeProracuna.Minute,
                vremeProracuna.Hour,
                vremeProracuna.Day,
                vremeProracuna.Month,
                vremeProracuna.Year,
                rezultat);
            str += stringvrsta;
            return str;
        }
        public void FromString(string s)
        {
            VrstaProracuna vp = new VrstaProracuna();
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
            if(secondparse[2] == "MINIMALNI")
            {
                vp = VrstaProracuna.MINIMALNI;
            }
            if (secondparse[2] == "MAKSIMALNI")
            {
                vp = VrstaProracuna.MAKSIMALNI;
            }
            if (secondparse[2] == "PROSECNI")
            {
                vp = VrstaProracuna.PROSECNI;
            }

            DateTime vreme = new DateTime(godina, mesec, dan, sat, minut, sekund);
            vremeProracuna = vreme;
            rezultat = rez;
        }
    }
}
