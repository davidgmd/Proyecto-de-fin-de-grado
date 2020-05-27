using ElEscribaDelDJ.Classes;
using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ElEscribaDelDJ
{
    /// <summary>
    /// Lógica de interacción para Registrarse.xaml
    /// </summary>
    public partial class Registrarse : Window
    {
        private List<Object> lista = new List<Object>();
        private string codigo;

        public Registrarse()
        {
            InitializeComponent();
        }

        private async void User_LostFocus(object sender, RoutedEventArgs e)
        {
            var regExp = new Regex("^[a-zA-Z]\\S*$");
            if ((this.userText.Text != "") && (regExp.IsMatch(userText.Text)))
            {
                if (await MainWindow.gitHub.UsuarioExiste(this.userText.Text) == false)
                {
                    this.MarcarCorrecto(ImgUser, ErrorUser, "Nombre de usuario disponible");
                    anadirLista(ImgUser);    
                }
                else
                {
                    this.MarcarIncorrecto(ImgUser, ErrorUser, "Ya existe alguien con ese usuario");
                    lista.Remove(ImgUser);
                }
            }
            HabilitarBoton();
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
                    ErrorUser.Visibility = Visibility.Visible;
                    ErrorUser.Foreground = Brushes.White;
                    ErrorUser.Text = "Cuando haya terminado de escribir el usuario \n pulse en el siguiente campo para comprobar \n la disponibilidad";
                }
                else
                {
                    this.MarcarIncorrecto(ImgUser, ErrorUser, "El nombre de usuario debe empezar \n por letras y sin espacios");
                }    
            }

            lista.Remove(ImgUser);
            HabilitarBoton();
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
                    anadirLista(ImgPassword);
                }
                else
                {
                    this.MarcarIncorrecto(ImgPassword, ErrorPassword, "La contraseña no puede contener \n espacios");
                    lista.Remove(ImgPassword);
                }
            }

            PasswordBox2_PasswordChanged(sender, e);
            HabilitarBoton();
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
                    anadirLista(ImgPassword2);
                }
                else
                {
                    this.MarcarIncorrecto(ImgPassword2, ErrorPassword2, "La contraseñas no coinciden");
                    lista.Remove(ImgPassword2);
                }
            }

            HabilitarBoton();

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
                    this.MarcarCorrecto(ImgCorreo, ErrorCorreo, "Correo Valido");
                    anadirLista(ImgCorreo);
                }
                else
                {
                    this.MarcarIncorrecto(ImgCorreo, ErrorCorreo, "La contraseña no puede contener \n espacios y debe tener @ y .es \n Por ejemplo @hotmail.com");
                    lista.Remove(ImgCorreo);
                }
            }

            CorreoTextBox2_TextChanged(sender, e);
            HabilitarBoton();
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
                    anadirLista(ImgCorreo2);
                }
                else
                {
                    this.MarcarIncorrecto(ImgCorreo2, ErrorCorreo2, "Los correos no coinciden");
                    lista.Remove(ImgCorreo2);
                }
            }

            HabilitarBoton();
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

        private void HabilitarBoton()
        {
            if (lista.Count.Equals(CamposRegistro.RowDefinitions.Count - 1))
            {
                AceptarButton.IsEnabled = true;
            }
            else
            {
                AceptarButton.IsEnabled = false;
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

        private void anadirLista(Image elementoimagen)
        {
            if (!lista.Contains(elementoimagen))
            {
                lista.Add(elementoimagen);
            }
        }

        private void AceptarButton_Click(object sender, RoutedEventArgs e)
        {
            this.codigo = this.generarcodigo();
            Email emailRegistro = new Email();       
            var exito = emailRegistro.sendEmail(CorreoTextBox.Text, "Usuario registrado con este correo en el escriba del dj", "Se ha solicitado" +
                $" registrarse con este correo por parte del usuario {userText.Text} debe introducir en el programa" +
                $" el código de registro {codigo}");
            if (exito)
            {
                System.Windows.MessageBox.Show($"Se ha enviado un correo a la dirección introducida {CorreoTextBox.Text} Revise el" +
                    $" correo para ver su código de registro el cual tendra que introducir, si no lo encuentra mire en la carpeta" +
                    $" de 'Spam' o 'Correo no deseado'");
                this.introducirCodigo.Visibility = Visibility.Visible;
            }
            else
            {
                System.Windows.MessageBox.Show("El envio a la dirección de correo ha fallado, revise que su conexión a internet " +
                    " funciona Correctamente y la dirección de correo introducida");
            }
        }

        private string generarcodigo()
        {
            StringBuilder builder = new StringBuilder();
            Randomizar random = new Randomizar(4, 6);
            int longitud = random.Longitud;

            for (int i = 0; i < longitud; i++)
            {          
                builder.Append(random.LetraAlAzar());
                builder.Append(random.NumeroAlAzar());              
            }

            return builder.ToString();
        }

        private void introducirButton_Click(object sender, RoutedEventArgs e)
        {
            if (codigoTextBox.Text.Equals(codigo))
            {
                CrearCredenciales(userText.Text, PasswordBoxText.Password);
                System.Windows.MessageBox.Show("usuario creado con exito");
                this.Close();
            }
            else
            {
                System.Windows.MessageBox.Show("El código introducido es incorrecto, revise el correo, si pulsa en aceptar de nuevo " +
                    "se le reenviara otro correo con otro código");
            }
        }

        private async void CrearCredenciales(string nombreusuario, string clave)
        {
            //creamos los datos del usuario y ciframos la contraseña y el correo con sha256 por motivos de seguridad
            Usuario usuario = new Usuario();
            usuario.NombreUsuario = userText.Text;
            usuario.Clave = PasswordBoxText.Password;
            usuario.Correo = Encriptacion(CorreoTextBox.Text);
            usuario.Clave = Encriptacion(PasswordBoxText.Password);

            //guardamos en la sesión el usuario y creamos las credenciales en git
            MainWindow.SesionUsuario = JObject.FromObject(usuario);
            await MainWindow.gitHub.CrearCredenciales(nombreusuario, clave);

            //cambiar a .user al finalizar las pruebas
            var path = RecursosAplicacion.DireccionBase + "\\Usuarios\\" + usuario.NombreUsuario + ".json";
            System.IO.File.WriteAllText(path, MainWindow.SesionUsuario.ToString());
        }

        private string Encriptacion(string inputString)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(inputString);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);
            return hash;
        }
    }
}
