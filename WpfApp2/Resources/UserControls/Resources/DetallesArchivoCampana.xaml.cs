using ElEscribaDelDJ.Classes;
using ElEscribaDelDJ.Classes.Utilidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ElEscribaDelDJ.Resources.UserControls.Resources
{
    /// <summary>
    /// Lógica de interacción para DetallesArchivo.xaml
    /// </summary>
    public partial class DetallesArchivoCampana : UserControl
    {
        public DetallesArchivoCampana(Archivos archivo)
        {
            InitializeComponent();
            NombreArchivo = archivo.NombreArchivo;
            DireccionArchivo = archivo.Direccion;
            UrlArchivo = archivo.Url;
            Imagen = Extensiones.IconoExtension(archivo.Extension);
            this.DataContext = this;
        }

        private string _nombrearchivo;

        public string NombreArchivo
        {
            get { return _nombrearchivo; }
            set { _nombrearchivo = value; }
        }

        private string _imagen;

        public string Imagen
        {
            get { return _imagen; }
            set { _imagen = value; }
        }


        private string _direccionarchivo;

        public string DireccionArchivo
        {
            get { return _direccionarchivo; }
            set { _direccionarchivo = value; }
        }

        private string _urlarchivo;

        public string UrlArchivo
        {
            get { return _urlarchivo; }
            set { _urlarchivo = value; }
        }



    }
}
