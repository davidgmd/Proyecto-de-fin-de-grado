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
        public AnadirArchivo(Archivos archivo1, string tipoaventura, string seccion, int indice=0)
        {
            InitializeComponent();
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

            this.DataContext = this;
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
                MessageBox.Show("Todos los campos obligatorios no han sido introducidos");
        }

        private void BotonEditar_Click(object sender, RoutedEventArgs e)
        {
            if (CamposCorrectos.Count >= 2)
                ArchivoNuevo.EditarArchivo(this.TipoAventura, this.ArchivoNuevo, this.indice,this.Seccion);
            else
                MessageBox.Show("Todos los campos obligatorios no han sido introducidos");
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
