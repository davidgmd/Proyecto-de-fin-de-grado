﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;

namespace ElEscribaDelDJ.Classes.Utilidades.Aplicacion
{
    public static class ConfiguracionPagina
    {
        private static ResourceDictionary idioma = new ResourceDictionary();

        public static ResourceDictionary Idioma
        {
            get { return idioma; }
            set { idioma = value; }
        }

        public static void DefinirIdioma(Window ventana, string origen)
        {
            //Según el idioma cargamos uno u otro           
            string path = RecursosAplicacion.Directorios["idiomas"] + ConfiguracionAplicacion.Default.Idioma;
            var methodInfo = new StackTrace().GetFrame(1).GetMethod();
            var nombreventana = methodInfo.ReflectedType.Name;

            switch (origen)
            {

                case ("MainMenu"):
                    path = path + "\\View\\" + nombreventana + ".xaml";
                    break;

                case ("Raiz"):
                    path = path + "\\" + nombreventana + ".xaml";
                    break;

                default:
                    path = path + "\\View\\" + origen + "\\" + nombreventana + ".xaml";
                    break;
            }
            

            idioma.Source = new Uri(path, UriKind.Absolute);
            ventana.Resources.MergedDictionaries.Add(idioma);
        }
    }
}
