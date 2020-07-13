using ElEscribaDelDJ.Classes.Utilidades;
using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
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
        private List<Event> eventosoriginales = new List<Event>();

        public List<Event> EventosOriginales
        {
            get { return eventosoriginales; }
            set { eventosoriginales = value; }
        }



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
            eventos.CollectionChanged += CambiarSeleccion;

            //ObtenerEventos();


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
            }

            //DataContext = null;
            DataContext = this;
        }

        private void FiltrarEventos(DateTime fecha)
        {
            eventos.Clear();

            foreach (Event evento in EventosOriginales)
            {
                if (evento.Start.DateTime.HasValue)
                {
                    int comparacion = DateTime.Compare(Calendar.DisplayDate, evento.Start.DateTime.Value);
                    if (comparacion <= 0)
                        eventos.Add(evento);
                }
            }

            //DataContext = null;
            //DataContext = this;
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
            EventosOriginales = eventos.ToList();
            FiltrarEventos(Calendar.DisplayDate);
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

        private void Calendar_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            if (calendariogoogle != null)
            {
                FiltrarEventos(Calendar.DisplayDate);
            }            
        }

        private void Calendar_Loaded(object sender, RoutedEventArgs e)
        {
            CalendarDateRange cdr = new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1));
            Calendar.BlackoutDates.Add(cdr);

            if (calendariogoogle != null)
            {
                ObtenerEventos();
                EventosOriginales = eventos.ToList();
            }
        }

        private void CambiarSeleccion(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (DatosEvento.ItemsSource != null)
            {
                if (DatosEvento.Items.Count > 0)
                {
                    DatosEvento.IsEditable = false;
                    //DatosEvento.IsReadOnly = true;
                    //DatosEvento.Focusable = false;
                    DatosEvento.SelectedIndex = 0;
                }
                else
                {
                    DatosEvento.IsEditable = true;
                    DatosEvento.IsReadOnly = true;
                    DatosEvento.Focusable = false;
                    DatosEvento.Text = "No hay eventos para esa fecha o posterior";
                }
            }     
        }

        private void DatosEvento_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            if (DatosEvento.Items.Count > 0)
            {
                DatosEvento.IsEditable = false;
                //DatosEvento.IsReadOnly = true;
                //DatosEvento.Focusable = false;
                DatosEvento.SelectedIndex = 0;
            }
            else
            {
                DatosEvento.IsEditable = true;
                DatosEvento.IsReadOnly = true;
                DatosEvento.Focusable = false;
                DatosEvento.Text = "No hay eventos para esa fecha o posterior";
            }
        }
    }
}
