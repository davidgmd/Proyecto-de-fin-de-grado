﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ElEscribaDelDJ.Classes.Utilidades.Aplicacion
{
    public static class GestionArchivos
    {
        public static string LeerArchivo(string carpeta, string nombrearchivo)
        {
            var path = RecursosAplicacion.DireccionBase + $"\\{carpeta}\\" + nombrearchivo + ".json";
            return System.IO.File.ReadAllText(path);
        }

        public static void EscribirArchivo(string carpeta, string nombrearchivo, string texto)
        {
            var path = RecursosAplicacion.DireccionBase + $"\\{carpeta}\\" + nombrearchivo + ".json";
            System.IO.File.WriteAllText(path,texto);
        }
    }
}