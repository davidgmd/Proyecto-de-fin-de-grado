using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

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
            Documentos = new List<string>() { ".doc", ".docx", ".txt" };
            Imagenes = new List<string>() { ".png", ".bmp", ".jpg", ".jpeg" };
            Musica = new List<string>() { ".mp3", ".wav" };
        }

        public static BitmapImage IconoExtension(string extension)
        {
            if (extension.Equals(".pdf"))
                return new BitmapImage(new Uri("/Images/icons/icons8-pdf2.png", UriKind.Relative)); 
            else if (Documentos.Contains(extension))
                return new BitmapImage(new Uri("/Images/icons/icons8-textdocument2.png", UriKind.Relative));
            else if (Imagenes.Contains(extension))
                return new BitmapImage(new Uri("/Images/icons/icons8-images.png", UriKind.Relative));
            else if (Musica.Contains(extension))
                return new BitmapImage(new Uri("/Images/icons/icons8-music.png", UriKind.Relative));
            else
                return new BitmapImage(new Uri("/Images/icons/icons8-file.png", UriKind.Relative));
        }

    }
}
