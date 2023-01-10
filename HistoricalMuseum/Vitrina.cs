using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoricalMuseum
{
    public class Vitrina
    {
        public int Nr { get; set; }
        public int Suprafata { get; set; }
        public string Material { get; set; }

        public Vitrina()
        { }
        public Vitrina(int nr, int suprafata, string material)
        {
            this.Nr = nr;
            this.Suprafata = suprafata;
            this.Material = material;
        }
    }
}
