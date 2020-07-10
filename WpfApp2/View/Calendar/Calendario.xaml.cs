﻿using ElEscribaDelDJ.Classes.Utilidades;
using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        private GoogleCalendar calendariogoogle = new GoogleCalendar();
        private List<DateTime> significantDates = new List<DateTime>();


        public ObservableCollection<Event> Eventos
        {
            get { return eventos; }
            set { eventos = value; }
        }


        public Calendario()
        {
            string codigocultura = "en-EN";
            
            if (ConfiguracionAplicacion.Default.Idioma.Equals("ES"))
            {
                codigocultura = "es-ES";
            }

            CultureInfo ci = new CultureInfo(codigocultura);
            CultureInfo.DefaultThreadCurrentCulture = ci;
            InitializeComponent();
            significantDates = new List<DateTime>();

            ObtenerEventos();

            
        }

        private void TextBlockHiperLink_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var elemento = (TextBlock)sender;
            string url = elemento.Text;
            url = url.Replace("&", "^&");
            var proceso = Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }

        private void ObtenerEventos()
        {
            var events = calendariogoogle.GetEvents();      
            eventos.Clear();

            foreach (Event evento in events.Items)
            {
                eventos.Add(evento);
                if (evento.Start.DateTime.HasValue) 
                    significantDates.Add(evento.Start.DateTime.Value.Date);
                else if(evento.End.DateTime.HasValue)
                    significantDates.Add(evento.End.DateTime.Value.Date);

            }

            //DataContext = null;
            DataContext = this;
        }

        private void ActualizarListView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ActualizarListView();
        }

        private void BotonActualizarListView_Click(object sender, RoutedEventArgs e)
        {
            ActualizarListView();
        }

        private void ActualizarListView()
        {
            ObtenerEventos();
            ICollectionView view = CollectionViewSource.GetDefaultView(DatosEvento.ItemsSource);
            view.Refresh();
        }

        private void calendarButton_Loaded(object sender, EventArgs e)
        {
            CalendarDayButton button = (CalendarDayButton)sender;
            DateTime date = (DateTime)button.DataContext;
            HighlightDay(button, date);
            button.DataContextChanged += new DependencyPropertyChangedEventHandler(calendarButton_DataContextChanged);
        }

        private void HighlightDay(CalendarDayButton button, DateTime date)
        {
            if (significantDates.Contains(date))
                button.Background = Brushes.LightBlue;
            else
                button.Background = Brushes.Transparent;
        }

        private void calendarButton_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CalendarDayButton button = (CalendarDayButton)sender;
            DateTime date = (DateTime)button.DataContext;
            HighlightDay(button, date);
        }
    }
}