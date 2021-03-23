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
    /// Lógica de interacción para ElementoRecursos.xaml
    /// </summary>
    public partial class ElementoRecursos : UserControl
    {
        public ElementoRecursos()
        {
            InitializeComponent();         
            this.DataContext = this;
            //MessageBox.Show(Altura.ToString());
        }

        public static readonly DependencyProperty NombreElementoProperty = DependencyProperty.Register("NombreElemento", typeof(string), typeof(ElementoRecursos), new
            PropertyMetadata("", new PropertyChangedCallback(OnSetStringChanged)));

        private static void OnSetStringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ElementoRecursos uc = d as ElementoRecursos;
            uc.OnSetStringChanged(e);
        }

        private void OnSetStringChanged(DependencyPropertyChangedEventArgs e)
        {
            NombreElemento = (string)e.NewValue;
        }

        public string NombreElemento
        {
            get { return (string)GetValue(NombreElementoProperty); }
            set { SetValue(NombreElementoProperty, value); }
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

        public static readonly DependencyProperty AlturaProperty = DependencyProperty.Register("Altura", typeof(double), typeof(ElementoRecursos), new
            PropertyMetadata(0.0d, new PropertyChangedCallback(OnSetDoubleChanged)));

        private static void OnSetDoubleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ElementoRecursos uc = d as ElementoRecursos;
            uc.OnSetDoubleChanged(e);
        }

        private void OnSetDoubleChanged(DependencyPropertyChangedEventArgs e)
        {
            Altura = (double)e.NewValue;
            Altura2 = (double)e.NewValue * 0.2;
        }

        public double Altura
        {
            get { return (double)GetValue(AlturaProperty); }
            set { SetValue(AlturaProperty,value); }
        }

        private double _altura2;

        public double Altura2
        {
            get { return _altura2; }
            set { _altura2 = value; }
        }


    }
}
