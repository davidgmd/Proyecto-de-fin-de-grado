using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
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

namespace ElEscribaDelDJ.View.Options
{
    /// <summary>
    /// Lógica de interacción para VentanaOpciones.xaml
    /// </summary>
    public partial class VentanaOpciones : Window
    {
        public VentanaOpciones()
        {
            InitializeComponent();
            ConfiguracionPagina.DefinirIdioma(this, "Options");
        }

        private void ActualizarContenidoLocal_MouseEnter(object sender, MouseEventArgs e)
        {
            ActualizarContenidoLocal.Foreground = Brushes.Black;
        }

        private void ActualizarContenidoLocal_MouseLeave(object sender, MouseEventArgs e)
        {
            ActualizarContenidoLocal.Foreground = Brushes.White;
        }

        private void UploadButton_MouseEnter(object sender, MouseEventArgs e)
        {
            UploadButton.Foreground = Brushes.Black;
        }

        private void UploadButton_MouseLeave(object sender, MouseEventArgs e)
        {
            UploadButton.Foreground = Brushes.White;
        }
    }
}
