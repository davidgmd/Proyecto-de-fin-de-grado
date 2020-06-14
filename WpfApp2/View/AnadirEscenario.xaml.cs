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
    public partial class AnadirEscenario : Window
    {
        private Campana campana1;
        private EscenarioCampana escenario1;
        private ObservableCollection<EscenarioCampana> observable;

        public ObservableCollection<EscenarioCampana> Observable
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


        public AnadirEscenario(Campana campana, EscenarioCampana escenario, ObservableCollection<EscenarioCampana> observable1)
        {
            InitializeComponent();

            this.Escenario = escenario;
            this.Campana = campana;
            this.observable = observable1;
            DataContext = this;

            if (escenario is null)
            {
                this.BotonAnadirEscenario.IsEnabled = false;
                this.BotonModificarEscenario.IsEnabled = false;
            }
        }

        private void AddEscenario_Click(object sender, RoutedEventArgs e)
        {
            EscenarioCampana escenario1 = new EscenarioCampana();
            escenario1.Nombre = this.TextBoxNombreEscenario.Text;
            escenario1.Descripcion = this.TextBoxDescripcionEscenario.Text;

            var iguales = Campana.ListaEscenarios.Where(c => c.Nombre.Equals(escenario1.Nombre) && c.Descripcion.Equals(escenario1.Descripcion));

            if (!(iguales.Count() > 0))
            {
                observable.Add(escenario1);
                RecursosAplicacion.SesionUsuario.ListCampaigns.Find(c => c.Nombre.Equals(Campana.Nombre) && c.Descripcion.Equals(Campana.Descripcion)).ListaEscenarios.Add(escenario1);
                GestionArchivos.EscribirUsuarioLocal();
                this.Close();
            }
            else
            {
                MessageBox.Show("Este escenario ya existe, si desea modificar, seleccionelo y pulse en el + en caso contrario cambie el nombre y/o la descripción");
            }            
        }

        private void TextBoxNombreEscenario_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarCampos();
        }

        private void TextBoxDescripcionEscenario_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarCampos();
        }

        private void ValidarCampos()
        {
            if (!this.TextBoxNombreEscenario.Text.Equals(string.Empty) && !this.TextBoxDescripcionEscenario.Text.Equals(string.Empty))
            {
                this.BotonAnadirEscenario.IsEnabled = true;
            }
            else
            {
                this.BotonAnadirEscenario.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BotonModificarEscenario_Click(object sender, RoutedEventArgs e)
        {
            EscenarioCampana escenario1 = new EscenarioCampana();
            escenario1.Nombre = this.TextBoxNombreEscenario.Text;
            escenario1.Descripcion = this.TextBoxDescripcionEscenario.Text;

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
            }
        }
    }
}
