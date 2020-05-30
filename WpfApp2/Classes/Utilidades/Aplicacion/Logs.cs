using System;
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

        public static DateTime FehacLog
        {
            get { return fechalog; }
            set { fechalog = value; }
        }

        public static string TituloLog
        {
            get { return titulolog; }
            set { titulolog = value; }
        }

        public static void GenerarLog(string titulolog, string[] camposlog, string nombrearchivo, string resultado)
        {
            //creamos un archivo temporal que luego sera eliminado, esto es simplemente para poder anidar al inicio
            using (StreamWriter sw = File.AppendText(RecursosAplicacion.DireccionBase + "Logs\\" + nombrearchivo + ".temp"))
            {
                //instruccion muy útil pero actualmente no usada
                //Employee e = (Employee)line; // unbox once
 
                //Primero escribimos el titulo
                sw.WriteLine("[" + titulolog + "]");
                //luego la fecha
                sw.WriteLine(DateTime.Now.ToString("g", CultureInfo.CreateSpecificCulture("es-ES")));
                //los campos
                foreach (string campo in camposlog)
                {
                    sw.WriteLine(campo);
                }
                //el resultado
                sw.WriteLine(resultado);
                //Introducimos una separación en el log
                sw.WriteLine("");

                //Para que en vez de estar añadido al final, lo este al principio y tengamos ordenados los logins
                //Del más reciente al más antiguo, anidamos las lineas originales tras escribir las nuevas
                foreach (string linea in System.IO.File.ReadAllLines(RecursosAplicacion.DireccionBase + "Logs\\" + nombrearchivo + ".log"))
                {
                    sw.WriteLine(linea);
                }               
            }

            //Sustituimos el login original por el nuevo y se crea una copia de seguridad por si ocurriera algún error que sera un .back
            System.IO.File.Replace(RecursosAplicacion.DireccionBase + "Logs\\" + nombrearchivo + ".temp", RecursosAplicacion.DireccionBase + "Logs\\" + nombrearchivo + ".log", RecursosAplicacion.DireccionBase + "Logs\\" + nombrearchivo + ".back");
        }

        public static string[] LeerLog(string nombrearchivo, string exito, string fallo)
        {
            List<String> campos = new List<String>();
            string linea;

            if (!File.Exists(RecursosAplicacion.DireccionBase + "Logs\\" + nombrearchivo + ".log"))
            {
                File.Create(RecursosAplicacion.DireccionBase + "Logs\\" + nombrearchivo + ".log");
                return null;
            }

            
            if (File.ReadAllText(RecursosAplicacion.DireccionBase + "Logs\\" + nombrearchivo + ".log").Trim().Equals(""))
            {
                return null;
            }
            using (StreamReader sr = new StreamReader(RecursosAplicacion.DireccionBase + "Logs\\" + nombrearchivo + ".log"))
            {
                while ((linea = sr.ReadLine()) != null)
                { 
                    if (!linea.Equals(""))
                    {
                        Logs.titulolog = linea;
                    }
                    else
                    {
                        Logs.titulolog = sr.ReadLine();
                    }
                    
                    Logs.fechalog = Convert.ToDateTime(sr.ReadLine());

                    campos.Clear();
                    while (!linea.Equals(exito) && !linea.Equals(fallo))
                    {
                        linea = sr.ReadLine();
                        campos.Add(linea);                        
                    }
                    Logs.resultadolog = linea;

                    if (Logs.resultadolog.Equals(exito))
                    {
                        Logs.camposlog = campos.ToArray();
                        return Logs.camposlog;
                    }
                    else
                    {
                        return null;
                    }
                }

                return null;
            }
        }

    }
}
