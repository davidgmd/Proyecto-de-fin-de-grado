using Newtonsoft.Json.Linq;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ElEscribaDelDJ.Classes.Utilidades.Aplicacion
{
    public static class Logs
    {
        private static string titulolog;
        private static DateTime fechalog;
        private static string[] camposlog;
        private static string resultadolog;

        public static string ResultadoLog
        {
            get { return resultadolog; }
            set { resultadolog = value; }
        }

        public static string[] CamposLog
        {
            get { return camposlog; }
            set { camposlog = value; }
        }

        public static DateTime FechaLog
        {
            get { return fechalog; }
            set { fechalog = value; }
        }

        public static string TituloLog
        {
            get { return titulolog; }
            set { titulolog = value; }
        }

        public static void GenerarLog(string titulolog, string[] camposlog, string nombrearchivo)
        {
            //creamos un archivo temporal que luego sera eliminado, esto es simplemente para poder anidar al inicio
            using (StreamWriter sw = File.AppendText(RecursosAplicacion.Directorios["logs"] + nombrearchivo + ".temp"))
            {
                //Primero escribimos el titulo
                StringBuilder texto = new StringBuilder();
                sw.WriteLine("[" + titulolog + "]");
                //luego la fecha
                string cultura = "es-ES";
                if (ConfiguracionAplicacion.Default.Idioma.Equals("EN"))
                    cultura = "en-GB";
                sw.WriteLine(DateTime.Now.ToString("g", CultureInfo.CreateSpecificCulture(cultura)));

                //los campos
                foreach (string campo in camposlog)
                {
                    sw.WriteLine(campo);
                }

                //Introducimos una separación en el log
                sw.WriteLine("");

                //Para que en vez de estar añadido al final, lo este al principio y tengamos ordenados los logins
                //Del más reciente al más antiguo, anidamos las lineas originales tras escribir las nuevas
                foreach (string linea in System.IO.File.ReadAllLines(RecursosAplicacion.Directorios["logs"] + nombrearchivo + ".log"))
                {
                    sw.WriteLine(linea);
                }               
            }

            //Sustituimos el login original por el nuevo y se crea una copia de seguridad por si ocurriera algún error que sera un .back
            System.IO.File.Replace(RecursosAplicacion.Directorios["logs"] + nombrearchivo + ".temp", RecursosAplicacion.Directorios["logs"] + nombrearchivo + ".log", RecursosAplicacion.Directorios["logs"] + nombrearchivo + ".back");
        }

        public static string[] LeerLog(string nombrearchivo)
        {
            List<String> campos = new List<String>();
            string linea;

            //Comprueba si el archivo logs existe en la carpeta "Logs", si no existe lo crea y finaliza.
            if (!File.Exists(RecursosAplicacion.Directorios["logs"] + nombrearchivo + ".log"))
            {
                File.Create(RecursosAplicacion.Directorios["logs"] + nombrearchivo + ".log");
                return null;
            }

            //Si el archivo esta vacio finaliza la lectura del login
            if (File.ReadAllText(RecursosAplicacion.Directorios["logs"] + nombrearchivo + ".log").Trim().Equals(""))
            {
                return null;
            }

            //Utilizamos using porque a si al acabar esta función el archivo se cierra automaticamente, buscamos el archivo de logs y lo leemos linea a linea
            using (StreamReader sr = new StreamReader(RecursosAplicacion.Directorios["logs"] + nombrearchivo + ".log"))
            {
                //Leemos la 1ª linea que es el titulo del log y luego la segunda que es la fecha en la que se produjo
                Logs.titulolog = sr.ReadLine();
                Logs.fechalog = Convert.ToDateTime(sr.ReadLine());

                //Limpiamos los campos por si acaso
                campos.Clear();

                //Leemos todas las lineas que quedan hasta llegar al espacio en blanco que generamos entre logins
                while ((linea = sr.ReadLine()) != "")
                {
                    campos.Add(linea);
                }

                Logs.camposlog = campos.ToArray();
                return Logs.camposlog;
            }
        }

    }
}
