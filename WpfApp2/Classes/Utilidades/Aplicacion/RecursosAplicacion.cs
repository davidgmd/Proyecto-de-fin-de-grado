using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ElEscribaDelDJ.Classes.Utilidades.Aplicacion
{
    public static class RecursosAplicacion
    {
        private static string direccionbase = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));

        public static string DireccionBase
        {
            get { return direccionbase; }
            set { direccionbase = value; }
        }

    }
}
