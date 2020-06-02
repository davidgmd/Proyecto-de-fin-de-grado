using ElEscribaDelDJ.Classes;
using ElEscribaDelDJ.Classes.Utilidades;
using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace ElEscribaDelDJ.View
{
    /// <summary>
    /// Lógica de interacción para menuPrincipal.xaml
    /// </summary>
    public partial class menuPrincipal : Window
    {
        private List<Campana> listacampana = new List<Campana>();
        private ObservableCollection<Campana> nombres = new ObservableCollection<Campana>();

        public ObservableCollection<Campana> Nombres
        {
            get { return nombres; }
            set { nombres = value; }
        }

        public List<Campana> ListaCampanas
        {
            get { return listacampana; }
            set { listacampana = value; }
        }


        public menuPrincipal()
        {
            InitializeComponent();
            ConfiguracionPagina.DefinirIdioma(this, "MainMenu");

            this.listacampana.AddRange(RecursosAplicacion.SesionUsuario.ListCampaigns);

            foreach (Campana item in this.listacampana)
            {
                this.nombres.Add(item);
            }

            DataContext = this;

            //campaignComboBox.ItemsSource = this.nombres;

        }

        private void CampaigneComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //hace que los botones esten activados una vez se ha elegido la campaña
            Application.Current.Resources["borderMenu"] = Application.Current.Resources["borderMenuEnabled"];
            Application.Current.Resources["botonMenu"] = Application.Current.Resources["botonMenuEnabled"];

            //cambia los respectivos iconos ocultando el por defecto
            if (Application.Current.Resources["noVisible"] != Application.Current.Resources["iconMenu"])
            {
                Application.Current.Resources["visibleInicio"] = Application.Current.Resources["noVisible"];
                Application.Current.Resources["noVisible"] = Application.Current.Resources["iconMenu"];
            }

            var nombrecampana = nombres[campaignComboBox.SelectedIndex].Nombre;
            string direccioncompletaimagen = RecursosAplicacion.DireccionBase + nombres[this.campaignComboBox.SelectedIndex].DireccionImagen;
            string carpeta = RecursosAplicacion.ImagenUsuario + $"\\{RecursosAplicacion.SesionUsuario.NombreUsuario}\\{nombrecampana}\\icon\\";
            string fichero = Regex.Replace(direccioncompletaimagen, "...+\\/|\\+", "");
            string direccionueva = carpeta + fichero;
            
            if (File.Exists(direccioncompletaimagen))
            {
                this.iconoCampaigne.Source = new BitmapImage(new Uri(direccioncompletaimagen, UriKind.Absolute));
                string a = "a";
            }      
            else
            {           
                MessageBox.Show($"no se encuentra el fichero {fichero} en la ruta {direccioncompletaimagen} por favor seleccione la nueva ruta del fichero");
                SelectorArchivos nuevoarchivo = new SelectorArchivos();
                string direccionarchivo = nuevoarchivo.SeleccionImagen();
                if (!(direccionarchivo is null))                                   
                    System.IO.Directory.CreateDirectory(carpeta);
                    File.Copy(direccionarchivo, direccionueva, true);
                    this.iconoCampaigne.Source = new BitmapImage(new Uri(direccionueva, UriKind.Absolute));
            }

            this.StackPanelEscenario.Visibility = Visibility.Visible;
            if (this.EscenarioComboBox.HasItems)
                this.EscenarioComboBox.SelectedIndex = 0;

        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            System.Environment.Exit(0);
        }

        private void campaignComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.campaignComboBox.HasItems)
                this.campaignComboBox.SelectedIndex = 0;
        }
    }
}
