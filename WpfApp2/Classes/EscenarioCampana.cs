using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElEscribaDelDJ.Classes
{
    public class EscenarioCampana:Campana
    {
        private List<Aventura> _listaaventuras = new List<Aventura>();
      
        public List<Aventura> ListaAventuras
        {
            get { return _listaaventuras; }
            set { _listaaventuras = value; }
        }

        public EscenarioCampana RecuperarEscenario(Campana campana, string nombreescenario)
        {
            List<EscenarioCampana> escenarios = campana.ListaEscenarios;
            EscenarioCampana result = escenarios.First(e => e.Nombre.Equals(nombreescenario));
            return result;
        }
    }
}
