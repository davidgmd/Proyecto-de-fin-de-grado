using ElEscribaDelDJ.Classes.Utilidades;
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

namespace ElEscribaDelDJ.View
{
    /// <summary>
    /// Lógica de interacción para AnadirCampana.xaml
    /// </summary>
    public partial class AnadirElemento : Window
    {
        public AnadirElemento()
        {
            InitializeComponent();
        }

        private void SeleccionarImagen_Click(object sender, RoutedEventArgs e)
        {
            SelectorArchivos archivo = new SelectorArchivos();
            var direccion = archivo.SeleccionImagen();
            ImagenElegida.Source = new BitmapImage(new Uri(direccion, UriKind.Absolute));
        }
    }
}
