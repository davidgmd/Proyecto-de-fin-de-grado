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
    public partial class AnadirElemento : Window
    {
        private Campana campana = new Campana();
        private ObservableCollection<Campana> observable = new ObservableCollection<Campana>();
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


        public AnadirElemento(Campana campana, ObservableCollection<Campana> campanas)
        {
            InitializeComponent();
            
            this.campana = campana;
            this.observable = campanas;
            this.DataContext = this;
        }

        private void SeleccionarImagen_Click(object sender, RoutedEventArgs e)
        {
            SelectorArchivos archivo = new SelectorArchivos();
            this.direccionimagen = archivo.SeleccionImagen();
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
                        campana.DireccionImagen = this.direccionimagen;                  
                }
            }

            RecursosAplicacion.SesionUsuario.ListCampaigns = this.observable.ToList<Campana>();
            GestionArchivos.EscribirUsuarioLocal();
        }

        private void BotonCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
