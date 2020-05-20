using System;
using System.IO;
using System.Windows;
using Path = System.IO.Path;

namespace ElEscribaDelDJ
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
