using System;
using System.Collections.Generic;
using System.Text;

namespace ElEscribaDelDJ.Classes
{
    public class EscenarioCampana
    {
        private string nombreescenario;
        private List<Aventura> listaaventuras = new List<Aventura>();

        public string NombreEscenario
        {
            get { return nombreescenario; }
            set { nombreescenario = value; }
        }
      
        public List<Aventura> ListaAventuras
        {
            get { return listaaventuras; }
            set { listaaventuras = value; }
        }

    }
}
