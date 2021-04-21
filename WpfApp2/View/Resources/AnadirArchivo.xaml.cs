using ElEscribaDelDJ.Classes;
using ElEscribaDelDJ.Classes.Utilidades;
using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
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
        public AnadirArchivo(Archivos archivo1, string tipoaventura, string seccion, int indice=0)
        {
            InitializeComponent();
            Inicializar(archivo1,tipoaventura,seccion,indice);
            ConfiguracionPagina.DefinirIdioma(this, "Resources");

            this.DataContext = this;
        }

        private void Inicializar(Archivos archivo1, string tipoaventura, string seccion, int indice = 0)
        {
            if (archivo1 is null)
            {
                BotonAnadir.IsEnabled = true;
            }
            else
            {
                BotonEditar.IsEnabled = true;
                this.ArchivoNuevo = archivo1;
            }

            this.TipoAventura = tipoaventura;
            this.Seccion = seccion;
            this.indice = indice;
        }

        private void ErrorCampos()
        {
            if (ConfiguracionAplicacion.Default.Idioma.Equals("EN"))
            {
                MessageBox.Show("Some or all required fields are missing");
            }
            else
            {
                MessageBox.Show("Todos los campos obligatorios no han sido introducidos");
            }
        }

        public Archivos ArchivoNuevo { get; set; } = new Archivos();
        public string TipoAventura { get; set; }
        public string Seccion { get; set; }
        public int indice { get; set; }
        public List<Control> CamposCorrectos = new List<Control>();

        private void BotonAnadir_Click(object sender, RoutedEventArgs e)
        {
            if (CamposCorrectos.Count >= 2)
                ArchivoNuevo.AgregarArchivo(this.TipoAventura, ArchivoNuevo, this.Seccion);
            else
                ErrorCampos();
        }

        private void BotonEditar_Click(object sender, RoutedEventArgs e)
        {
            if (CamposCorrectos.Count >= 2)
                ArchivoNuevo.EditarArchivo(this.TipoAventura, this.ArchivoNuevo, this.indice, this.Seccion);
            else
                ErrorCampos();
        }

        private void BotonCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BotonBuscarArchivo_Click(object sender, RoutedEventArgs e)
        {
            SelectorArchivos seleccionararchivo = new SelectorArchivos();
            this.ArchivoNuevo.Direccion = seleccionararchivo.SeleccionarArchivo(this.Seccion);
            if (!(this.ArchivoNuevo.Direccion is null))
            {
                FileInfo fichero = new FileInfo(this.ArchivoNuevo.Direccion);
                this.ArchivoNuevo.Extension = fichero.Extension;
            }
            else
            {
                this.ArchivoNuevo.Extension = null;
            }
            
        }

        private void NombreTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NombreTextBox.Text.Equals(""))
            {
                ValidarCampo(false, NombreTextBox);
            }
            else
            {
                ValidarCampo(true, NombreTextBox);
            }
        }

        private void ValidarCampo (bool validacion, Control elementointerfaz)
        {
            if (validacion)
            {
                elementointerfaz.BorderBrush = Brushes.Green;
                if (!CamposCorrectos.Contains(elementointerfaz))
                    CamposCorrectos.Add(elementointerfaz);
            }
            else
            {
                elementointerfaz.BorderBrush = Brushes.Red;
                CamposCorrectos.Remove(elementointerfaz);
            }
        }

        private void UrlTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (UrlTextBox.Text.Equals(""))
            {
                ValidarCampo2(false, UrlTextBox);
            }
            else
            {
                ValidarCampo2(true, UrlTextBox);
            }
        }

        private void textodireccionarchivo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textodireccionarchivo.Text.Equals(""))
                ValidarCampo2(false, textodireccionarchivo);   
            else
                ValidarCampo2(true, textodireccionarchivo);
        }

        private void ValidarCampo2(bool validacion, Control elementointerfaz)
        {
            if (validacion)
            {
                elementointerfaz.BorderBrush = Brushes.Green;
                if (!CamposCorrectos.Contains(elementointerfaz))
                    CamposCorrectos.Add(elementointerfaz);
            }
            else
            {
                elementointerfaz.BorderBrush = Brushes.Goldenrod;
                CamposCorrectos.Remove(elementointerfaz);
            }
        }
    }
}
