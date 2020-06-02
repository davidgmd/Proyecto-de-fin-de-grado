using ElEscribaDelDJ.Classes;
using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace ElEscribaDelDJ.View
{
    /// <summary>
    /// Lógica de interacción para menuPrincipal.xaml
    /// </summary>
    public partial class menuPrincipal : Window
    {
        private List<Campana> listacampana = new List<Campana>();
        private ObservableCollection<Campana> nombres = new ObservableCollection<Campana>();

        public ObservableCollection<Campana> Nombres
        {
            get { return nombres; }
            set { nombres = value; }
        }

        public List<Campana> ListaCampanas
        {
            get { return listacampana; }
            set { listacampana = value; }
        }


        public menuPrincipal()
        {
            InitializeComponent();
            ConfiguracionPagina.DefinirIdioma(this, "MainMenu");

            foreach (Campana item in this.listacampana)
            {
                this.nombres.Add(item);
            }

            DataContext = this;
            if (this.campaignComboBox.HasItems)
            this.campaignComboBox.SelectedIndex = 0;
            //campaignComboBox.ItemsSource = this.nombres;
            
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

            var item = this.campaignComboBox.SelectedIndex;

            switch (item)
            {
                case 0:
                    
                    this.iconoCampaigne.Source = new BitmapImage(new Uri("/Images/icons/D&D.png", UriKind.Relative));
                    break;

                case 1:
                    this.iconoCampaigne.Source = new BitmapImage(new Uri("/Images/icons/warhammer-removebg.png", UriKind.Relative));
                    break;

                default:
                    MessageBox.Show("Otro");
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
