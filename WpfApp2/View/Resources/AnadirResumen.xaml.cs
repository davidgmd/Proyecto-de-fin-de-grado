using ElEscribaDelDJ.Classes;
using ElEscribaDelDJ.Classes.Utilidades;
using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using ElEscribaDelDJ.Resources.UserControls.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
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

            if (indice == 0)
                AnadirButton.IsEnabled = true;
            else
                EditarButton.IsEnabled = true;

            ConfiguracionPagina.DefinirIdioma(this, "Resources");
            
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

        public void ValidarCampoNumerico(TextBox pagina)
        {
            if (pagina.Text.Equals("0"))
                pagina.Text = "1";

            if (!camposcorrectos.Contains(pagina.Name))
                camposcorrectos.Add(pagina.Name);
            pagina.BorderBrush = Brushes.Green;
        }

        private void NombreAMostrarTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarCampo((TextBox)sender, Brushes.Red);
        }

        private void EtiquetasTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarCampo((TextBox)sender,Brushes.Red);
        }

        private void DescripcionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarCampo((TextBox)sender, Brushes.Red);
        }

        private void PaginaTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarCampoNumerico((TextBox)sender);
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
                MessageBox.Show(this.FindResource("FieldsRightMessage").ToString());
            }
            else
            {
                MostrarError();
            }

        }

        public void MostrarError()
        {
            MessageBox.Show(this.FindResource("MissingFieldsError").ToString());
        }

        private void AbrirExploradorBoton_Click(object sender, RoutedEventArgs e)
        {
            SelectorArchivos seleccionararchivo = new SelectorArchivos();
            this.Resumen.Manual = seleccionararchivo.SeleccionarArchivo("Documentos");
        }

        private void EtiquetasTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox etiquetas = (TextBox)sender;
            //Eliminamos todos los espacios en blanco incluidos los que haya entre palabras (trim solo elimina al principio o al final)
            string nuevo_texto = etiquetas.Text.Replace(" ", "");
            //todos en los que haya más de una , sin ningún caracter entre medio se sustituyen por una sola coma.
            nuevo_texto = Regex.Replace(nuevo_texto, @"[\,]{2,}", ",");
            //si hay una , al final se elimina
            nuevo_texto = Regex.Replace(nuevo_texto, @"[\,]$", "");
            etiquetas.Text = nuevo_texto;
        }

        private void PaginaTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox pagina = (TextBox)sender;
            //Eliminamos todos los espacios en blanco incluidos los que haya entre palabras (trim solo elimina al principio o al final)
            string nuevo_texto = pagina.Text.Replace(" ", "");
            if (pagina.Text.Equals(""))
            {
                pagina.Text = "1";
            }
            else { 
                //Elimina todo lo que no sea un numero
                nuevo_texto = Regex.Replace(nuevo_texto, @"\D+", "");
                //si hay algún 0 a la izquierda lo elimina
                nuevo_texto = Regex.Replace(nuevo_texto, @"^[0]+", "");
                //si solo queda un espacio en blanco se cambia a 1
                if (nuevo_texto.Equals(""))
                    nuevo_texto = "1";
                pagina.Text = nuevo_texto;
            }
        }
    }
}
