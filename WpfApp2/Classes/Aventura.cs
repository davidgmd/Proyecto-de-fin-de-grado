using System;
using System.Collections.Generic;
using System.Text;

namespace ElEscribaDelDJ.Classes
{
    public class Aventura
    {
        private string nombreaventura;
        private int numeroaventura;

        public int NumeroAventura
        {
            get { return numeroaventura; }
            set { numeroaventura = value; }
        }

        public string NombreAventura
        {
            get { return nombreaventura; }
            set { nombreaventura = value; }
        }

    }
}
