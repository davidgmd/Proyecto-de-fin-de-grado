using ElEscribaDelDJ.Classes;
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

namespace ElEscribaDelDJ
{
    /// <summary>
    /// Lógica de interacción para Registrarse.xaml
    /// </summary>
    public partial class Registrarse : Window
    {
        public Registrarse()
        {
            InitializeComponent();
        }

        private async void User_LostFocus(object sender, RoutedEventArgs e)
        {
            
            if (await MainWindow.gitHub.UsuarioExiste(this.userText.Text) == true)
            {
                ImgUser.Source = new BitmapImage(new Uri("/images/icons/icons8-verificado.png", UriKind.Relative));
                ErrorUser.Text = "";
                AceptarButton.IsEnabled = true;
            }
            else
            {
                ImgUser.Source = new BitmapImage(new Uri("/images/icons/icons8-incorrecto.png", UriKind.Relative));
                ErrorUser.Text = "Ese nombre de usuario ya existe";
                AceptarButton.IsEnabled = false;
            }
        }
    }
}
