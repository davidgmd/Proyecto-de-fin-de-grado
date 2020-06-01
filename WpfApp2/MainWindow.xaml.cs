﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ElEscribaDelDJ.Classes;
using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using ElEscribaDelDJ.View;
using Newtonsoft.Json;
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
        private string passwordlog = "";
        private string[] valoresinicialesconf;
        private static Usuario datosusuario;

        public static Usuario DatosUsuario
        {
            get { return datosusuario; }
            set { datosusuario = value; }
        }


        public string[] ValoresInicialesConfiguracion
        {
            get { return valoresinicialesconf; }
            set { valoresinicialesconf = value; }
        }

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

            //Leemos la configuracion inicial
            this.ConfiguracionInicial();

            //Comprobamos si esta activada la casilla de recordar usuario o login
            //En caso afirmativo cargamos los datos y marcamos la casilla correspondiente
            if (ConfiguracionAplicacion.Default.RecordarUsuario || ConfiguracionAplicacion.Default.RecordarLogin)
                this.LoginInicial();

            //Indicamos que va a haber un diccionario de recursos y su dirección        
            ConfiguracionPagina.DefinirIdioma(this, "Login");
        }

        //Se ejecuta si hay que recordarusuario o login, marca el que sea, y lee el login buscando el ultimo
        //login, una vez encontrado, añade el nombre de usuario y/o la contraseña y la información suficiente
        //para que sepamos que no hay que volver a cifrar la contraseña
        private void LoginInicial()
        {
            if (ConfiguracionAplicacion.Default.RecordarUsuario)
            {
                this.RememberUserCheck.IsChecked = true;
                string[] ultimologin = Logs.LeerLog("login", "Login exitoso", this.FindResource("ErrorUser").ToString());
                if (ultimologin != null)
                {
                    this.userText.Text = ultimologin[0];
                }
            }
            else
            {
                this.RememberLoginCheck.IsChecked = true;
                string[] ultimologin = Logs.LeerLog("login", "Login exitoso", this.FindResource("ErrorUser").ToString());
                if (ultimologin != null)
                {
                    this.userText.Text = ultimologin[0];
                    this.passwordText.Password = ultimologin[1];
                    this.passwordlog = ultimologin[1];
                }
            }
        }

        //Lee la configuración inicial
        private void ConfiguracionInicial()
        {
            this.valoresinicialesconf = System.IO.File.ReadAllLines(RecursosAplicacion.DireccionBase + "Settings.ini");
            int i = 0;
            foreach (string cadenainicial in this.valoresinicialesconf)
            {      
                var cadenaresultante = cadenainicial.Split(':');
                this.valoresinicialesconf[i] = cadenaresultante[1].Trim().ToString();
                i += 1;
            }

            Regex.Replace(this.valoresinicialesconf[1], @"\s+", "");

            ConfiguracionAplicacion.Default.Idioma = this.valoresinicialesconf[0];            
            ConfiguracionAplicacion.Default.RecordarUsuario = Convert.ToBoolean(this.valoresinicialesconf[1].Trim());
            ConfiguracionAplicacion.Default.RecordarLogin = Convert.ToBoolean(this.valoresinicialesconf[2].Trim());
        }

        //Cuando se marca alguna de las casillas se llama a esta función para guardar el cambio en la 
        //configuracion inicial .ini
        private void GuardarConfiguracion()
        {
            this.valoresinicialesconf[0] = "Language:" + ConfiguracionAplicacion.Default.Idioma;
            this.valoresinicialesconf[1] = "RememberUser:" + ConfiguracionAplicacion.Default.RecordarUsuario.ToString();
            this.valoresinicialesconf[2] = "RememberLogin:" + ConfiguracionAplicacion.Default.RecordarLogin.ToString();
            System.IO.File.WriteAllLines(RecursosAplicacion.DireccionBase + "Settings.ini", this.valoresinicialesconf);
        }

        //Si el campo usuario obtiene el foco selecciona el texto inicial y lo borra, esto simula un placeholder
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

        //Si se cambia el texto comprueba si hay algo en el campo password en caso afirmativo se deslboquea el boton login
        private void userText_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (this.passwordText is object)
            {
                ComprobarCambios(passwordText.Password, userText.Text);
            }                 
        }

        //Si el texto de usuario pierde el foco comprueba si es vacio para advertir de que debe tener algún valor
        private void userTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.userText.Text == "")
            {
                this.userText.Text = this.FindResource("UserText").ToString();
            }
        }

        //lo mismo que usuario pero al reves si el campo usuario no esta vacio, se desbloquea login 
        private void passwordText_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ComprobarCambios(passwordText.Password, userText.Text);
        }

        //Funcion que comprueba los campos que no sean vacios y que no sea el texto inicial
        private void ComprobarCambios(string password, string username)
        {
            if (password != "" && username != "" && username != this.FindResource("UserText").ToString())
            {
                loginButton.IsEnabled = true;
            }
            else
            {
                loginButton.IsEnabled = false;
            }
        }     

        //Funcion que llama a la ventana de registro
        private void registrarse_Click(object sender, RoutedEventArgs e)
        {
            Registrarse ventanaRegistro = new Registrarse();
            ventanaRegistro.Show();
            this.Hide();
        }

        //Funcion de login
        private async void login_Click(object sender, RoutedEventArgs e)
        {
            //Declaraciones
            Usuario usuario = new Usuario();

            //Asignar los valores de usuario
            usuario.NombreUsuario = this.userText.Text;
            
            if (!this.passwordlog.Equals("") && this.passwordText.Password.Equals(passwordlog))
            {
                usuario.Clave = this.passwordlog;
            }
            else
            {
                usuario.Clave = Encriptacion(this.passwordText.Password);
            }
            
            if(File.Exists(RecursosAplicacion.DireccionBase + "\\Usuarios\\" + usuario.NombreUsuario + ".json"))
            {
                string datosusuario = File.ReadAllText(RecursosAplicacion.DireccionBase + "\\Usuarios\\" + usuario.NombreUsuario + ".json");
                Usuario usuario1 = JsonConvert.DeserializeObject<Usuario>(datosusuario);
                usuario.ListCampaigns = usuario1.ListCampaigns;
            }
            else
            {
                usuario.ListCampaigns = new List<Campana>();
                anadirelementosiniciales(usuario.ListCampaigns);
            }
            

            string[] campos = { usuario.NombreUsuario, usuario.Clave };

            if (await this.ComprobarCredenciales (usuario.NombreUsuario, usuario.Clave))
            {
                //Asignamos la sesión
                SesionUsuario = JObject.FromObject(usuario);
                MainWindow.datosusuario = usuario;

                System.Windows.MessageBox.Show(this.FindResource("RightLogin").ToString());
                Logs.GenerarLog("Intento de login", campos, "login", "Login exitoso");

                //mostramos la ventana del menu
                menuPrincipal ventanaPrincipal = new menuPrincipal();
                ventanaPrincipal.Show();
                this.Hide();
            }
            else
            {
                System.Windows.MessageBox.Show(this.FindResource("ErrorUser").ToString());
                Logs.GenerarLog("Intento de login", campos, "login", this.FindResource("ErrorUser").ToString());
            }    
        }

        //Boton salir
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            System.Environment.Exit(0);
        }

        //Si pulsamos en la imagen de los creditos nos llega a la información de la licencia
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string url = "http://creativecommons.org/licenses/by-nc-sa/4.0/";
            url = url.Replace("&", "^&");
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }

        //Si pulsamos en creditos nos muestra credits o creditos según idioma
        private void Credits_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string archivo;
            if (ConfiguracionAplicacion.Default.Idioma.Equals("ES"))
            {
                archivo = "creditos.txt";
            }
            else 
            {
                archivo = "credits.txt";
            }

            Creditos ventana = new Creditos(archivo);
            ventana.Show();
        }

        //Encripta la clave y el correo
        private string Encriptacion(string inputString)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(inputString);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);
            return hash;
        }

        //Metodo que cuando se intenta loguear comprueba que el usuario existe en github
        private async Task<Boolean> ComprobarCredenciales(string nombreusuario, string clave)
        {
            //Comprobamos las credenciales online
            var respuesta = await MainWindow.github.ComprobarCredenciales(nombreusuario, clave);
            return respuesta;
        }

        //Funcion que cambia el idioma a ingles al pulsar el stackpanel y guarda el cambio de conf
        private void IdiomaEN_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ConfiguracionAplicacion.Default.Idioma = "EN";
            ConfiguracionPagina.DefinirIdioma(this, "Login.xaml");
            GuardarConfiguracion();
        }

        //Funcion que cambia el idioma a español al pulsar el stackpanel y guarda el cambio de conf
        private void IdiomaES_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ConfiguracionAplicacion.Default.Idioma = "ES";
            ConfiguracionPagina.DefinirIdioma(this, "Login.xaml");
            GuardarConfiguracion();
        }

        //Si se marca la casilla de recordar usuario, desmarca la otra y guarda la conf
        private void RememberUserCheck_Checked(object sender, RoutedEventArgs e)
        {
            ConfiguracionAplicacion.Default.RecordarUsuario = true;
            if (this.RememberLoginCheck.IsChecked == true)
            {
                this.RememberLoginCheck.IsChecked = false;                
            }
            GuardarConfiguracion();
        }

        //Si es desmarcada recordar usuario cambia la conf
        private void RememberUserCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            ConfiguracionAplicacion.Default.RecordarUsuario = false;
            GuardarConfiguracion();
        }

        //Si es marcada recordar login desmarca la otra y guarda conf
        private void RememberLoginCheck_Checked(object sender, RoutedEventArgs e)
        {
            ConfiguracionAplicacion.Default.RecordarLogin = true;
            if (this.RememberUserCheck.IsChecked == true)
            {
                this.RememberUserCheck.IsChecked = false;
            }
            GuardarConfiguracion();
        }

        //Si es desmarcado recordar login guarda la conf
        private void RememberLoginCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            ConfiguracionAplicacion.Default.RecordarLogin = false;
            GuardarConfiguracion();
        }

        //Funcion que inicializa los campos de campaña
        private void anadirelementosiniciales(List<Campana> listacampana)
        {
            Campana campana = new Campana();
            campana.NombreCampana = "D&D 3.5";
            campana.Descripcion = "Elemento creado como base para aventuras de Dungeons and dragons 3.5";
            campana.DireccionImagen = "/Images/icons/D&D.png";
            listacampana.Add(campana);

            campana = new Campana();
            campana.NombreCampana = "Warhammer 2ª edición";
            campana.Descripcion = "Elemento creado como base para aventuras de Warhammer 2ª edición";
            campana.DireccionImagen = "/Images/icons/warhammer-removebg.png";
            listacampana.Add(campana);
        }
    }
}
