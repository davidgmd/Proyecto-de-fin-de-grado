﻿using System;
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

        public static int IndiceCampana;

        private static EscenarioCampana _escenarioseleccionado = new EscenarioCampana();

        public static EscenarioCampana EscenarioSeleccionado
        {
            get { return _escenarioseleccionado; }
            set { _escenarioseleccionado = value; }
        }

        public static int IndiceEscenario;

        private static Aventura _aventuraseleccionada;

        public static Aventura AventuraSeleccionada
        {
            get { return _aventuraseleccionada; }
            set { _aventuraseleccionada = value; }
        }

        public static int IndiceAventuraSeleccionada;
    }
}
