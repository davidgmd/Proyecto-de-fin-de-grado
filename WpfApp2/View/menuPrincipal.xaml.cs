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

namespace WpfApp2.View
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
            this.botonCalendario.IsEnabled = true;
            this.botonOpciones.IsEnabled = true;
            this.botonRecursos.IsEnabled = true;
            this.botonTirador.IsEnabled = true;

            var item = (ComboBoxItem)this.campaigneComboBox.SelectedValue;
                var content = (TextBlock)item.Content;
            

            switch (content.Text.Trim())
            {
                case "D&D 3.5":
                    
                    this.iconoCampaigne.Source = new BitmapImage(new Uri("/images/icons/D&D.png", UriKind.Relative));
                    break;

                case "Warhammer":
                    this.iconoCampaigne.Source = new BitmapImage(new Uri("/images/icons/warhammer.jpg", UriKind.Relative));
                    break;

                default:
                    MessageBox.Show(content.Text);
                    break;
            }
        }
    }
}
