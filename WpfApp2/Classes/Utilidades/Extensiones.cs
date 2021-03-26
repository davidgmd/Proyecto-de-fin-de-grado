using System;
using System.Collections.Generic;
using System.Text;

namespace ElEscribaDelDJ.Classes.Utilidades
{
    public static class Extensiones
    {
        private static List<String> _documentos;

        public static List<String> Documentos
        {
            get { return _documentos; }
            set { _documentos = value; }
        }

        private static List<String> _imagenes;

        public static List<String> Imagenes
        {
            get { return _imagenes; }
            set { _imagenes = value; }
        }

        private static List<String> _musica;

        public static List<String> Musica 
        {
            get { return _musica; }
            set { _musica = value; }
        }

        static Extensiones()
        {
            Documentos = new List<string>() { "doc", "docx", "txt" };
            Imagenes = new List<string>() { "png", "bmp", "jpg", "jpeg" };
            Musica = new List<string>() { "mp3", "wav" };
        }

        public static string IconoExtension(string extension)
        {
            if (extension.Equals("pdf"))
                return "x";
            else if (Documentos.Contains(extension))
                return "a";
            else if (Imagenes.Contains(extension))
                return "b";
            else
                return "c";
        }

    }
}
