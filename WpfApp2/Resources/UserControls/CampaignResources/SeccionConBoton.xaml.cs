using ElEscribaDelDJ.Classes;
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

namespace ElEscribaDelDJ.Resources.UserControls.CampaignResources
{
    /// <summary>
    /// Lógica de interacción para SeccionConBoton.xaml
    /// </summary>
    public partial class SeccionConBoton : UserControl
    {
        public SeccionConBoton()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private string _seccion;

        public string Seccion
        {
            get { return _seccion; }
            set { _seccion = value; }
        }

        private List<Recursos> myVar;

        public List<Recursos> MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

    }
}
