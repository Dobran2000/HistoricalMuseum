using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoricalMuseum
{
    public class Exponat
    {
        #region Properties
        public string NumeExponat { get; set; }

        public string AnProvenienta { get; set; }

        public int Varsta { get; set; }

        public string TipExponat { get; set; }

        public string Greutate { get; set; }
        public DateTime DataSosireMuzeu { get; set; }

        public string ValoareEstimata { get; set; }

        public string Continent { get; set; }

        public Vitrina Vitrina { get; set; }
        public Paznic Paznic { get; set; }

        public 

        #endregion

        #region Constructors
        Exponat()
        {

        }

        public Exponat(string numeExponat, string anProvenienta, int varsta, string tipExponat, DateTime dataSosireMuzeu, string greutate ,string valoareEstimata, string continent)
        {
            NumeExponat = numeExponat;
            AnProvenienta = anProvenienta;
            Varsta = varsta;
            TipExponat = tipExponat;
            DataSosireMuzeu = dataSosireMuzeu;
            Greutate = greutate;
            ValoareEstimata = valoareEstimata;
            Continent = continent;
        }
        #endregion
    }
}
