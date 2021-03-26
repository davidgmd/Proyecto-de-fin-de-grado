using System;
using System.Collections.Generic;
using System.Text;

namespace ElEscribaDelDJ.Classes
{
    public class Aventura:EscenarioCampana
    {        
        private int _numeroaventura;

        public int NumeroAventura
        {
            get { return _numeroaventura; }
            set { _numeroaventura = value; }
        }
    }
}
