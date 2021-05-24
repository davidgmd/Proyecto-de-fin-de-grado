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
            Traducir();
            
        }

        public ObservableCollection<Resumenes> ListaResumenes { get; set; } = new ObservableCollection<Resumenes>();
        public CollectionView view;


        public void Inicializar()
        {
            //Añade todos los resumenes de la campaña, escenario y aventura seleccionada
            foreach (Resumenes resumen in DatosAplicacion.CampanaSeleccionada.Recursos.Resumenes)
            {
                ListaResumenes.Add(resumen);
            }

            if (!(DatosAplicacion.EscenarioSeleccionado is null))
            foreach (Resumenes resumen in DatosAplicacion.EscenarioSeleccionado.Recursos.Resumenes)
                {
                    ListaResumenes.Add(resumen);
                }

            if (!(DatosAplicacion.AventuraSeleccionada is null))
                foreach (Resumenes resumen in DatosAplicacion.AventuraSeleccionada.Recursos.Resumenes)
                {
                    ListaResumenes.Add(resumen);
                }

            //Vincula el listview con la lista para que aparezcan sus miembros
            ResumenesListView.ItemsSource = ListaResumenes;

            view = (CollectionView)CollectionViewSource.GetDefaultView(ResumenesListView.ItemsSource);
            view.Filter = UserFilter;
        }

        public void Traducir()
        {
            if (ConfiguracionAplicacion.Default.Idioma.Equals("EN"))
            {
                RulesSearcherTextBlock.Text = "Rules Search";
                NombreFiltroTextBlock.Text = "Filter by name";
                AddButtonTextBlock.Text = "Add";
                EditButtonTextBlock.Text = "Edit";
                DeleteButtonTextBlock.Text = "Delete";
            }
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
            AnadirResumen editar = new AnadirResumen(ListaResumenes, resumen, ResumenesListView.SelectedIndex);
            editar.ShowDialog();
        }

        private void BotonEliminar_Click(object sender, RoutedEventArgs e)
        {
            
            Resumenes resumen = (Resumenes)ResumenesListView.SelectedItem;
            switch (resumen.TipoAventura)
            {
                case "Campana":
                    DatosAplicacion.CampanaSeleccionada.EliminarResumen(resumen);
                    break;

                case "Escenario":
                    DatosAplicacion.EscenarioSeleccionado.EliminarResumen(resumen);
                    break;

                case "Aventura":
                    DatosAplicacion.AventuraSeleccionada.EliminarResumen(resumen);
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
                this.NombreFiltroTextBlock.Text = "Filter by labels";
            }
            else if (NombreFiltroTextBlock.Text.ToLower().Contains("etiquetas"))
            {
                this.view.Filter = UserFilter;
                this.NombreFiltroTextBlock.Text = "Buscar por nombre";
            }
            else
            {
                this.view.Filter = UserFilter;
                this.NombreFiltroTextBlock.Text = "Filter by name";
            }
        }
    }
}
