using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace WpfApp2
{
    /// <summary>
    /// Lógica de interacción para Creditos.xaml
    /// </summary>
    public partial class Creditos : Window
    {
        public Creditos()
        {
            InitializeComponent();
            ReadFile();
        }

        public string FileText { get; set; }
        public void ReadFile()
        {
            var localDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            string path = localDirectory + "creditos.txt";
            FileText = File.ReadAllText(path);
            this.creditos.Text = FileText;
        }
    }
}
