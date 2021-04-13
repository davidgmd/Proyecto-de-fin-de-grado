using ElEscribaDelDJ.Classes;
using ElEscribaDelDJ.Classes.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ElEscribaDelDJ.View.Resources
{
    /// <summary>
    /// Lógica de interacción para AnadirArchivo.xaml
    /// </summary>
    public partial class AnadirArchivo : Window
    {
        public AnadirArchivo(Archivos archivo1, string tipoaventura, string seccion)
        {
            InitializeComponent();
            if (archivo1 is null)
            {
                BotonAnadir.IsEnabled = true;
            }
            else
            {
                BotonEditar.IsEnabled = true;
                this.ArchivoViejo = archivo1;
                this.ArchivoNuevo = archivo1;
            }

            this.TipoAventura = tipoaventura;
            this.Seccion = seccion;

            this.DataContext = this;
        }

        public Archivos ArchivoNuevo { get; set; } = new Archivos();
        public Archivos ArchivoViejo { get; set; }
        public string TipoAventura { get; set; }
        public string Seccion { get; set; }

        private void BotonAnadir_Click(object sender, RoutedEventArgs e)
        {
            ArchivoNuevo.AgregarArchivo(this.TipoAventura,ArchivoNuevo,this.Seccion);
        }

        private void BotonEditar_Click(object sender, RoutedEventArgs e)
        {
            ArchivoViejo.EditarArchivo(this.TipoAventura, this.ArchivoViejo, this.ArchivoNuevo, this.Seccion);
        }

        private void BotonCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BotonBuscarArchivo_Click(object sender, RoutedEventArgs e)
        {
            SelectorArchivos seleccionarimagen = new SelectorArchivos();
            this.ArchivoNuevo.Direccion = seleccionarimagen.SeleccionarArchivo();
            if (!(this.ArchivoNuevo is null))
            {
                FileInfo fichero = new FileInfo(this.ArchivoNuevo.Direccion);
                this.ArchivoNuevo.Extension = fichero.Extension;
            }
            
        }
    }
}
