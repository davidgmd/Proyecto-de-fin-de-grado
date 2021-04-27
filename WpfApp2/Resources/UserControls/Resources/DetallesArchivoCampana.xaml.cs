using ElEscribaDelDJ.Classes;
using ElEscribaDelDJ.Classes.Utilidades;
using ElEscribaDelDJ.View.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
        public DetallesArchivoCampana(Archivos archivo, string seccion, string tipoaventura, int? indice, PanelMostrarArchivos mostrararchivos = null)
        {
            InitializeComponent();
            Inicializar(archivo, seccion, tipoaventura, indice, mostrararchivos);
            Traducir();
            this.DataContext = this;
        }

        private string _nombrearchivo;

        public string NombreArchivo
        {
            get { return _nombrearchivo; }
            set { _nombrearchivo = value; }
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

        private string _extension;

        public string Extension
        {
            get { return _extension; }
            set { _extension = value; }
        }

        public string Seccion { get; set; }
        public string TipoAventura { get; set; }
        public int Indice;
        public PanelMostrarArchivos MostrarArchivos;


        private void Inicializar(Archivos archivo, string seccion, string tipoaventura, int? indice, PanelMostrarArchivos mostrararchivos = null)
        {
            NombreArchivo = archivo.NombreArchivo;
            DireccionArchivo = archivo.Direccion;
            if (File.Exists(DireccionArchivo))
            {
                BotonUbicacion.IsEnabled = true;
                ImagenUbicacion.Source = new BitmapImage(new Uri("/Images/icons/icons8-folder.png", UriKind.Relative));
            }
            else
            {
                BotonUbicacion.IsEnabled = false;
                ImagenUbicacion.Source = new BitmapImage(new Uri("/Images/icons/icons8-lock.png", UriKind.Relative));
            }

            UrlArchivo = archivo.Url;

            if (ComprobarUrl().Result)
            {
                BotonUrl.IsEnabled = true;
                ImagenUrl.Source = new BitmapImage(new Uri("/Images/icons/icons8-web.png", UriKind.Relative));
            }
            else
            {
                BotonUrl.IsEnabled = false;
                ImagenUrl.Source = new BitmapImage(new Uri("/Images/icons/icons8-lock.png", UriKind.Relative));
            }

            IconoExtension.Source = Extensiones.IconoExtension(archivo.Extension);
            IconoExtension.Width = 35;

            Extension = archivo.Extension;

            this.Seccion = seccion;
            this.TipoAventura = tipoaventura;
            this.Indice = indice.Value;
            this.MostrarArchivos = mostrararchivos;
        }

        private void Traducir()
        {
            if (ConfiguracionAplicacion.Default.Idioma.Equals("EN"))
            {
                //traducimos el texto
                UbicacionTextBlock.Text = "Location";
                EditarTextBlock.Text = "Edit";
                EliminarTextBlock.Text = "Delete";

                //traducimos los tooltips
                BotonArchivo.ToolTip = "Click here once to open the file";
                NameToShowTextBlock.ToolTip = "Name to show no the real file name";
                BotonUbicacion.ToolTip = "Open the file folder in the file explorer";
                BotonUrl.ToolTip = "Open the url in your default web browser";
            }
        }

        private async Task<bool> ComprobarUrl()
        {
            Uri uriResult;
            if (!(Uri.TryCreate(UrlArchivo, UriKind.Absolute, out uriResult)
    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)))
                return false;

            HttpClient client = new HttpClient();
            var checkingResponse = await client.GetAsync(UrlArchivo).ConfigureAwait(false);
            if (!checkingResponse.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void BotonUbicacion_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", System.IO.Path.GetDirectoryName(this.DireccionArchivo));
        }

        private void BotonUrl_Click(object sender, RoutedEventArgs e)
        {
            string url = this.UrlArchivo.Replace("&", "^&");
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }

        private void BotonArchivo_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", this.DireccionArchivo);
        }

        private void BotonEditar_Click(object sender, RoutedEventArgs e)
        {
            AnadirArchivo editar = 
                new AnadirArchivo(new Archivos() 
                { Direccion=this.DireccionArchivo, Extension=this.Extension, NombreArchivo= this.NombreArchivo, Url=this.UrlArchivo},
                this.TipoAventura,this.Seccion,this.Indice);
            editar.ShowDialog();
            ListaAMostrar();
        }

        private void BotonEliminar_Click(object sender, RoutedEventArgs e)
        {
            Archivos eliminararchivo = new Archivos() 
            { Direccion = this.DireccionArchivo, Extension = this.Extension, NombreArchivo = this.NombreArchivo, Url = this.UrlArchivo };

            eliminararchivo.EliminarArchivo(this.TipoAventura, eliminararchivo, this.Seccion, this.Indice);
            ListaAMostrar();
        }

        private void ListaAMostrar()
        {
            switch (this.TipoAventura)
            {
                case "Campana":
                    MostrarArchivos.MostrarListaCampana();
                    break;
                case "Escenario":
                    MostrarArchivos.MostrarListaEscenario();
                    break;
                case "Aventura":
                    MostrarArchivos.MostrarListaAventuras();
                    break;
                default:
                    break;
            }
        }
    }
}
