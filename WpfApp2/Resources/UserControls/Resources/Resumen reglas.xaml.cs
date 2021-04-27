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
            
        }

        public ObservableCollection<Resumenes> ListaResumenes { get; set; } = new ObservableCollection<Resumenes>();
        public Dictionary<string, int> Indices = new Dictionary<string, int>();
        public CollectionView view;


        public void Inicializar()
        {
            //Añade todos los resumenes de la campaña, escenario y aventura seleccionada
            int indicereal = 0;
            foreach (Resumenes resumen in DatosAplicacion.CampanaSeleccionada.Recursos.Resumenes)
            {
                resumen.NombreTipoAventura = DatosAplicacion.CampanaSeleccionada.Nombre;
                resumen.TipoAventura = "Campana";
                ListaResumenes.Add(resumen);
                Indices.Add(resumen.Nombre, indicereal);
                indicereal++;
            }

            indicereal = 0;
            if (!(DatosAplicacion.EscenarioSeleccionado is null))
            foreach (Resumenes resumen in DatosAplicacion.EscenarioSeleccionado.Recursos.Resumenes)
            {
                resumen.NombreTipoAventura = DatosAplicacion.EscenarioSeleccionado.Nombre;
                resumen.TipoAventura = "Escenario";
                ListaResumenes.Add(resumen);
                Indices.Add(resumen.Nombre, indicereal);
                indicereal++;
                }

            indicereal = 0;
            if (!(DatosAplicacion.AventuraSeleccionada is null))
                foreach (Resumenes resumen in DatosAplicacion.AventuraSeleccionada.Recursos.Resumenes)
            {
                resumen.NombreTipoAventura = DatosAplicacion.AventuraSeleccionada.Nombre;
                resumen.TipoAventura = "Aventura";
                ListaResumenes.Add(resumen);
                Indices.Add(resumen.Nombre, indicereal);
                indicereal++;
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

            view = (CollectionView)CollectionViewSource.GetDefaultView(ResumenesListView.ItemsSource);
            view.Filter = UserFilter;
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(BuscadorResumenesTextBox.Text))
                return true;
            else
                return ((item as Resumenes).Nombre.IndexOf(BuscadorResumenesTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private bool LabelsFilter(object item)
        {
            if (String.IsNullOrEmpty(BuscadorResumenesTextBox.Text))
                return true;
            else
                return ((item as Resumenes).Etiquetas.IndexOf(BuscadorResumenesTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }



        private void ResumenesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ResumenesListView.SelectedIndex >= 0) 
            { 
                BotonEditar.IsEnabled = true;
                BotonEliminar.IsEnabled = true;
            }
            else
            {
                BotonEditar.IsEnabled = false;
                BotonEliminar.IsEnabled = false;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AnadirResumen anadir = new AnadirResumen(ListaResumenes);
            anadir.ShowDialog();
        }

        private void BotonEditar_Click(object sender, RoutedEventArgs e)
        {
            Resumenes resumen = (Resumenes)ResumenesListView.SelectedItem;
            AnadirResumen editar = new AnadirResumen(ListaResumenes, resumen, Indices[resumen.Nombre]);
            editar.ShowDialog();
        }

        private void BotonEliminar_Click(object sender, RoutedEventArgs e)
        {
            
            Resumenes resumen = (Resumenes)ResumenesListView.SelectedItem;
            switch (resumen.TipoAventura)
            {
                case "Campana":
                    DatosAplicacion.CampanaSeleccionada.EliminarResumen(Indices[resumen.Nombre]);
                    break;

                case "Escenario":
                    DatosAplicacion.EscenarioSeleccionado.EliminarResumen(Indices[resumen.Nombre]);
                    break;

                case "Aventura":
                    DatosAplicacion.AventuraSeleccionada.EliminarResumen(Indices[resumen.Nombre]);
                    break;
                default:
                    break;
            }

            ListaResumenes.RemoveAt(ResumenesListView.SelectedIndex);
        }

        private void BuscadorResumenesTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ResumenesListView.ItemsSource).Refresh();
        }

        private void BotonFiltro_Click(object sender, RoutedEventArgs e)
        {
            if (NombreFiltroTextBlock.Text.ToLower().Contains("nombre"))
            {
                this.view.Filter = LabelsFilter;
                this.NombreFiltroTextBlock.Text = "Buscar por etiquetas";
            }
            else if (NombreFiltroTextBlock.Text.ToLower().Contains("name"))
            {
                this.view.Filter = LabelsFilter;
                this.NombreFiltroTextBlock.Text = "Search for labels";
            }
            else if (NombreFiltroTextBlock.Text.ToLower().Contains("etiquetas"))
            {
                this.view.Filter = UserFilter;
                this.NombreFiltroTextBlock.Text = "Buscar por nombre";
            }
            else
            {
                this.view.Filter = UserFilter;
                this.NombreFiltroTextBlock.Text = "Search for name";
            }
        }
    }
}
