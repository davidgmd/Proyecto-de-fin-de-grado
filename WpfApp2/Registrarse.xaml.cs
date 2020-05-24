using ElEscribaDelDJ.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
        private List<Object> lista = new List<Object>();

        public Registrarse()
        {
            InitializeComponent();


        }

        private async void User_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.userText.Text != "")
            {
                if (await MainWindow.gitHub.UsuarioExiste(this.userText.Text) == false)
                {
                    this.MarcarCorrecto(ImgUser, ErrorUser, "Nombre de usuario disponible");
                    lista.Add(ImgUser);
                }
                else
                {
                    this.MarcarIncorrecto(ImgUser, ErrorUser, "Ya existe alguien con ese usuario");
                    lista.Remove(ImgUser);
                }
            }                     
        }

        private void userText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var regExp = new Regex("^[a-zA-Z][a-zA-Z0-9]*$");
            if (userText.Text == "")
            {
                this.MarcarIncorrecto(ImgUser, ErrorUser, "Introduzca el nombre de usuario");
            }
            else 
            {
                if (regExp.IsMatch(userText.Text))
                {
                    ImgUser.Visibility = Visibility.Hidden;
                    ErrorUser.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.MarcarIncorrecto(ImgUser, ErrorUser, "El nombre de usuario debe empezar \n por letras y no usar signos especiales");
                }    
            }           
        }

        private void MarcarCorrecto(Image elementoimagen, TextBlock elementotexto, string texto)
        {
            elementoimagen.Visibility = Visibility.Visible;
            elementotexto.Visibility = Visibility.Visible;
            elementoimagen.Source = new BitmapImage(new Uri("/images/icons/icons8-verificado.png", UriKind.Relative));
            elementotexto.Text = texto;
            elementotexto.Foreground = Brushes.Green;
        }

        private void MarcarIncorrecto(Image elementoimagen, TextBlock elementotexto, string texto)
        {
            elementoimagen.Visibility = Visibility.Visible;
            elementotexto.Visibility = Visibility.Visible;
            elementoimagen.Source = new BitmapImage(new Uri("/images/icons/icons8-incorrecto.png", UriKind.Relative));
            elementotexto.Text = texto;
            elementotexto.Foreground = Brushes.Red;
        }
    }
}
