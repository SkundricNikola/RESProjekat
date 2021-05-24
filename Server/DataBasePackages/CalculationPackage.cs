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
    }
}
