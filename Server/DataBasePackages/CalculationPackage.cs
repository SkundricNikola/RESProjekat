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
        private DateTime posVreme;
        private double rezultat;
        VrstaProracuna vrstaProracuna;
        public VrstaProracuna VrstaProracuna
        {
            get { return vrstaProracuna; }
            set { vrstaProracuna = value; }
        }
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
        public DateTime PosVreme
        {
            get { return posVreme; }
            set { posVreme = value; }
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
            string str = string.Format("{0}/{1}/{2}/{3}/{4}/{5}-{6}/{7}/{8}/{9}/{10}/{11}-{12}-",
                vremeProracuna.Second,
                vremeProracuna.Minute,
                vremeProracuna.Hour,
                vremeProracuna.Day,
                vremeProracuna.Month,
                vremeProracuna.Year,
                posVreme.Second,
                posVreme.Minute,
                posVreme.Hour,
                posVreme.Day,
                posVreme.Month,
                posVreme.Year,
                rezultat);
            str += stringvrsta;
            return str;
        }
        public void FromString(string s)
        {
            VrstaProracuna vp = new VrstaProracuna();
            double rez;
            string[] parseddata = s.Split('-');
            string[] vremeProracun = parseddata[0].Split('/');
            int sekund = 0, minut = 0, sat = 0, dan = 0, mesec = 0, godina = 0;
            Int32.TryParse(vremeProracun[0], out sekund);
            Int32.TryParse(vremeProracun[1], out minut);
            Int32.TryParse(vremeProracun[2], out sat);
            Int32.TryParse(vremeProracun[3], out dan);
            Int32.TryParse(vremeProracun[4], out mesec);
            Int32.TryParse(vremeProracun[5], out godina);
            DateTime vreme = new DateTime(godina, mesec, dan, sat, minut, sekund);
            vremeProracuna = vreme;
            string[] posV = parseddata[1].Split('/');
            Int32.TryParse(posV[0], out sekund);
            Int32.TryParse(posV[1], out minut);
            Int32.TryParse(posV[2], out sat);
            Int32.TryParse(posV[3], out dan);
            Int32.TryParse(posV[4], out mesec);
            Int32.TryParse(posV[5], out godina);
            vreme = new DateTime(godina, mesec, dan, sat, minut, sekund);
            posVreme = vreme;
            if (parseddata[3] == "MINIMALNI")
            {
                vp = VrstaProracuna.MINIMALNI;
            }
            if (parseddata[3] == "MAKSIMALNI")
            {
                vp = VrstaProracuna.MAKSIMALNI;
            }
            if (parseddata[3] == "PROSECNI")
            {
                vp = VrstaProracuna.PROSECNI;
            }

            
            Double.TryParse(parseddata[2],out rezultat);
        }
    }
}
