using ElEscribaDelDJ.Classes;
using ElEscribaDelDJ.Classes.Utilidades;
using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace ElEscribaDelDJ.View
{
    /// <summary>
    /// Lógica de interacción para AnadirCampana.xaml
    /// </summary>
    public partial class AnadirCampana : Window
    {
        private Campana campana = new Campana();
        private ObservableCollection<Campana> observable = new ObservableCollection<Campana>();
        private SelectorArchivos archivo;
        private string direccionimagen;

        public ObservableCollection<Campana> Observable
        {
            get { return observable; }
            set { observable = value; }
        }


        public Campana Campana
        {
            get { return campana; }
            set { campana = value; }
        }


        public AnadirCampana(Campana campana, ObservableCollection<Campana> campanas)
        {
            InitializeComponent();

            ConfiguracionPagina.DefinirIdioma(this, "MainMenu");
            
            this.campana = campana;
            this.observable = campanas;
            this.DataContext = this;
        }

        private void SeleccionarImagen_Click(object sender, RoutedEventArgs e)
        {
            this.archivo = new SelectorArchivos();
            this.direccionimagen = archivo.SeleccionImagen();
            if (direccionimagen != null)
            ImagenElegida.Source = new BitmapImage(new Uri(direccionimagen, UriKind.Absolute));
        }

        private void Modificar_Click(object sender, RoutedEventArgs e)
        {
            foreach (Campana campana1 in this.observable)
            {
                if (campana1.Nombre.Equals(this.campana.Nombre) && (campana1.Descripcion.Equals(this.campana.Descripcion)))
                {
                    campana1.Nombre = this.NombreTextBox.Text;
                    campana1.Descripcion = this.DescripcionTextBox.Text;
                    if (!(this.direccionimagen is null))
                    {
                        string direccion = this.archivo.MoverImagen(campana1.Nombre, this.direccionimagen);
                        campana.DireccionImagen = direccion;
                    }                                      
                }
            }

            RecursosAplicacion.SesionUsuario.ListCampaigns = this.observable.ToList<Campana>();
            GestionArchivos.EscribirUsuarioLocal();

            this.Close();
        }

        private void BotonCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BotonAnadir_Click(object sender, RoutedEventArgs e)
        {
            Campana campana1 = new Campana();
            campana1.Nombre = NombreTextBox.Text;
            campana1.Descripcion = DescripcionTextBox.Text;
            if (!(this.direccionimagen is null))
            {
                string direccion = this.archivo.MoverImagen(campana1.Nombre, this.direccionimagen);
                campana1.DireccionImagen = direccion;
            }
            else
            {
                campana1.DireccionImagen = this.campana.DireccionImagen;
            }

            var iguales = observable.Where(c => c.Nombre.Equals(campana1.Nombre) && c.Descripcion.Equals(campana1.Descripcion));

                //RecursosAplicacion.SesionUsuario.ListCampaigns.Find(c => c.Nombre.Equals(Campana.Nombre) && c.Descripcion.Equals(Campana.Descripcion)).ListaEscenarios.Add(escenario1);

            if (!(iguales.Count() > 0))
            {
                observable.Add(campana1);
                RecursosAplicacion.SesionUsuario.ListCampaigns = this.observable.ToList<Campana>();
                GestionArchivos.EscribirUsuarioLocal();
                this.Close();
            }
            else
            {
                MessageBox.Show("Esta campaña ya existe, introduzca otro nombre de campaña o descripción");
            }
            
        }
    }
}
