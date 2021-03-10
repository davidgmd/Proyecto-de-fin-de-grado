using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ElEscribaDelDJ.View.CampaignResources
{
    /// <summary>
    /// Lógica de interacción para MenuRecursos.xaml
    /// </summary>
    public partial class MenuRecursos : Window
    {
        public MenuRecursos()
        {
            InitializeComponent();
        }

        private void MostrarStackPanelDetalles(StackPanel panel)
        {
            if (panel.IsVisible) {
                panel.Visibility = Visibility.Collapsed;
            }
            else
            {             
                panel.Visibility = Visibility.Visible;
            }
        }

        private void MostrarScrollViewer(ScrollViewer panel)
        {
            if (panel.IsVisible)
            {
                panel.Visibility = Visibility.Collapsed;
            }
            else
            {               
                panel.Visibility = Visibility.Visible;
            }
        }

        private void BotonDetallesDocumentos_Click(object sender, RoutedEventArgs e)
        {
            MostrarScrollViewer(ScrollViewerDocumentos);
        }

        private void BotonDetallesReglas_Click(object sender, RoutedEventArgs e)
        {
            MostrarScrollViewer(ScrollViewerReglas);
        }
    }
}
