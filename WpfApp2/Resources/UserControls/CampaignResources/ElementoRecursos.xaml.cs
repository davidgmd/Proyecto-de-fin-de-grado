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
    /// Lógica de interacción para ElementoRecursos.xaml
    /// </summary>
    public partial class ElementoRecursos : UserControl
    {
        public ElementoRecursos()
        {
            InitializeComponent();         
            this.DataContext = this;
        }

        private string _nombrelemento;

        public string NombreElemento
        {
            get { return _nombrelemento; }
            set { _nombrelemento = value; }
        }

        private string _direccionimagen;

        public string DireccionImagen
        {
            get { return _direccionimagen; }
            set { _direccionimagen = value; }
        }

        private Boolean _detallesarchivo;

        public Boolean DetallesArchivos
        {
            get { return _detallesarchivo; }
            set { _detallesarchivo = value; }
        }
    }
}
