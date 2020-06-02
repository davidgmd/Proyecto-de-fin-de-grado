using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Text;

namespace ElEscribaDelDJ.Classes.Utilidades.Aplicacion
{
    public static class RecursosAplicacion
    {
        private static string direccionbase = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
        private static Usuario sesionusuario;
        private static JObject userjobject;
        public static readonly string idiomas = direccionbase + "\\Idiomas\\";
        public static readonly string recursos = direccionbase + "\\Resources\\";
        public static readonly string ImagenIcono = direccionbase + "\\Images\\icons\\";
        public static readonly string ImagenUsuario = direccionbase + $"\\Images\\User\\";


        public static JObject UserJobject
        {
            get { return userjobject; }
            set { userjobject = value; }
        }


        public static Usuario SesionUsuario
        {
            get { return sesionusuario; }
            set { sesionusuario = value; }
        }


        public static string DireccionBase
        {
            get { return direccionbase; }
            set { direccionbase = value; }
        }

    }
}
