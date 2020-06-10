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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace ElEscribaDelDJ.View
{
    /// <summary>
    /// Lógica de interacción para AnadirEscenario.xaml
    /// </summary>
    public partial class AnadirEscenario : Window
    {
        private Campana campana1;
        private EscenarioCampana escenario1;

        public EscenarioCampana Escenario
        {
            get { return escenario1; }
            set { escenario1 = value; }
        }


        public Campana Campana
        {
            get { return campana1; }
            set { campana1 = value; }
        }


        public AnadirEscenario(Campana campana, EscenarioCampana escenario)
        {
            InitializeComponent();

            this.Escenario = escenario;
            DataContext = this;
        }
    }
}
