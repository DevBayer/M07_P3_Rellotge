using System;
using System.ComponentModel;

namespace P3_Rellotge
{

    [Serializable()]
    public class DiferenciaHoraria
    {
        String pais;
        int diff;

        public int Diff
        {
            get
            {
                return diff;
            }

            set
            {
                diff = value;
            }
        }

        public string Pais
        {
            get
            {
                return pais;
            }

            set
            {
                pais = value;
            }
        }

        public DiferenciaHoraria(String pais, int diff)
        {
            this.pais = pais;
            this.diff = diff;
        }

    }
}
