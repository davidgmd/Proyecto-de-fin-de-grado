using ElEscribaDelDJ.Classes;
using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using ElEscribaDelDJ.View.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            //ConfiguracionPagina.DefinirIdioma(this, "Resources");
            
            InitializeComponent();
            this.DataContext = this;
        }

        public void MostrarListas()
        {
            MostrarListaCampana();
            MostrarListaEscenario();
            MostrarListaAventuras();      
        }

        public void MostrarListaCampana()
        {
            this.ListaArchivosCampana = null;
            this.ListaArchivosCampana = ElegirLista(DatosAplicacion.CampanaSeleccionada);
        }

        public void MostrarListaEscenario()
        {
            this.ListaArchivosEscenario = null;
            this.ListaArchivosEscenario = ElegirLista(DatosAplicacion.EscenarioSeleccionado);
        }

        public void MostrarListaAventuras()
        {
            this.ListaArchivosAventura = null;
            this.ListaArchivosAventura = ElegirLista(DatosAplicacion.AventuraSeleccionada);
        }

        public ObservableCollection<Archivos> ElegirLista(Campana escenario_o_aventura)
        {
            if (escenario_o_aventura is null)
            {
                return null;
            }
            else
            {
                switch (this.Seccion)
                {
                    case"Documentos":
                        return escenario_o_aventura.Recursos.Documentos;
                    case "Lore":
                        return escenario_o_aventura.Recursos.Lore;
                    case "Media":
                        return escenario_o_aventura.Recursos.Media;
                    case "Fichas":
                        return escenario_o_aventura.Recursos.Fichas;
                    default:
                        return null;
                }
            }
        }

        #region ListaArchivosCampana
        /// <summary>
        /// La <see cref="DependencyProperty"/> para <see cref="PropertyName"/>.
        /// </summary>
        public static readonly DependencyProperty ListaArchivosCampanaProperty =
            DependencyProperty.Register(
                "ListaArchivosCampana",
                typeof(ObservableCollection<Archivos>),
                typeof(PanelMostrarArchivos),
                new PropertyMetadata(null, OnListaArchivosCampanaPropertyChanged));

        /// <summary>
        /// Es llamada cuando el valor de <see cref="ListaArchivosCampanaProperty"/> cambia en una instancia dada de <see cref="PanelMostrarArchivos"/>.
        /// </summary>
        /// <param name="d">La instancia donde se cambia la propiedad.</param>
        /// <param name="e">La <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instancia que contiene los datos del evento.</param>
        private static void OnListaArchivosCampanaPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PanelMostrarArchivos).OnListaArchivosCampanaChanged(e.OldValue as ObservableCollection<Archivos>, e.NewValue as ObservableCollection<Archivos>);
        }

        /// <summary>
        /// Llamada cuando <see cref="ListaArchivosCampana"/> cambia.
        /// </summary>
        /// <param name="oldValue">El viejo valor</param>
        /// <param name="newValue">El nuevo valor</param>
        private void OnListaArchivosCampanaChanged(ObservableCollection<Archivos> oldValue, ObservableCollection<Archivos> newValue)
        {
            WrapPanelCampaign.Children.Clear();
            RellenarPanel(this.ListaArchivosCampana, WrapPanelCampaign, StackPanelCampana, "Campana");
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<Archivos> ListaArchivosCampana
        {
            get { return (ObservableCollection<Archivos>)GetValue(ListaArchivosCampanaProperty); }
            set { SetValue(ListaArchivosCampanaProperty, value); }
        }
        #endregion

        #region ListaArchivosEscenario
        /// <summary>
        /// La <see cref="DependencyProperty"/> para <see cref="PropertyName"/>.
        /// </summary>
        public static readonly DependencyProperty ListaArchivosEscenarioProperty =
            DependencyProperty.Register(
                "ListaArchivosEscenario",
                typeof(ObservableCollection<Archivos>),
                typeof(PanelMostrarArchivos),
                new PropertyMetadata(null, OnListaArchivosEscenarioPropertyChanged));

        /// <summary>
        /// Es llamada cuando el valor de <see cref="ListaArchivosEscenarioProperty"/> cambia en una instancia dada de <see cref="PanelMostrarArchivos"/>.
        /// </summary>
        /// <param name="d">La instancia donde se cambia la propiedad.</param>
        /// <param name="e">La <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instancia que contiene los datos del evento.</param>
        private static void OnListaArchivosEscenarioPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PanelMostrarArchivos).OnListaArchivosEscenarioChanged(e.OldValue as ObservableCollection<Archivos>, e.NewValue as ObservableCollection<Archivos>);
        }

        /// <summary>
        /// Llamada cuando <see cref="ListaArchivosEscenario"/> cambia.
        /// </summary>
        /// <param name="oldValue">El viejo valor</param>
        /// <param name="newValue">El nuevo valor</param>
        private void OnListaArchivosEscenarioChanged(ObservableCollection<Archivos> oldValue, ObservableCollection<Archivos> newValue)
        {
            WrapPanelScenary.Children.Clear();
            RellenarPanel(this.ListaArchivosEscenario, WrapPanelScenary, StackPanelScenary, "Escenario");
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<Archivos> ListaArchivosEscenario
        {
            get { return (ObservableCollection<Archivos>)GetValue(ListaArchivosEscenarioProperty); }
            set { SetValue(ListaArchivosEscenarioProperty, value); }
        }
        #endregion

        #region ListaArchivosAventura
        /// <summary>
        /// La <see cref="DependencyProperty"/> para <see cref="PropertyName"/>.
        /// </summary>
        public static readonly DependencyProperty ListaArchivosAventuraProperty =
            DependencyProperty.Register(
                "ListaArchivosAventura",
                typeof(ObservableCollection<Archivos>),
                typeof(PanelMostrarArchivos),
                new PropertyMetadata(null, OnListaArchivosAventuraPropertyChanged));

        /// <summary>
        /// Es llamada cuando el valor de <see cref="ListaArchivosAventuraProperty"/> cambia en una instancia dada de <see cref="PanelMostrarArchivos"/>.
        /// </summary>
        /// <param name="d">La instancia donde se cambia la propiedad.</param>
        /// <param name="e">La <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instancia que contiene los datos del evento.</param>
        private static void OnListaArchivosAventuraPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PanelMostrarArchivos).OnListaArchivosAventuraChanged(e.OldValue as ObservableCollection<Archivos>, e.NewValue as ObservableCollection<Archivos>);
        }

        /// <summary>
        /// Llamada cuando <see cref="ListaArchivosAventura"/> cambia.
        /// </summary>
        /// <param name="oldValue">El viejo valor</param>
        /// <param name="newValue">El nuevo valor</param>
        private void OnListaArchivosAventuraChanged(ObservableCollection<Archivos> oldValue, ObservableCollection<Archivos> newValue)
        {
            WrapPanelAdventures.Children.Clear();
            RellenarPanel(this.ListaArchivosAventura, WrapPanelAdventures, StackPanelAdventure, "Aventura");
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<Archivos> ListaArchivosAventura
        {
            get { return (ObservableCollection<Archivos>)GetValue(ListaArchivosAventuraProperty); }
            set { SetValue(ListaArchivosAventuraProperty, value); }
        }
        #endregion

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

        public string Seccion
        {
            get { return (string)GetValue(SeccionProperty); }
            set 
            { 
                SetValue(SeccionProperty, value);
                MostrarListas();
            }
        }

        // Using a DependencyProperty as the backing store for Seccion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeccionProperty =
            DependencyProperty.Register("Seccion", typeof(string), typeof(PanelMostrarArchivos), new PropertyMetadata(""));

        private void RellenarPanel(ObservableCollection<Archivos> listaarchivos, WrapPanel panel, StackPanel stackpanel, string tipoaventura)
        {
            if (!(listaarchivos is null))
                if (listaarchivos.Any())
                {
                    int indice = 0;
                    stackpanel.Visibility = Visibility.Visible;
                    foreach (Archivos archivo in listaarchivos)
                    {                     
                        DetallesArchivoCampana detalles = new DetallesArchivoCampana(archivo, this.Seccion, tipoaventura, indice, this);
                        indice++;
                        panel.Children.Add(detalles);
                    }
                }
                else
                {
                    stackpanel.Visibility = Visibility.Visible;
                    TextBlock textoinformativo = new TextBlock();

                    if (ConfiguracionAplicacion.Default.Idioma.Equals("ES"))
                        textoinformativo.Text = "No hay elementos que mostrar";
                    else
                        textoinformativo.Text = "There's no elements to show";

                    panel.HorizontalAlignment = HorizontalAlignment.Center;
                    textoinformativo.Padding = new Thickness(6);
                    panel.Children.Add(textoinformativo);
                }
        }

        private void BotonAnadirCampana_Click(object sender, RoutedEventArgs e)
        {        
            AnadirArchivo anadirarchivo = new AnadirArchivo(null,"Campana",this.Seccion);
            anadirarchivo.ShowDialog();
            MostrarListaCampana();
        }

        private void BotonAnadirEscenario_Click(object sender, RoutedEventArgs e)
        {
            AnadirArchivo anadirarchivo = new AnadirArchivo(null, "Escenario", this.Seccion);
            anadirarchivo.ShowDialog();
            MostrarListaEscenario();
        }

        private void BotonAnadirAventura_Click(object sender, RoutedEventArgs e)
        {
            AnadirArchivo anadirarchivo = new AnadirArchivo(null, "Aventura", this.Seccion);
            anadirarchivo.ShowDialog();
            MostrarListaAventuras();
        }
    }
}
