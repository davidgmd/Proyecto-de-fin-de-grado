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
            //Define la variable cultural según el archivo de configuración
            string codigocultura = "en-EN";
            
            if (ConfiguracionAplicacion.Default.Idioma.Equals("ES"))
            {
                codigocultura = "es-ES";
            }

            //modifica la variable de infórmación cultural del programa en curso
            CultureInfo ci = new CultureInfo(codigocultura);
            CultureInfo.DefaultThreadCurrentCulture = ci;

            //inicializamos el programa
            InitializeComponent();

            //declaramos vacio una lista de fechas para marcar en el calendario
            significantDates = new List<DateTime>();
            //vinculamos cualquier cambio en el observable eventos al metodo cambiarselección
            eventos.CollectionChanged += CambiarSeleccion;

            //ObtenerEventos();
            InicializarSelectorHoras();

        }

        private void InicializarSelectorHoras()
        {
            for (int i=00; i < 24; i++)
            {
                ComboBoxHoras.Items.Add(i.ToString("00"));
            }

            for (int i = 00; i < 60; i++)
            {
                ComboBoxMinutos.Items.Add(i.ToString("00"));
            }
        }

        //abre el navegador al pulsar en un texto que este como hipervinculo
        private void TextBlockHiperLink_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var elemento = (TextBlock)sender;
            string url = elemento.Text;
            url = url.Replace("&", "^&");
            var proceso = Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }

        //obtiene todos los eventos de googlecalendar del usuario
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

        //filtra los eventos según la fecha actual
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

        //Actualiza la lista ya sea pulsando en el botón o en la parte de texto
        private void ActualizarListView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ActualizarListView();
        }

        private void BotonActualizarListView_Click(object sender, RoutedEventArgs e)
        {
            ActualizarListView();
        }

        //evento que actualiza la lista de eventos
        private void ActualizarListView()
        {
            ObtenerEventos();
            EventosOriginales = eventos.ToList();
            FiltrarEventos(Calendar.DisplayDate);
            ICollectionView view = CollectionViewSource.GetDefaultView(DatosEvento.ItemsSource);
            view.Refresh();
        }

        //Al inicializarse los botones del calendario, este carga el aspecto de los dias como botones, marca los dias importantes y 
        //vincula el evento change de esos botones al del calendario
        private void calendarButton_Loaded(object sender, EventArgs e)
        {
            CalendarDayButton button = (CalendarDayButton)sender;
            DateTime date = (DateTime)button.DataContext;
            HighlightDay(button, date);
            button.DataContextChanged += new DependencyPropertyChangedEventHandler(calendarButton_DataContextChanged);
        }

        //Marca los dias importantes, añadiendo un fondo azulado a los dias importantes y transparente al resto
        private void HighlightDay(CalendarDayButton button, DateTime date)
        {
            button.IsEnabled = false;
            if (significantDates.Contains(date))
                button.Background = Brushes.LightBlue;
            else
                button.Background = Brushes.Transparent;
        }

        //Indica que pasa cuando cambian los datos
        private void calendarButton_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CalendarDayButton button = (CalendarDayButton)sender;
            DateTime date = (DateTime)button.DataContext;
            HighlightDay(button, date);
        }

        //Indica que ocurre cuando cambian los datos de las fechas 
        private void Calendar_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            if (calendariogoogle != null)
            {
                FiltrarEventos(Calendar.DisplayDate);
            }            
        }

        //Al cargar el calendario marca como no usables todos los dias anteriores a hoy
        private void Calendar_Loaded(object sender, RoutedEventArgs e)
        {
            CalendarDateRange cdr = new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1));
            Calendar.BlackoutDates.Add(cdr);
            FechaInicioDatePicker.BlackoutDates.Add(cdr);
        }

        //Al seleccionar otro dia comprueba si hay eventos y en caso de que no haya muestra el mensaje, si hay marca el primero
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

        //Si cambia la cuenta de google o de donde estemos extrayendo los datos
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

        //Cuando marcamos en google, trata de conectar con nuestra cuenta si no lo consigue nos manda a internet a indicar los datos
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

        //Al cambiar la fecha repasa los dias no disponibles, los marca en el otro calendario, si estos no cuadraran avisa del error.
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

        private void BotonEditarEvento_Click(object sender, RoutedEventArgs e)
        {
            var item = DatosEvento.SelectedItem;
            Event evento = (Event)item;
            TextoOrganizador.Text = evento.Organizer.DisplayName;
            TextoEmail.Text = evento.Organizer.Email;
            TextoAsunto.Text = evento.Description;
            switch (evento.Status)
            {
                case "tentative":
                    ComboBoxEstado.SelectedIndex = 0;
                    break;
                case "confirmed":
                    ComboBoxEstado.SelectedIndex = 1;
                    break;
                case "cancelled":
                    ComboBoxEstado.SelectedIndex = 2;
                    break;
                default:
                    ComboBoxEstado.SelectedIndex = 0;
                    break;
            }
        }
    }
}
