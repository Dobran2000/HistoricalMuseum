using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoricalMuseum
{
    public class Paznic
    {
        #region Properties
        public string Nume { get; set; }

        public string Prenume { get; set; }

        public string ZonaDeActivitate { get; set; }

        public int Varsta { get; set; }
        #endregion

        #region Constructors
        public Paznic()
        {

        }
        public Paznic(string nume, string prenume, string zonaDeActivitate, int varsta)
        {
            Nume = nume;
            Prenume = prenume;
            ZonaDeActivitate = zonaDeActivitate;
            Varsta = varsta;
        }
        #endregion
    }
}
