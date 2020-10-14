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
        private menuPrincipal ventana;

        public VentanaOpciones(menuPrincipal ventanaprincipal)
        {
            InitializeComponent();
            ConfiguracionPagina.DefinirIdioma(this, "Options");
            ventana = ventanaprincipal;
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

        private void CheckUpdates_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("No hay actualizaciones pendientes");
        }

        private void IdiomaES_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ConfiguracionAplicacion.Default.Idioma = "ES";
            ConfiguracionPagina.DefinirIdioma(this, "Options");
            GuardarConfiguracion();
        }

        private void IdiomaEN_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ConfiguracionAplicacion.Default.Idioma = "EN";
            ConfiguracionPagina.DefinirIdioma(this, "Options");
            GuardarConfiguracion();
        }

        //Cuando se marca alguna de las casillas se llama a esta función para guardar el cambio en la 
        //configuracion inicial .ini
        private void GuardarConfiguracion()
        {
            string[] valoresinicialesconf = new string[3];

            valoresinicialesconf[0] = "Language:" + ConfiguracionAplicacion.Default.Idioma;
            valoresinicialesconf[1] = "RememberUser:" + ConfiguracionAplicacion.Default.RecordarUsuario.ToString();
            valoresinicialesconf[2] = "RememberLogin:" + ConfiguracionAplicacion.Default.RecordarLogin.ToString();
            System.IO.File.WriteAllLines(RecursosAplicacion.DireccionBase + "Settings.ini", valoresinicialesconf);
        }

        private void ActualizarContenidoLocal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ventana.CambiarIdioma();
        }
    }
}
