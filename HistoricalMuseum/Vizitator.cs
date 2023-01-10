using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoricalMuseum
{
    public class Vizitator
    {
        #region Properties
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public int Varsta { get; set; }
        
        public DateTime DataSosire;
        public string tipPersoana { get; set; }
        public int Pret { get; set; }
        public int nrVizite { get; set; }
        #endregion

        #region Constructors
        public Vizitator()
        {

        }
        public Vizitator(string nume, string prenume, int varsta, DateTime dataSosire, string tipPersoana, int pret, int nrVizite)
        {
            Nume = nume;
            Prenume = prenume;
            Varsta = varsta;
            DataSosire = dataSosire;
            this.tipPersoana = tipPersoana;
            Pret = pret;
            this.nrVizite = nrVizite;
        }

        #endregion
    }
}
