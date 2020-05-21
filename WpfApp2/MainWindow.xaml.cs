using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ElEscribaDelDJ.Classes;
using ElEscribaDelDJ.View;
using Newtonsoft.Json.Linq;

namespace ElEscribaDelDJ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static JObject sesionusuario;

        public static JObject SesionUsuario
        {
            get { return sesionusuario; }
            set { sesionusuario = value; }
        }

        public MainWindow()
        {
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.9);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.9);
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();         

    }

        private void userTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.userText.Text == "Introduzca su nombre de usuario")
            {
                this.userText.SelectionStart = 0;
                this.userText.SelectionLength = this.userText.Text.Length;
                this.userText.Text = "";
            }
        }

        private void userTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.userText.Text == "")
            {
                this.userText.Text = "Introduzca su nombre de usuario";
                this.loginButton.IsEnabled = false;
            }
            else
            {
                this.loginButton.IsEnabled = true;
            }
        }

        private void registrarse_Click(object sender, RoutedEventArgs e)
        {

            //CrearCredenciales
            
        }

        private async void login_Click(object sender, RoutedEventArgs e)
        {
            //Declaraciones
            Usuario usuario = new Usuario();

            //Asignar los valores de usuario
            usuario.NombreUsuario = this.userText.Text;
            usuario.Clave = Encriptacion(this.passwordText.Password);
            usuario.ListCampaignes = new List<Campaign>();

            await this.ComprobarCredenciales (usuario.NombreUsuario, usuario.Clave);

            //Asignamos la sesión
            SesionUsuario = JObject.FromObject(usuario);
           
            menuPrincipal ventanaPrincipal = new menuPrincipal();
            ventanaPrincipal.Show();
            this.Hide(); 
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string url = "http://creativecommons.org/licenses/by-nc-sa/4.0/";
            url = url.Replace("&", "^&");
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }

        private void Credits_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Creditos ventana = new Creditos();

            ventana.Show();
        }

        private string Encriptacion(string inputString)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(inputString);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);
            return hash;
        }

        private async void CrearCredenciales()
        {
            GitHub prueba = new GitHub();
            //await prueba.CrearCredenciales(usuario.NombreUsuario, usuario.Clave);
        }

        private async Task ComprobarCredenciales(string nombreusuario, string clave)
        {
            GitHub prueba = new GitHub();
            await prueba.ComprobarCredenciales(nombreusuario, clave);
        }
    }
}
