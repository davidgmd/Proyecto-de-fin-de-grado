using ElEscribaDelDJ.Classes;
using ElEscribaDelDJ.Classes.Utilidades;
using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using ElEscribaDelDJ.Resources.UserControls.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para AnadirResumen.xaml
    /// </summary>
    public partial class AnadirResumen : Window
    {
        public AnadirResumen(ObservableCollection<Resumenes> listaresumenes, Resumen_reglas uc = null, int indice = 0)
        {
            InitializeComponent();
            Inicializar();
            this.Resumenes = listaresumenes;
            this.DataContext = this;

            if (uc is null)
                AnadirButton.IsEnabled = true;
            else
                EditarButton.IsEnabled = true;
            
        }

        public ObservableCollection<Campana> TiposAventuras { get; set; } = new ObservableCollection<Campana>();
        public Resumenes Resumen { get; set; } = new Resumenes();
        public ObservableCollection<Resumenes> Resumenes;
        public List<string> camposcorrectos = new List<string>();

        public void Inicializar()
        {
            TiposAventuras.Add(DatosAplicacion.CampanaSeleccionada);
                if (!(DatosAplicacion.EscenarioSeleccionado is null))
            TiposAventuras.Add(DatosAplicacion.EscenarioSeleccionado);
            if (!(DatosAplicacion.AventuraSeleccionada is null))
                TiposAventuras.Add(DatosAplicacion.AventuraSeleccionada);

            if (ComboBoxTiposAventura.HasItems)
            ComboBoxTiposAventura.SelectedIndex = 0;

            
        }

        public void ValidarCampo(TextBox cajadetexto, Brush color)
        {
            if (cajadetexto.Text.Equals(""))
            {
                cajadetexto.BorderBrush = color;
                camposcorrectos.Remove(cajadetexto.Name);
            }
            else
            {
                cajadetexto.BorderBrush = Brushes.Green;
                if (!camposcorrectos.Contains(cajadetexto.Name))
                    camposcorrectos.Add(cajadetexto.Name);
            }
        }

        private void NombreAMostrarTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarCampo((TextBox)sender, Brushes.Red);
        }

        private void EtiquetasTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarCampo((TextBox)sender, Brushes.Red);
        }

        private void DescripcionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarCampo((TextBox)sender, Brushes.Red);
        }

        private void PaginaTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarCampo((TextBox)sender, Brushes.Red);
        }

        private void ManualTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarCampo((TextBox)sender, Brushes.Goldenrod);
        }

        private void UrlTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarCampo((TextBox)sender, Brushes.Goldenrod);
        }

        private void AnadirButton_Click(object sender, RoutedEventArgs e)
        {
            if (camposcorrectos.Count >= 5)
            {
                Campana campana = (Campana)ComboBoxTiposAventura.SelectedItem;
                Resumen.NombreTipoAventura = campana.Nombre;

                if (ComboBoxTiposAventura.SelectedIndex == 0)
                {
                    Resumen.TipoAventura = "Campana";
                    DatosAplicacion.CampanaSeleccionada.AnadirResumen(Resumen);
                }          
                else if (ComboBoxTiposAventura.SelectedIndex == 1)
                {
                    Resumen.TipoAventura = "Escenario";
                    DatosAplicacion.EscenarioSeleccionado.AnadirResumen(Resumen);
                }
                else
                {
                    Resumen.TipoAventura = "Aventura";
                    DatosAplicacion.AventuraSeleccionada.AnadirResumen(Resumen);
                }               

                this.Resumenes.Add(Resumen);
            }
            else
            {
                MostrarError();
            }

        }

        public void MostrarError()
        {
            MessageBox.Show("Faltan algunos o todos los campos esenciales");
        }

        private void AbrirExploradorBoton_Click(object sender, RoutedEventArgs e)
        {
            SelectorArchivos seleccionararchivo = new SelectorArchivos();
            this.Resumen.Manual = seleccionararchivo.SeleccionarArchivo("Documentos");
        }
    }
}
