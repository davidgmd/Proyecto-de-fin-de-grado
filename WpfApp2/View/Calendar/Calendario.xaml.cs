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
        private GoogleCalendar calendariogoogle;
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
            button.IsEnabled = false;
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
            FechaInicioDatePicker.BlackoutDates.Add(cdr);
        }

        private void CambiarSeleccion(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (DatosEvento.ItemsSource != null)
            {
                if (DatosEvento.Items.Count > 0)
                {
                    DatosEvento.IsEditable = false;
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

        private void botonGoogleCalendar_Click(object sender, RoutedEventArgs e)
        {
            if (calendariogoogle is null)
            {
                calendariogoogle = new GoogleCalendar();
                ObtenerEventos();
                EventosOriginales = eventos.ToList();
                Calendar.IsEnabled = true;
                ListaEventosGoogleCalendar.IsEnabled = true;
            }            
        }

        private void FechaInicioDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FechaFinDatePicker.IsEnabled = true;
            CalendarDateRange cdr = new CalendarDateRange(DateTime.MinValue, FechaInicioDatePicker.SelectedDate.Value.AddDays(-1));
            FechaFinDatePicker.BlackoutDates.Clear();

            try
            {               
                FechaFinDatePicker.BlackoutDates.Add(cdr);
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("La fecha de inicio no puede ser posterior a la de fin");
                FechaFinDatePicker.SelectedDate = null;
                FechaFinDatePicker.BlackoutDates.Add(cdr);
            }
            
        }

        private void HoraInicio_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox hora = (TextBox)sender; 
            if(hora.Text.Equals("HH"))
            {
                hora.Text = "";
            }
        }

        private void HoraInicio_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox hora = (TextBox)sender;
            if (hora.Text.Equals(String.Empty))
            {
                hora.Text = "HH";
            }
            else
            {
                var hora1 = int.Parse(hora.Text);
                if (hora1<0 || hora1 > 23)
                {
                    MessageBox.Show("La hora debe estar entre 00 y 23");
                    hora.Text = "HH";
                }
                else
                {
                    ValidarHora(sender);               
                }
            }
            
        }

        private void HoraInicio_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) ||
            (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void MinutosInicio_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox minutos = (TextBox)sender;
            if (minutos.Text.Equals("MM"))
            {
                minutos.Text = "";
            }
        }

        private void MinutosInicio_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox minutos = (TextBox)sender;
            if (minutos.Text.Equals(String.Empty))
            {
                minutos.Text = "MM";
            }
            else
            {
                var minutos1 = int.Parse(minutos.Text);
                if (minutos1 < 0 || minutos1 > 60)
                {
                    MessageBox.Show("Los minutos deben estar entre 00 y 60");
                    minutos.Text = "MM";
                }
            }
        }

        private void MinutosInicio_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) ||
            (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void HoraFin_GotFocus(object sender, RoutedEventArgs e)
        {
            HoraInicio_GotFocus(sender, e);
        }

        private void HoraFin_LostFocus(object sender, RoutedEventArgs e)
        {
            HoraInicio_LostFocus(sender, e);
        }

        private void HoraFin_KeyDown(object sender, KeyEventArgs e)
        {
            HoraInicio_KeyDown(sender, e);
        }

        private void MinutosFin_GotFocus(object sender, RoutedEventArgs e)
        {
            MinutosInicio_GotFocus(sender, e);
        }

        private void MinutosFin_LostFocus(object sender, RoutedEventArgs e)
        {
            MinutosInicio_LostFocus(sender, e);
        }

        private void MinutosFin_KeyDown(object sender, KeyEventArgs e)
        {
            MinutosInicio_KeyDown(sender, e);
        }

        private Boolean ValidarHora(int hora1, int hora2, object sender)
        {          
            if (hora1 > hora2)
            {
                MessageBox.Show("La hora de fin no puede ser menor que la de inicio");
                TextBox hora = (TextBox)sender;
                hora.Text = "HH";
                return false;
            }
            else
            {
                return true;
            }
        }

        private Boolean FechasIguales()
        {
            if (DateTime.Compare(FechaInicioDatePicker.SelectedDate.Value, FechaFinDatePicker.SelectedDate.Value) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void ValidarHora(Object sender)
        {
            if (FechasIguales())
            {
                if (int.TryParse(HoraInicio.Text, out int horaini) && int.TryParse(HoraFin.Text, out int horafin))
                {
                    if (ValidarHora(horaini, horafin, sender))
                    {
                        if (int.TryParse(MinutosInicio.Text, out int minini) && int.TryParse(MinutosFin.Text, out int minfin))
                        {
                            if (horaini == horafin)
                            {
                                //ValidarMinutos();
                            }
                        }
                    }
                }
            }
        }
    }
}
