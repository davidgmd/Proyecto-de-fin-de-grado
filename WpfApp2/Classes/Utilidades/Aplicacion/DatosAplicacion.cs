using System;
using System.Collections.Generic;
using System.Text;

namespace ElEscribaDelDJ.Classes.Utilidades.Aplicacion
{
    public static class DatosAplicacion
    {
        private static Campana _campana = new Campana();

        public static Campana CampanaSeleccionada
        {
            get { return _campana; }
            set { _campana = value; }
        }

        public static EscenarioCampana EscenarioSeleccionado;
        public static Aventura AventuraSeleccionada;

    }
}
