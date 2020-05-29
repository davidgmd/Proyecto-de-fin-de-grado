using System;
using System.Collections.Generic;
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

        public static void DefinirIdioma(Window ventana, string nombreventana)
        {
            //Según el idioma cargamos uno u otro
            var path = RecursosAplicacion.DireccionBase + "\\Idiomas\\" + ConfiguracionAplicacion.Default.Idioma + "\\" + nombreventana;

            idioma.Source = new Uri(path, UriKind.Absolute);
            ventana.Resources.MergedDictionaries.Add(idioma);
        }
    }
}
