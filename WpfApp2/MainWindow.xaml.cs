using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2.View;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            }
            else
            {
                this.loginButton.IsEnabled = true;
            }
        }

        private void registrarse_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            menuPrincipal ventanaPrincipal = new menuPrincipal();

            ventanaPrincipal.Show();
        }
    }
}
