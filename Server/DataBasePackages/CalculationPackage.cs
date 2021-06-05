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
            posVreme = new DateTime();
        }

        public CalculationPackage(DateTime datum, double vrednost,VrstaProracuna vrsta)
        {
            try
            {
                vremeProracuna = new DateTime(datum.Year, datum.Month, datum.Day, datum.Hour, datum.Minute, datum.Second);
                if(vrednost < 0)
                {
                    throw new ArgumentOutOfRangeException("Vrednost merenja potrosnje manja od nule.");
                }
                rezultat = vrednost;
                if(vrsta != VrstaProracuna.MAKSIMALNI && vrsta != VrstaProracuna.MINIMALNI && vrsta != VrstaProracuna.PROSECNI && vrsta != VrstaProracuna.NEODREDJENI)
                {
                    throw new ArgumentOutOfRangeException("Nepostojeca vrsta proracuna");
                }
                vrstaProracuna = vrsta;
            }
            catch(ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public override string ToString()
        {
            string stringvrsta = "";
            try
            {
                if (vrstaProracuna == VrstaProracuna.MAKSIMALNI)
                {
                    stringvrsta = "MAKSIMALNI";
                }
                else if (vrstaProracuna == VrstaProracuna.PROSECNI)
                {
                    stringvrsta = "PROSECNI";
                }
                else if (vrstaProracuna == VrstaProracuna.MINIMALNI)
                {
                    stringvrsta = "MINIMALNI";
                }
                else if (vrstaProracuna == VrstaProracuna.NEODREDJENI)
                {
                    stringvrsta = "NEODREDJENI";
                }
                else { throw new ArgumentOutOfRangeException("Nepostojeca vrsta proracuna"); }
            }
            catch(ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
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
            str += stringvrsta.Trim();
            return str;
        }
        public void FromString(string s)
        {
            VrstaProracuna vp = new VrstaProracuna();
            string[] parseddata = s.Split('-');
            string[] vremeProracun = parseddata[0].Split('/');
            int sekund = 0, minut = 0, sat = 0, dan = 0, mesec = 0, godina = 0;
            bool[] tryparsesuccess = new bool[6];
            try
            {
                tryparsesuccess[0] = Int32.TryParse(vremeProracun[0], out sekund);
                tryparsesuccess[1] = Int32.TryParse(vremeProracun[1], out minut);
                tryparsesuccess[2] = Int32.TryParse(vremeProracun[2], out sat);
                tryparsesuccess[3] = Int32.TryParse(vremeProracun[3], out dan);
                tryparsesuccess[4] = Int32.TryParse(vremeProracun[4], out mesec);
                tryparsesuccess[5] = Int32.TryParse(vremeProracun[5], out godina);
                foreach(var tp in tryparsesuccess)
                {
                    if(tp == false)
                    {
                        throw new ArgumentNullException("Nemoguca konverzija u datum");
                    }
                }
                if(sekund < 0 || sekund > 59 || minut < 0 || minut > 59 || sat < 0 || sat > 23 || dan < 1 || dan > 31 || mesec < 1 || mesec > 12 || godina < 1900 || godina > 2021)
                {
                    throw new ArgumentOutOfRangeException("Neispravne vrednosti parametara datuma");
                }
            }
            catch(ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch(ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            DateTime vreme = new DateTime(godina, mesec, dan, sat, minut, sekund);
            vremeProracuna = vreme;
            string[] posV = parseddata[1].Split('/');
            try
            {
                tryparsesuccess[0] = Int32.TryParse(posV[0], out sekund);
                tryparsesuccess[1] = Int32.TryParse(posV[1], out minut);
                tryparsesuccess[2] = Int32.TryParse(posV[2], out sat);
                tryparsesuccess[3] = Int32.TryParse(posV[3], out dan);
                tryparsesuccess[4] = Int32.TryParse(posV[4], out mesec);
                tryparsesuccess[5] = Int32.TryParse(posV[5], out godina);
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
            vreme = new DateTime(godina, mesec, dan, sat, minut, sekund);
            posVreme = vreme;
            try
            {
                if (parseddata[3] == "MINIMALNI")
                {
                    vp = VrstaProracuna.MINIMALNI;
                }
                else if (parseddata[3] == "MAKSIMALNI")
                {
                    vp = VrstaProracuna.MAKSIMALNI;
                }
                else if (parseddata[3] == "PROSECNI")
                {
                    vp = VrstaProracuna.PROSECNI;
                }
                else if (parseddata[3] == "NEODREDJENI")
                {
                    vp = VrstaProracuna.NEODREDJENI;
                }
                else { throw new ArgumentOutOfRangeException("Nepostojeca vrsta merenja"); }
            }
            catch(ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            vrstaProracuna = vp;
            if(Double.TryParse(parseddata[2],out rezultat) == false)
            {
                throw new ArgumentOutOfRangeException("Nemoguca konverzija");
            }
            if(rezultat < 0)
            {
                throw new ArgumentOutOfRangeException("Negativan rezultat merenja.");
            }
        }
    }
}
