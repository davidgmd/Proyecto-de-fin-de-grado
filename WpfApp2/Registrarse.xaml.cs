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
            var regExp = new Regex("^[a-zA-Z]\\S*$");
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
                    this.MarcarIncorrecto(ImgUser, ErrorUser, "El nombre de usuario debe empezar \n por letras y sin espacios");
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

        private void PasswordBox_PasswordChange(object sender, RoutedEventArgs e)
        {
            var regExp = new Regex("^\\S+$");
            if (PasswordBoxText.Password == "")
            {
                this.MarcarIncorrecto(ImgPassword, ErrorPassword, "Introduzca la contraseña");
                lista.Remove(ImgPassword);
            }
            else
            {
                if (regExp.IsMatch(PasswordBoxText.Password))
                {
                    this.MarcarCorrecto(ImgPassword, ErrorPassword, "Contraseña valida");
                    lista.Add(ImgPassword);
                }
                else
                {
                    this.MarcarIncorrecto(ImgPassword, ErrorPassword, "La contraseña no puede contener \n espacios");
                    lista.Remove(ImgPassword);
                }
            }
            RoutedEventArgs es = new RoutedEventArgs();
            PasswordBox2_PasswordChanged(this, es);
        }

        private void PasswordBox2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordBox2.Password == "")
            {
                this.MarcarIncorrecto(ImgPassword2, ErrorPassword2, "Introduzca la contraseña");
                lista.Remove(ImgPassword2);
            }
            else
            {
                if (PasswordBox2.Password.Equals(PasswordBoxText.Password))
                {
                    this.MarcarCorrecto(ImgPassword2, ErrorPassword2, "Coinciden las contraseñas");
                    lista.Add(ImgPassword2);
                }
                else
                {
                    this.MarcarIncorrecto(ImgPassword2, ErrorPassword2, "La contraseñas no coinciden");
                    lista.Remove(ImgPassword2);
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {            
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow ventana = new MainWindow();
            ventana.Show();
        }

        private void CorreoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var regExp = new Regex("^\\S+@\\S+\\.\\S+$");
            if (CorreoTextBox.Text == "")
            {
                this.MarcarIncorrecto(ImgCorreo, ErrorCorreo, "Introduzca el correo");
                lista.Remove(ImgCorreo);
            }
            else
            {
                if (regExp.IsMatch(CorreoTextBox.Text))
                {
                    this.MarcarCorrecto(ImgCorreo, ErrorCorreo, "Contraseña valida");
                    lista.Add(ImgCorreo);
                }
                else
                {
                    this.MarcarIncorrecto(ImgCorreo, ErrorCorreo, "La contraseña no puede contener \n espacios y debe tener @ y .es \n Por ejemplo @hotmail.com");
                    lista.Remove(ImgCorreo);
                }
            }

            CorreoTextBox2_TextChanged(sender, e);
        }

        private void CorreoTextBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CorreoTextBox2.Text == "")
            {
                this.MarcarIncorrecto(ImgCorreo2, ErrorCorreo2, "Introduzca el correo");
                lista.Remove(ImgCorreo2);
            }
            else
            {
                if (CorreoTextBox2.Text.Equals(CorreoTextBox.Text))
                {
                    this.MarcarCorrecto(ImgCorreo2, ErrorCorreo2, "Coinciden los correos");
                    lista.Add(ImgCorreo2);
                }
                else
                {
                    this.MarcarIncorrecto(ImgCorreo2, ErrorCorreo2, "Los correos no coinciden");
                    lista.Remove(ImgCorreo2);
                }
            }
        }
    }
}
