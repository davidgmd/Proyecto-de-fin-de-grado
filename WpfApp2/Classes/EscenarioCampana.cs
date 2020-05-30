using System;
using System.Collections.Generic;
using System.Text;

namespace ElEscribaDelDJ.Classes
{
    class EscenarioCampana
    {
        private string nombreescenario;

        public string NombreEscenario
        {
            get { return nombreescenario; }
            set { nombreescenario = value; }
        }

        private List<Aventura> listaaventuras;

        public List<Aventura> ListaAventuras
        {
            get { return listaaventuras; }
            set { listaaventuras = value; }
        }

    }
}
