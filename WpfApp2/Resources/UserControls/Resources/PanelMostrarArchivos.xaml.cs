using ElEscribaDelDJ.Classes;
using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Lógica de interacción para PanelMostrarArchivos.xaml
    /// </summary>
    public partial class PanelMostrarArchivos : UserControl
    {

        
        public PanelMostrarArchivos()
        {
            ConfiguracionPagina.DefinirIdioma(this, "Resources");
            
            InitializeComponent();
            this.DataContext = this;
            RellenarPanel(DatosAplicacion.CampanaSeleccionada.Recursos.Documentos, WrapPanelCampaign, StackPanelCampana);
            if (DatosAplicacion.EscenarioSeleccionado is null)
                RellenarPanel(null, WrapPanelScenary, StackPanelScenary);
            else
                RellenarPanel(DatosAplicacion.EscenarioSeleccionado.Recursos.Documentos, WrapPanelScenary, StackPanelScenary);

            if (DatosAplicacion.AventuraSeleccionada is null)
                RellenarPanel(null, WrapPanelAdventures, StackPanelAdventure);
            else
                RellenarPanel(DatosAplicacion.AventuraSeleccionada.Recursos.Documentos, WrapPanelAdventures, StackPanelAdventure);

        }

        public string TituloCampana
        {
            get { return (string)GetValue(TituloCampanaProperty); }
            set { SetValue(TituloCampanaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TituloCampana.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TituloCampanaProperty =
            DependencyProperty.Register("TituloCampana", typeof(string), typeof(PanelMostrarArchivos), new PropertyMetadata(""));


        public string TituloEscenario
        {
            get { return (string)GetValue(TituloEscenarioProperty); }
            set { SetValue(TituloEscenarioProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TituloEscenario.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TituloEscenarioProperty =
            DependencyProperty.Register("TituloEscenario", typeof(string), typeof(PanelMostrarArchivos), new PropertyMetadata(""));


        public string TituloAventura
        {
            get { return (string)GetValue(TituloAventuraProperty); }
            set { SetValue(TituloAventuraProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TituloAventura.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TituloAventuraProperty =
            DependencyProperty.Register("TituloAventura", typeof(string), typeof(PanelMostrarArchivos), new PropertyMetadata(""));


        public string TextoBoton
        {
            get { return (string)GetValue(TextoBotonProperty); }
            set { SetValue(TextoBotonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextoBoton.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextoBotonProperty =
            DependencyProperty.Register("TextoBoton", typeof(string), typeof(PanelMostrarArchivos), new PropertyMetadata(""));

        private void RellenarPanel(List<Archivos> listaarchivos, WrapPanel panel, StackPanel stackpanel)
        {
            if (listaarchivos is null) { }
                else
                if (listaarchivos.Any())
                {
                    foreach (Archivos archivo in listaarchivos)
                    {
                        stackpanel.Visibility = Visibility.Visible;
                        DetallesArchivoCampana detalles = new DetallesArchivoCampana(archivo);
                        panel.Children.Add(detalles);
                    }
                }
                else
                {
                    stackpanel.Visibility = Visibility.Visible;
                    TextBlock textoinformativo = new TextBlock();
                    textoinformativo.Text = this.FindResource("NoElements").ToString();
                    panel.HorizontalAlignment = HorizontalAlignment.Center;
                    textoinformativo.Padding = new Thickness(6);
                    panel.Children.Add(textoinformativo);
                }
        }
    }
}
