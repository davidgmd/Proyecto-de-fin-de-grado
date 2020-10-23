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
        private static string direccionbase = System.IO.Path.GetDirectoryName(
System.Reflection.Assembly.GetExecutingAssembly().Location);
        private static Usuario _sesionusuario;
        private static JObject _userjobject;
        private static SortedList<string, string> _directoriosaplicacion = new SortedList<string, string>();

        public static SortedList<string, string> Directorios
        {
            get { return _directoriosaplicacion; }
            set { _directoriosaplicacion = value; }
        }

        public static JObject UserJobject
        {
            get { return _userjobject; }
            set { _userjobject = value; }
        }


        public static Usuario SesionUsuario
        {
            get { return _sesionusuario; }
            set { _sesionusuario = value; }
        }


        public static string DireccionBase
        {
            get { return direccionbase; }
            set { direccionbase = value; }
        }

        public static void EntornoInicialAplicacion()
        {
            //Añadimos todas las direcciones a las que tendremos que hacer referencia, estas son, idiomas, una para cada idioma
            //Logs, recursos, imagenes, usuario, credenciales, etc.
            Directorios.Add("logs", DireccionBase + "\\Logs\\");
            Directorios.Add("idiomas", DireccionBase + "\\Idiomas\\");
            Directorios.Add("idioma_ingles", DireccionBase + "\\Idiomas\\EN\\");
            Directorios.Add("idioma_espanol", DireccionBase + "\\Idiomas\\ES\\");
            Directorios.Add("clave_encriptado", DireccionBase + "\\Utils\\Keys\\");
            Directorios.Add("credenciales", DireccionBase + "\\Utils\\Credentials\\");
            Directorios.Add("iconos", DireccionBase + "\\Imagenes\\Icons");
            Directorios.Add("imagenes_defecto", DireccionBase + "\\Imagenes\\Default\\");
            Directorios.Add("usuario", DireccionBase + "\\Usuarios\\");
            Directorios.Add("imagenes_usuario", DireccionBase + "\\Usuarios\\Imagenes\\");
            Directorios.Add("archivos_usuario", DireccionBase + "\\Usuarios\\Archivos\\");
            Directorios.Add("recursos", DireccionBase + "\\Resources\\");

            //Nos aseguramos de que estan todas las carpetas importantes creadas, creandolas si faltan.
            foreach (string direccion in Directorios.Values)
            {
                Directory.CreateDirectory(direccion);
            }
        }

    }
}
