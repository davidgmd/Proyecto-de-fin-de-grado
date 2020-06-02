using System;
using System.Collections.Generic;
using System.Text;

namespace ElEscribaDelDJ.Classes
{
    public class Aventura:EscenarioCampana
    {        
        private int numeroaventura;

        public int NumeroAventura
        {
            get { return numeroaventura; }
            set { numeroaventura = value; }
        }
    }
}
