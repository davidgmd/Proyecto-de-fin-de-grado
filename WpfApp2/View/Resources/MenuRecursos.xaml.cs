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
        public MenuRecursos()
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



        public string TituloCampana
        {
            get { return (string)GetValue(TituloCampanaProperty); }
            set { SetValue(TituloCampanaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TituloCampana.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TituloCampanaProperty =
            DependencyProperty.Register("TituloCampana", typeof(string), typeof(MenuRecursos), new PropertyMetadata(""));


        public string TituloEscenario
        {
            get { return (string)GetValue(TituloEscenarioProperty); }
            set { SetValue(TituloEscenarioProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TituloEscenario.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TituloEscenarioProperty =
            DependencyProperty.Register("TituloEscenario", typeof(string), typeof(MenuRecursos), new PropertyMetadata(""));


        public string TituloAventura
        {
            get { return (string)GetValue(TituloAventuraProperty); }
            set { SetValue(TituloAventuraProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TituloAventura.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TituloAventuraProperty =
            DependencyProperty.Register("TituloAventura", typeof(string), typeof(MenuRecursos), new PropertyMetadata(""));



        public string TextoBoton
        {
            get { return (string)GetValue(TextoBotonProperty); }
            set { SetValue(TextoBotonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextoBoton.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextoBotonProperty =
            DependencyProperty.Register("TextoBoton", typeof(string), typeof(MenuRecursos), new PropertyMetadata(""));

        private void RefrescarUcs()
        {
            UCdocuments.NombreElemento = this.FindResource("DocumentsTitle").ToString();
            UCresumenes.NombreElemento = this.FindResource("RulesTitle").ToString();
            UClore.NombreElemento = this.FindResource("LoreTitle").ToString();
            UCmedia.NombreElemento = this.FindResource("MediaTitle").ToString();
            UCfichas.NombreElemento = this.FindResource("SheetsTitle").ToString();

            if (ConfiguracionAplicacion.Default.Idioma.Equals("ES"))
            {
                TituloCampana = "Campaña";
                TituloEscenario = "Escenario";
                TituloAventura = "Aventura";
                TextoBoton = "Añadir";
            }
            else
            {
                TituloCampana = "Campaign";
                TituloEscenario = "Scenary";
                TituloAventura = "Adventure";
                TextoBoton = "Add";
            }
            
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
