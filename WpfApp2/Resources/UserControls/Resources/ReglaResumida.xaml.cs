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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ElEscribaDelDJ.Resources.UserControls.Resources
{
    /// <summary>
    /// Lógica de interacción para ReglaResumida.xaml
    /// </summary>
    public partial class ReglaResumida : UserControl
    {
        public ReglaResumida()
        {
            InitializeComponent();
            Traducir();
        }

        private void BotonMasDetalles_Click(object sender, RoutedEventArgs e)
        {
            if (StackPanelDetalles.Visibility.Equals(Visibility.Collapsed)){
                StackPanelDetalles.Visibility = Visibility.Visible;
                ImagenBotonMasDetalles.Source = new BitmapImage(new Uri("/images/icons/iconoMenos.png", UriKind.Relative));
            }
            else
            {
                StackPanelDetalles.Visibility = Visibility.Collapsed;
                ImagenBotonMasDetalles.Source = new BitmapImage(new Uri("/images/icons/iconoMas.png", UriKind.Relative));
            }
        }

        private void ManualTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", this.ManualTextBlock.Text);
        }

        private void UrlTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string url = this.UrlTextBlock.Text.Replace("&", "^&");
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }

        public void Traducir()
        {
            NameToShowTextBlock.Text = "Name to Show:";
            LabelsTextBlock.Text = "Labels:";
            DetallesTextBlock.Text = "Details";
            ReferenceToTextBlock.Text = "Makes references to:";
            DescriptionTextBlock.Text = "Description:";
            PaginaTextBlock.Text = "Page:";
            BookTextBlock.Text = "Book";
            UrlLabel.Text = "Book Url:";
        }
    }
}
