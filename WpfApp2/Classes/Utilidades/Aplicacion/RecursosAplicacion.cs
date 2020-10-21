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
        public static readonly string Idiomas = direccionbase + "\\Idiomas\\";
        public static readonly string Recursos = direccionbase + "\\Resources\\";
        public static readonly string Usuarios = direccionbase + "\\Usuarios\\";
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

        public static void EntornoInicialAplicacion()
        {
            //obtenemos la dirección del ejecutable
            string direccionejecutable = System.IO.Path.GetDirectoryName(
System.Reflection.Assembly.GetExecutingAssembly().Location);
            //creamos varias carpetas iniciales, la primera es donde iran los logs, luego de utilidades, los archivos para encriptar y credenciales
            //aparte de estos tenemos los de imagenes, idiomas, y uno para los datos de usuario
            string directoriologs = direccionejecutable + "\\Logs\\";
            Directory.CreateDirectory(directoriologs);
            string directorioclaves = direccionejecutable + "\\Utils\\Claves";
            string directoriocredenciales = direccionejecutable + "\\Utils\\Credenciales";
            string directorioiconos = direccionejecutable + "\\Imagenes\\Icons";
            string imagenespordefecto = direccionejecutable + "\\Imagenes\\Default";
            string idiomaingles = direccionejecutable + "\\Idiomas\\EN\\";
            string idiomaespanol = direccionejecutable + "\\Idiomas\\ES\\";
            string imagenesusuario = direccionejecutable + "\\Usuarios\\Imagenes\\";
            string archivossusuario = direccionejecutable + "\\Usuarios\\Archivos\\";


        }

    }
}
