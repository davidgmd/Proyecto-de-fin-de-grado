using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ElEscribaDelDJ.Classes;
using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
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

        private static GitHub github;


        public static GitHub gitHub
        {
            get { return github; }
            set { github = value; }
        }

        public static JObject SesionUsuario
        {
            get { return sesionusuario; }
            set { sesionusuario = value; }
        }

        public MainWindow()
        {
            //Modificamos la posición de la pantalla y su alto y ancho para que sean un 90% del tamaño de la pantalla
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.9);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.9);
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //Se crean los componentes
            InitializeComponent();

            //Creamos la sesión de github que se va a mantener por toda la aplicación
            MainWindow.gitHub = GitHub.GithubInstancia;

            //Indicamos que va a haber un diccionario de recursos y su dirección
            ResourceDictionary dict = new ResourceDictionary();
            //Según el idioma cargamos uno u otro
            var path = RecursosAplicacion.DireccionBase + "\\Idiomas\\ES\\" + "Login.xaml";

            if (ConfiguracionAplicacion.Default.Idioma.Equals("ES"))
            {
                path = RecursosAplicacion.DireccionBase + "\\Idiomas\\ES\\" + "Login.xaml";
            }
            else
            {
                path = RecursosAplicacion.DireccionBase + "\\Idiomas\\EN\\" + "Login.xaml";
            }
            
            dict.Source = new Uri(path, UriKind.Absolute);
            this.Resources.MergedDictionaries.Add(dict);
        }

        private void userTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var prueba = this.FindResource("UserText").ToString();
            if (this.userText.Text.Equals(prueba))
            {
                this.userText.SelectionStart = 0;
                this.userText.SelectionLength = this.userText.Text.Length;
                this.userText.Text = "";
            }
        }

        private void userText_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (this.passwordText is object)
            {
                ComprobarCambios(passwordText.Password, userText.Text);
            }                 
        }

        private void passwordText_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ComprobarCambios(passwordText.Password, userText.Text);
        }

        private void ComprobarCambios(string password, string username)
        {
            if (password != "" && username != "")
            {
                loginButton.IsEnabled = true;
            }
            else
            {
                loginButton.IsEnabled = false;
            }
        }

        private void userTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.userText.Text == "")
            {
                this.userText.Text = this.FindResource("UserText").ToString();
            }
        }

        private void registrarse_Click(object sender, RoutedEventArgs e)
        {
            Registrarse ventanaRegistro = new Registrarse();
            ventanaRegistro.Show();
            this.Hide();
        }

        private async void login_Click(object sender, RoutedEventArgs e)
        {
            //Declaraciones
            Usuario usuario = new Usuario();

            //Asignar los valores de usuario
            usuario.NombreUsuario = this.userText.Text;
            usuario.Clave = Encriptacion(this.passwordText.Password);
            usuario.ListCampaignes = new List<Campaign>();

            if (await this.ComprobarCredenciales (usuario.NombreUsuario, usuario.Clave))
            {
                //Asignamos la sesión
                SesionUsuario = JObject.FromObject(usuario);

                System.Windows.MessageBox.Show(this.FindResource("RightLogin").ToString());

                //mostramos la ventana del menu
                menuPrincipal ventanaPrincipal = new menuPrincipal();
                ventanaPrincipal.Show();
                this.Hide();
            }
            else
            {
                System.Windows.MessageBox.Show(this.FindResource("ErrorUser").ToString());
            }    
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            System.Environment.Exit(0);
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

        private async Task<Boolean> ComprobarCredenciales(string nombreusuario, string clave)
        {
            //Comprobamos las credenciales online
            var respuesta = await MainWindow.github.ComprobarCredenciales(nombreusuario, clave);
            return respuesta;
        }
    }
}
