using ElEscribaDelDJ.Classes.Utilidades;
using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ElEscribaDelDJ.View.Calendar
{
    /// <summary>
    /// Lógica de interacción para Calendario.xaml
    /// </summary>
    public partial class Calendario : Window
    {
        private ObservableCollection<Event> eventos = new ObservableCollection<Event>();

        public ObservableCollection<Event> Eventos
        {
            get { return eventos; }
            set { eventos = value; }
        }


        public Calendario()
        {
            InitializeComponent();
            GoogleCalendar calendariogoogle = new GoogleCalendar();
            var events = calendariogoogle.GetEvents();
            foreach (Event evento in events.Items)
            {
                eventos.Add(evento);
            }

            DataContext = this;
        }
    }
}
