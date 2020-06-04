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
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ElEscribaDelDJ.View
{
    /// <summary>
    /// Lógica de interacción para menuPrincipal.xaml
    /// </summary>
    public partial class menuPrincipal : Window
    {
        private List<Campana> listacampana = new List<Campana>();
        private ObservableCollection<Campana> campanas = new ObservableCollection<Campana>();
        private ObservableCollection<EscenarioCampana> escenarios = new ObservableCollection<EscenarioCampana>();
        private ObservableCollection<Aventura> aventuras = new ObservableCollection<Aventura>();
        private List<Style> estilosbase = new List<Style>();
        private List<Style> estilosactivados = new List<Style>();

        public ObservableCollection<Aventura> Aventuras
        {
            get { return aventuras; }
            set { aventuras = value; }
        }

        public ObservableCollection<EscenarioCampana> Escenarios
        {
            get { return escenarios; }
            set { escenarios = value; }
        }

        public ObservableCollection<Campana> Campanas
        {
            get { return campanas; }
            set { campanas = value; }
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

            //Tras añadir todas las aventuras, vamos añadiendo todas las campañas y de cada campaña sus aventuras
            foreach (Campana item in this.listacampana)
            {
                this.campanas.Add(item);               
            }

            DataContext = this;

            DefinirEstilos();

            //campaignComboBox.ItemsSource = this.nombres;

        }

        private void DefinirEstilos()
        {
            estilosbase.Add((Style)this.Resources["borderMenu"]);
            estilosbase.Add((Style)this.Resources["botonMenu"]);
            estilosbase.Add((Style)this.Resources["noVisible"]);

            estilosactivados.Add((Style)this.Resources["borderMenuEnabled"]);
            estilosactivados.Add((Style)this.Resources["botonMenuEnabled"]);
            estilosactivados.Add((Style)this.Resources["iconMenu"]);         
        }

        private void CampaigneComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var estilo = Application.Current.Resources["borderMenu"];
            Application.Current.Resources["botonMenu"] = Application.Current.Resources["botonMenuEnabled"];

            //cambia los respectivos iconos ocultando el por defecto
            if (Application.Current.Resources["noVisible"] != Application.Current.Resources["iconMenu"])
            {
                Application.Current.Resources["visibleInicio"] = Application.Current.Resources["noVisible"];
                Application.Current.Resources["noVisible"] = Application.Current.Resources["iconMenu"];
            }

            //si se busca un elemento que no esta solo cambia el estilo, si existe lo busca y encuentra
            //para determinar que estilos usar enviamos una u otra lista de estilos
            if (campaignComboBox.SelectedIndex >= 0)
            {
                CambiarEstilos(estilosactivados);
                SeleccionarCampana();
            }             
            else
            {
                CambiarEstilos(estilosbase);
                this.iconoCampaign.Source = new BitmapImage(new Uri("/Images/icons/icons8-escudopregunta.png", UriKind.Relative));
            }

                     
        }

        //modifica los estilos base por activados o viceversa
        private void CambiarEstilos(List<Style> listaestilos)
        {
            //hace que los botones esten activados una vez se ha elegido la campaña
            this.Resources["borderMenu"] = listaestilos[0];
            this.Resources["botonMenu"] = listaestilos[1];

            //cambia los respectivos iconos ocultando el por defecto
            if (this.Resources["noVisible"] != this.Resources["iconMenu"])
            {
                this.Resources["visibleInicio"] = this.Resources["noVisible"];
                this.Resources["noVisible"] = listaestilos[2];
            }
        }

        private void SeleccionarCampana()
        {           
            var nombrecampana = Campanas[campaignComboBox.SelectedIndex].Nombre;
            string direccioncompletaimagen = RecursosAplicacion.DireccionBase + Campanas[this.campaignComboBox.SelectedIndex].DireccionImagen;
            string carpeta = RecursosAplicacion.ImagenUsuario + $"\\{RecursosAplicacion.SesionUsuario.NombreUsuario}\\{nombrecampana}\\icon\\";
            string fichero = Regex.Replace(direccioncompletaimagen, "...+\\/|\\+", "");
            string direccionueva = carpeta + fichero;

            if (File.Exists(direccioncompletaimagen))
            {
                this.iconoCampaign.Source = new BitmapImage(new Uri(direccioncompletaimagen, UriKind.Absolute));
            }
            else
            {
                MessageBox.Show($"no se encuentra el fichero {fichero} en la ruta {direccioncompletaimagen} por favor seleccione la nueva ruta del fichero");
                SelectorArchivos nuevoarchivo = new SelectorArchivos();
                string direccionarchivo = nuevoarchivo.SeleccionImagen();
                if (!(direccionarchivo is null))
                    System.IO.Directory.CreateDirectory(carpeta);
                File.Copy(direccionarchivo, direccionueva, true);
                this.iconoCampaign.Source = new BitmapImage(new Uri(direccionueva, UriKind.Absolute));
            }

            this.escenarios.Clear();
            foreach (EscenarioCampana escenario in Campanas[campaignComboBox.SelectedIndex].ListaEscenarios)
            {
                this.escenarios.Add(escenario);
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

        private void EscenarioComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.aventuras.Clear();
            foreach (Aventura aventura in Escenarios[campaignComboBox.SelectedIndex].ListaAventuras)
            {
                this.aventuras.Add(aventura);
            }
            this.AventuraComboBox.Visibility = Visibility;
            if (this.AventuraComboBox.HasItems)
                this.AventuraComboBox.SelectedIndex = 0;
        }

        private void CampaignAddButton_Click(object sender, RoutedEventArgs e)
        {
            AnadirElemento ventanapopup = new AnadirElemento();
            ventanapopup.Show();
        }
    }
}
