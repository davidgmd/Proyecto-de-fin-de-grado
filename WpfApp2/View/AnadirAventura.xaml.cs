using ElEscribaDelDJ.Classes;
using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ElEscribaDelDJ.View
{
    /// <summary>
    /// Lógica de interacción para AnadirEscenario.xaml
    /// </summary>
    public partial class AnadirAventura : Window
    {
        private Campana campana1;
        private EscenarioCampana escenario1;
        private Aventura aventura1;
        private ObservableCollection<Aventura> observable;

        public ObservableCollection<Aventura> Observable
        {
            get { return observable; }
            set { observable = value; }
        }
        public EscenarioCampana Escenario
        {
            get { return escenario1; }
            set { escenario1 = value; }
        }
        public Campana Campana
        {
            get { return campana1; }
            set { campana1 = value; }
        }
        public Aventura Aventura
        {
            get { return aventura1; }
            set { aventura1 = value; }
        }


        public AnadirAventura(Campana campana, EscenarioCampana escenario, Aventura aventura, ObservableCollection<Aventura> observable1)
        {
            InitializeComponent();

            this.Escenario = escenario;
            this.Campana = campana;
            this.Aventura = aventura;
            this.observable = observable1;
            DataContext = this;

            if (aventura is null)
            {
                this.BotonAnadirAventura.IsEnabled = false;
                this.BotonModificarAventura.IsEnabled = false;
            }
        }

        private void AddAventura_Click(object sender, RoutedEventArgs e)
        {
            Aventura aventura1 = new Aventura();
            aventura1.Nombre = this.TextBoxNombreAventura.Text;
            aventura1.Descripcion = this.TextBoxDescripcionAventura.Text;

            var iguales = Escenario.ListaAventuras.Where(c => c.Nombre.Equals(aventura1.Nombre) && c.Descripcion.Equals(aventura1.Descripcion));

            if (!(iguales.Count() > 0))
            {
                observable.Add(aventura1);
                var lista_escenarios = RecursosAplicacion.SesionUsuario.ListCampaigns.Find(c => c.Nombre.Equals(Campana.Nombre) && c.Descripcion.Equals(Campana.Descripcion)).ListaEscenarios;
                lista_escenarios.Find(c => c.Nombre.Equals(Escenario.Nombre) && c.Descripcion.Equals(Escenario.Descripcion)).ListaAventuras.Add(aventura1);
                GestionArchivos.EscribirUsuarioLocal();
                this.Close();
            }
            else
            {
                MessageBox.Show("Este escenario ya existe, si desea modificar, seleccionelo y pulse en el + en caso contrario cambie el nombre y/o la descripción");
            }          
        }

        private void TextBoxNombreAventura_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarCampos();
        }

        private void TextBoxDescripcionAventura_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarCampos();
        }

        private void ValidarCampos()
        {
            if (!this.TextBoxNombreAventura.Text.Equals(string.Empty) && !this.TextBoxDescripcionAventura.Text.Equals(string.Empty))
            {
                this.BotonAnadirAventura.IsEnabled = true;
            }
            else
            {
                this.BotonAnadirAventura.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BotonModificarAventura_Click(object sender, RoutedEventArgs e)
        {
            /*EscenarioCampana escenario1 = new EscenarioCampana();
            escenario1.Nombre = this.TextBoxNombreAventura.Text;
            escenario1.Descripcion = this.TextBoxDescripcionAventura.Text;

            var iguales = observable.Where(c => c.Nombre.Equals(Escenario.Nombre) && c.Descripcion.Equals(Escenario.Descripcion));

            if ((iguales.Count() > 0))
            {
                iguales.First().Nombre = escenario1.Nombre;
                iguales.First().Descripcion = escenario1.Descripcion;

                RecursosAplicacion.SesionUsuario.ListCampaigns.Find(c => c.Nombre.Equals(Campana.Nombre) && c.Descripcion.Equals(Campana.Descripcion)).ListaEscenarios = observable.ToList<EscenarioCampana>();
                GestionArchivos.EscribirUsuarioLocal();
                this.Close();
            }
            else
            {
                MessageBox.Show("Este escenario no existe, si desea añadir, pulse en añadir");
            }*/
        }
    }
}
