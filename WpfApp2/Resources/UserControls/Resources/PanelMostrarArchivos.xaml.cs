using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ElEscribaDelDJ.Resources.UserControls.Resources
{
    /// <summary>
    /// Lógica de interacción para PanelMostrarArchivos.xaml
    /// </summary>
    public partial class PanelMostrarArchivos : UserControl
    {

        
        public PanelMostrarArchivos()
        {
            ConfiguracionPagina.DefinirIdioma(this, "Resources");
            refrescartitulos();
            this.DataContext = this;
            InitializeComponent();
            
        }

        public static readonly DependencyProperty CampaignTitleProperty = DependencyProperty.Register("CampaignTitle", typeof(string), typeof(PanelMostrarArchivos), new
            PropertyMetadata("", new PropertyChangedCallback(OnCampaignTitleChanged)));

        private static void OnCampaignTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PanelMostrarArchivos uc = d as PanelMostrarArchivos;
            uc.OnSetCampaignTitleChanged(e);
        }

        private void OnSetCampaignTitleChanged(DependencyPropertyChangedEventArgs e)
        {
            CampaignTitle = (string)e.NewValue;
        }

        public string CampaignTitle
        {
            get { return (string)GetValue(CampaignTitleProperty); }
            set { SetValue(CampaignTitleProperty, value); }
        }

        public static readonly DependencyProperty ScenaryTitleProperty = DependencyProperty.Register("ScenaryTitle", typeof(string), typeof(PanelMostrarArchivos), new
            PropertyMetadata("", new PropertyChangedCallback(OnScenaryTitleChanged)));

        private static void OnScenaryTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PanelMostrarArchivos uc = d as PanelMostrarArchivos;
            uc.OnSetScenaryTitleChanged(e);
        }

        private void OnSetScenaryTitleChanged(DependencyPropertyChangedEventArgs e)
        {
            ScenaryTitle = (string)e.NewValue;
        }

        public string ScenaryTitle
        {
            get { return (string)GetValue(ScenaryTitleProperty); }
            set { SetValue(ScenaryTitleProperty, value); }
        }

        public static readonly DependencyProperty AdventureTitleProperty = DependencyProperty.Register("AdventureTitle", typeof(string), typeof(PanelMostrarArchivos), new
            PropertyMetadata("", new PropertyChangedCallback(OnAdventureTitleChanged)));

        private static void OnAdventureTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PanelMostrarArchivos uc = d as PanelMostrarArchivos;
            uc.OnSetAdventureTitleChanged(e);
        }

        private void OnSetAdventureTitleChanged(DependencyPropertyChangedEventArgs e)
        {
            AdventureTitle = (string)e.NewValue;
        }

        public string AdventureTitle
        {
            get { return (string)GetValue(AdventureTitleProperty); }
            set { SetValue(AdventureTitleProperty, value); }
        }

        private void refrescartitulos()
        {
            CampaignTitle = this.FindResource("CampaignSection").ToString();
            ScenaryTitle = this.FindResource("ScenarySection").ToString();
            AdventureTitle = this.FindResource("AdventureSection").ToString();
        }
    }
}
