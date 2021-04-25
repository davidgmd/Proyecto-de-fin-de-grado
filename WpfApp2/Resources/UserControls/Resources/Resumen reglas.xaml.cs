using ElEscribaDelDJ.Classes;
using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using ElEscribaDelDJ.View.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para Resumen_reglas.xaml
    /// </summary>
    public partial class Resumen_reglas : UserControl
    {
        public Resumen_reglas()
        {
            InitializeComponent();
            Inicializar();
            //recursos.Add(new Recursos() { NombreRecurso = "Jane Doe", Age = 39, Mail = "jane@doe-family.com" });
            //recursos.Add(new Recursos() { NombreRecurso = "Sammy Doe", Age = 13, Mail = "sammy.doe@gmail.com" });
            
        }

        public ObservableCollection<Resumenes> ListaResumenes { get; set; } = new ObservableCollection<Resumenes>();


        public void Inicializar()
        {
            //Añade todos los resumenes de la campaña, escenario y aventura seleccionada
            foreach (Resumenes resumen in DatosAplicacion.CampanaSeleccionada.Recursos.Resumenes)
            {
                resumen.NombreTipoAventura = DatosAplicacion.CampanaSeleccionada.Nombre;
                resumen.TipoAventura = "Campaña";
                ListaResumenes.Add(resumen);
            }

            if (!(DatosAplicacion.EscenarioSeleccionado is null))
            foreach (Resumenes resumen in DatosAplicacion.EscenarioSeleccionado.Recursos.Resumenes)
            {
                resumen.NombreTipoAventura = DatosAplicacion.EscenarioSeleccionado.Nombre;
                resumen.TipoAventura = "Escenario";
                ListaResumenes.Add(resumen);
            }

            if (!(DatosAplicacion.AventuraSeleccionada is null))
                foreach (Resumenes resumen in DatosAplicacion.AventuraSeleccionada.Recursos.Resumenes)
            {
                resumen.NombreTipoAventura = DatosAplicacion.AventuraSeleccionada.Nombre;
                resumen.TipoAventura = "Aventura";
                ListaResumenes.Add(resumen);
            }

            //Añade una de ejemplo por si no hay ninguna
            ListaResumenes.Add(new Resumenes()
            {
                Nombre = "Regla transformación en garou",
                Etiquetas = "Hombre lobo, garou, mundo de tinieblas",
                NombreTipoAventura = "D&D 3.5",
                Descripcion = "Esta regla permite al hombre lobo pasar de su forma hominida a su forma garou obteniendo los diversos bonificadores",
                Pagina = 120,
                Manual = "Hombre Lobo 20 aniversario",
                ManualUrl = "http://www.meloinvento.com"
            });

            //Vincula el listview con la lista para que aparezcan sus miembros
            ResumenesListView.ItemsSource = ListaResumenes;
        }

        public void ActualizarLista()
        {
            ListaResumenes[ResumenesListView.SelectedIndex] = (Resumenes)ResumenesListView.SelectedItem;
        }

        public void RemoverDeLaLista()
        {
            ListaResumenes.RemoveAt(ResumenesListView.SelectedIndex);
        }

        private void ResumenesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("Seleccionado " + ResumenesListView.SelectedItem);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AnadirResumen anadir = new AnadirResumen(ListaResumenes);
            anadir.ShowDialog();
        }
    }
}
