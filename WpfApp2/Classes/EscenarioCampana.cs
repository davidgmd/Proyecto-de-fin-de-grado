using System;
using System.Collections.Generic;
using System.Text;

namespace ElEscribaDelDJ.Classes
{
    public class EscenarioCampana:Campana
    {
        private List<Aventura> listaaventuras = new List<Aventura>();
      
        public List<Aventura> ListaAventuras
        {
            get { return listaaventuras; }
            set { listaaventuras = value; }
        }
    }
}
