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
using System.Windows.Shapes;

namespace ElEscribaDelDJ.View.Resources
{
    /// <summary>
    /// Lógica de interacción para AnadirArchivo.xaml
    /// </summary>
    public partial class AnadirArchivo : Window
    {
        public AnadirArchivo(List<Archivos> lista)
        {
            InitializeComponent();
            listaarchivos = lista;
        }

        public AnadirArchivo(Archivos archivo1)
        {
            InitializeComponent();
            archivo = archivo1;
        }


        public List<Archivos> listaarchivos { get; set; }
        public Archivos archivo { get; set; }
    }
}
