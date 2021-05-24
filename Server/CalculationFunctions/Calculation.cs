using DataBasePackages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationFunctions
{
    class Calculation
    {
        static void Main(string[] args)
        {
            //TODO:Konekcija sa DataAccess-om i zahtevanje paketa klijenta odnosno informacija koje se nalaze za trenutni dan u bazi podataka
            //primamo string formatiran unutar override metode toString klase ClientPackage
            //List<double> listaMerenja = new List<double>();
            //CalculationPackage paketOut = new CalculationPackage();

            /*//loop
                  string[] delovi = unos.Split('-');
                  //radimo sa DateTime delom
                  string[] datumpodaci = delovi[0].Slit('/');
                  int sekunde = 0, minuti = 0, sati = 0, dani = 0, meseci = 0, godine = 0;
                  Int32.TryParse(datumpodaci[0], out sekunde);
                  Int32.TryParse(datumpodaci[1], out minuti);
                  Int32.TryParse(datumpodaci[2], out sati);
                  Int32.TryParse(datumpodaci[3], out dani);
                  Int32.TryParse(datumpodaci[4], out meseci);
                  Int32.TryParse(datumpodaci[0], out godine);
                  DateTime datum = new DateTime(godine, meseci, dani, sati, minuti, sekunde);
                  paketOut.VremeProracuna = datum;
                  //radimo sa vrednosti potrosnje
                  double potrosnja = 0;
                  Int32.TryParse(delovi[2],out potrosnja);
                  listaMerenja.Add(potrosnja);
             //loopback
             //TODO: rad sa upisom trenutnog vremena izracunavanja
             //kad skontamo sta je
             primili bi i kod koji predstavlja id potrebne operacije
             mozemo ga poslati sa poslednjim paketom koji saljemo iz DataAccessa
             //Minimum kodovi su abstraktni
             if(delovi[3] == 1)
             {
                paketOut.Rezultat = listaMerenja.Min();
             }
             //Maximum
             if(delovi[3] == 2)
             {
                paketOut.Rezultat = listaMerenja.Max();
             }
             //Prosek
             if(delovi[3] == 3)
             {
                paketOut.Rezultat = listaMerenja.Average();
             }
             */
             


        }

    }
}
