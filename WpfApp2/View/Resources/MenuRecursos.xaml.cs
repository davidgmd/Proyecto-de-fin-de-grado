using ElEscribaDelDJ.Classes;
using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
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
using System.Windows.Shapes;

namespace ElEscribaDelDJ.View.Resources
{
    /// <summary>
    /// Lógica de interacción para MenuRecursos.xaml
    /// </summary>
    public partial class MenuRecursos : Window
    {
        public MenuRecursos(Campana CampanaSeleccionada, EscenarioCampana EscenarioSeleccionado, Aventura AventuraSeleccionada)
        {
            InitializeComponent();
            ConfiguracionPagina.DefinirIdioma(this, "Resources");
            RefrescarUcs();
        }

        private double _maxheightscrollviewers;

        public double MaximaAlturaScrollViewers
        {
            get { return _maxheightscrollviewers; }
            set { _maxheightscrollviewers = value; }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            double AlturaColumna = PrimeraColumna.ActualHeight;
            double AlturaBoton = BotonDetallesDocumentos.ActualHeight;

            MaximaAlturaScrollViewers = (AlturaColumna - AlturaBoton);
            this.DataContext = this;
        }

        private void RefrescarUcs()
        {
            UCdocuments.NombreElemento = this.FindResource("DocumentsTitle").ToString();
            UCresumenes.NombreElemento = this.FindResource("RulesTitle").ToString();
            UClore.NombreElemento = this.FindResource("LoreTitle").ToString();
            UCmedia.NombreElemento = this.FindResource("MediaTitle").ToString();
            UCfichas.NombreElemento = this.FindResource("SheetsTitle").ToString();
        }

        private void MostrarScrollViewer(ScrollViewer panel, Border bordeocultar)
        {
            if (panel.IsVisible)
            {
                panel.Visibility = Visibility.Collapsed;
                bordeocultar.Visibility = Visibility.Visible;
            }
            else
            {               
                panel.Visibility = Visibility.Visible;
                bordeocultar.Visibility = Visibility.Collapsed;
            }
        }

        private void BotonDetallesDocumentos_Click(object sender, RoutedEventArgs e)
        {
            MostrarScrollViewer(ScrollViewerDocumentos, DescripcionDocumentos);
        }

        private void BotonDetallesReglas_Click(object sender, RoutedEventArgs e)
        {
            MostrarScrollViewer(ScrollViewerReglas, DescripcionResumenes);
        }

        private void BotonLore_Click(object sender, RoutedEventArgs e)
        {
            MostrarScrollViewer(ScrollViewerLore, DescripcionLore);
        }

        private void BotonMedia_Click(object sender, RoutedEventArgs e)
        {
            MostrarScrollViewer(ScrollViewerMedia, DescripcionMedia);
        }

        private void BotonFichas_Click(object sender, RoutedEventArgs e)
        {
            MostrarScrollViewer(ScrollViewerFichas, DescripcionFichas);
        }
    }
}
