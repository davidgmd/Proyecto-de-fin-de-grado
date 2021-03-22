using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ElEscribaDelDJ.View.Roller
{
    /// <summary>
    /// Lógica de interacción para VentanaModal.xaml
    /// </summary>
    public partial class VentanaModal : Window
    {
        public VentanaModal()
        {
            InitializeComponent();
            ConfiguracionPagina.DefinirIdioma(this, "Roller");
        }

        private void BotonDadinos_Click(object sender, RoutedEventArgs e)
        {
            Dadinos lanzador = new Dadinos();
            lanzador.Show();
        }

        private void BotonNavegador_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://dados.tr4ck.net#!/";
            url = url.Replace("&", "^&");
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }

        private void BotonNavegador_MouseEnter(object sender, MouseEventArgs e)
        {
            BotonNavegador.Foreground = Brushes.Black;
        }

        private void BotonDadinos_MouseEnter(object sender, MouseEventArgs e)
        {
            BotonDadinos.Foreground = Brushes.Black;
        }

        private void BotonDadinos_MouseLeave(object sender, MouseEventArgs e)
        {
            BotonDadinos.Foreground = Brushes.White;
        }

        private void BotonNavegador_MouseLeave(object sender, MouseEventArgs e)
        {
            BotonNavegador.Foreground = Brushes.White;
        }
    }
}
