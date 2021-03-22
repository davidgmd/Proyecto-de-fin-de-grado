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
    /// Lógica de interacción para Resumen_reglas.xaml
    /// </summary>
    public partial class Resumen_reglas : UserControl
    {
        public Resumen_reglas()
        {
            InitializeComponent();
            List<Resumenes> resumenes = new List<Resumenes>();
            resumenes.Add(new Resumenes() { Nombre = "Regla transformación en garou", Etiquetas = "Hombre lobo, garou, mundo de tinieblas", 
                Descripcion = "Esta regla permite al hombre lobo pasar de su forma huminida a su forma garou obteniendo los diversos bonificadores", 
                Pagina = 120, Manual="Hombre Lobo 20 aniversario" });
            //recursos.Add(new Recursos() { NombreRecurso = "Jane Doe", Age = 39, Mail = "jane@doe-family.com" });
            //recursos.Add(new Recursos() { NombreRecurso = "Sammy Doe", Age = 13, Mail = "sammy.doe@gmail.com" });
            ListaResumenes.ItemsSource = resumenes;
        }
    }
}
