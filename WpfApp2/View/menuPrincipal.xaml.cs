using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ElEscribaDelDJ.View
{
    /// <summary>
    /// Lógica de interacción para menuPrincipal.xaml
    /// </summary>
    public partial class menuPrincipal : Window
    {
        public menuPrincipal()
        {
            InitializeComponent();
        }

        private void CampaigneComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Application.Current.Resources["borderMenu"] = Application.Current.Resources["borderMenuEnabled"];
            Application.Current.Resources["botonMenu"] = Application.Current.Resources["botonMenuEnabled"];

            if(Application.Current.Resources["noVisible"] != Application.Current.Resources["iconMenu"])
            {
                Application.Current.Resources["visibleInicio"] = Application.Current.Resources["noVisible"];
                Application.Current.Resources["noVisible"] = Application.Current.Resources["iconMenu"];
            } 

            var item = (ComboBoxItem)this.campaigneComboBox.SelectedValue;
            var content = (TextBlock)item.Content;

            this.tituloCampana.Text = content.Text.Trim();

            switch (content.Text.Trim())
            {
                case "D&D 3.5":
                    
                    this.iconoCampaigne.Source = new BitmapImage(new Uri("/Images/icons/D&D.png", UriKind.Relative));
                    break;

                case "Warhammer":
                    this.iconoCampaigne.Source = new BitmapImage(new Uri("/Images/icons/warhammer-removebg.png", UriKind.Relative));
                    break;

                default:
                    MessageBox.Show(content.Text);
                    break;
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            System.Environment.Exit(0);
        }
    }
}
