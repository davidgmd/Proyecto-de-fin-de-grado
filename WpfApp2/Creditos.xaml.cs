using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
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
            string path = RecursosAplicacion.DireccionBase + "\\" + archivocreditos;
            FileText = File.ReadAllText(path);
            this.creditos.Text = FileText;
        }
    }
}
