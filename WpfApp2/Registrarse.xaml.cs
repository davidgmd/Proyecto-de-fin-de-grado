using ElEscribaDelDJ.Classes;
using ElEscribaDelDJ.Classes.Utilidades;
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
            ConfiguracionPagina.DefinirIdioma(this, "SignUp");
        }

        private async void User_LostFocus(object sender, RoutedEventArgs e)
        {
            var regExp = new Regex("^[a-zA-Z]\\S*$");
            if ((this.userText.Text != "") && (regExp.IsMatch(userText.Text)))
            {
                if (await MainWindow.gitHub.UsuarioExiste(this.userText.Text) == false)
                {
                    this.MarcarCorrecto(ImgUser, ErrorUser, this.FindResource("UserNameAvaible").ToString());
                    anadirLista(ImgUser);    
                }
                else
                {
                    this.MarcarIncorrecto(ImgUser, ErrorUser, this.FindResource("UserAlreadyExist").ToString());
                    lista.Remove(ImgUser);
                }
            }
            HabilitarBoton();
        }

        private void UserText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var regExp = new Regex("^[a-zA-Z]\\S*$");
            if (userText.Text == "")
            {
                this.MarcarIncorrecto(ImgUser, ErrorUser, this.FindResource("UserEmpty").ToString());
            }
            else 
            {
                if (regExp.IsMatch(userText.Text))
                {
                    ImgUser.Visibility = Visibility.Hidden;
                    ErrorUser.Visibility = Visibility.Visible;
                    ErrorUser.Foreground = Brushes.White;
                    ErrorUser.Text = this.FindResource("UserNameChanged").ToString();
                }
                else
                {
                    this.MarcarIncorrecto(ImgUser, ErrorUser, this.FindResource("RulesForUser").ToString());
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
                this.MarcarIncorrecto(ImgPassword, ErrorPassword, this.FindResource("PasswordEmpty").ToString());
                lista.Remove(ImgPassword);
            }
            else
            {
                if (regExp.IsMatch(PasswordBoxText.Password))
                {
                    this.MarcarCorrecto(ImgPassword, ErrorPassword, this.FindResource("ValidPassword").ToString());
                    anadirLista(ImgPassword);
                }
                else
                {
                    this.MarcarIncorrecto(ImgPassword, ErrorPassword, this.FindResource("RulesForPasword").ToString());
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
                this.MarcarIncorrecto(ImgPassword2, ErrorPassword2, this.FindResource("PasswordEmpty").ToString());
                lista.Remove(ImgPassword2);
            }
            else
            {
                if (PasswordBox2.Password.Equals(PasswordBoxText.Password))
                {
                    this.MarcarCorrecto(ImgPassword2, ErrorPassword2, this.FindResource("ValidRepeatPassword").ToString());
                    anadirLista(ImgPassword2);
                }
                else
                {
                    this.MarcarIncorrecto(ImgPassword2, ErrorPassword2, this.FindResource("PasswordUnMatch").ToString());
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
                this.MarcarIncorrecto(ImgCorreo, ErrorCorreo, this.FindResource("EmailEmpty").ToString());
                lista.Remove(ImgCorreo);
            }
            else
            {
                if (regExp.IsMatch(CorreoTextBox.Text))
                {
                    this.MarcarCorrecto(ImgCorreo, ErrorCorreo, this.FindResource("ValidEmail").ToString());
                    anadirLista(ImgCorreo);
                }
                else
                {
                    this.MarcarIncorrecto(ImgCorreo, ErrorCorreo, this.FindResource("RulesForEmail").ToString());
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
                this.MarcarIncorrecto(ImgCorreo2, ErrorCorreo2, this.FindResource("EmailEmpty").ToString());
                lista.Remove(ImgCorreo2);
            }
            else
            {
                if (CorreoTextBox2.Text.Equals(CorreoTextBox.Text))
                {
                    this.MarcarCorrecto(ImgCorreo2, ErrorCorreo2, this.FindResource("ValidRepeatEmail").ToString());
                    anadirLista(ImgCorreo2);
                }
                else
                {
                    this.MarcarIncorrecto(ImgCorreo2, ErrorCorreo2, this.FindResource("EmailUnMatch").ToString());
                    lista.Remove(ImgCorreo2);
                }
            }

            HabilitarBoton();
        }

        private void MarcarCorrecto(Image elementoimagen, TextBlock elementotexto, string texto)
        {
            elementoimagen.Visibility = Visibility.Visible;
            elementotexto.Visibility = Visibility.Visible;
            elementoimagen.Source = new BitmapImage(new Uri("Images/icons/icons8-verificado.png", UriKind.Relative));
            elementotexto.Text = texto;
            elementotexto.Foreground = Brushes.Green;
        }

        private void MarcarIncorrecto(Image elementoimagen, TextBlock elementotexto, string texto)
        {
            elementoimagen.Visibility = Visibility.Visible;
            elementotexto.Visibility = Visibility.Visible;
            elementoimagen.Source = new BitmapImage(new Uri("Images/icons/icons8-incorrecto.png", UriKind.Relative));
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
            var exito = emailRegistro.SendEmail(CorreoTextBox.Text, this.FindResource("EmailSubject").ToString(), string.Format(this.FindResource("EmailMessage").ToString(), this.userText.Text, codigo));
            if (exito)
            {
                System.Windows.MessageBox.Show(string.Format(this.FindResource("UserSignUpSucessfull").ToString(), this.CorreoTextBox.Text));
                this.introducirCodigo.Visibility = Visibility.Visible;
            }
            else
            {
                System.Windows.MessageBox.Show(string.Format(this.FindResource("UserSignUpFailed").ToString(), this.CorreoTextBox.Text));
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
                System.Windows.MessageBox.Show(this.FindResource("RightCode").ToString());
                this.Close();
            }
            else
            {
                System.Windows.MessageBox.Show(this.FindResource("WrongCode").ToString());
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
            usuario.ListCampaigns = new List<Campana>();
            anadirelementosiniciales(usuario.ListCampaigns);

            //guardamos en la sesión el usuario y creamos las credenciales en git
            await MainWindow.gitHub.CrearCredenciales(nombreusuario, clave, JsonUtils.DeUserAJsonObject(usuario));

            //cambiar a .user al finalizar las pruebas
            GestionArchivos.EscribirArchivo("Usuarios", usuario.NombreUsuario, JsonUtils.DeUserAJsonObject(usuario).ToString());
        }

        //Funcion que inicializa los campos de campaña
        private void anadirelementosiniciales(List<Campana> listacampana)
        {
            Campana campana = new Campana();
            campana.Nombre = "D&D 3.5";
            campana.Descripcion = "Elemento creado como base para aventuras de Dungeons and dragons 3.5";
            campana.DireccionImagen = "/Images/icons/D&D.png";
            listacampana.Add(campana);

            campana = new Campana();
            campana.Nombre = "Warhammer 2ª edición";
            campana.Descripcion = "Elemento creado como base para aventuras de Warhammer 2ª edición";
            campana.DireccionImagen = "/Images/icons/warhammer-removebg.png";
            listacampana.Add(campana);
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
