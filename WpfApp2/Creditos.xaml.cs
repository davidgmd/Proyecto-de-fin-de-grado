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
        public Creditos(string archivocreditos)
        {
            InitializeComponent();
            ReadFile(archivocreditos);
        }

        public string FileText { get; set; }
        public void ReadFile(string archivocreditos)
        {
            var localDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            string path = localDirectory + archivocreditos;
            FileText = File.ReadAllText(path);
            this.creditos.Text = FileText;
        }
    }
}
