using ElEscribaDelDJ.Classes.Utilidades;
using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using Google.Apis.Calendar.v3.Data;
using Octokit;
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
using System.Xml;

namespace ElEscribaDelDJ.View.Calendar
{
    /// <summary>
    /// Lógica de interacción para Calendario.xaml
    /// </summary>
    public partial class Calendario : Window
    {
        private ObservableCollection<Event> _eventos = new ObservableCollection<Event>();
        private GoogleCalendar _calendariogoogle;
        private List<DateTime> _significantDates = new List<DateTime>();
        private List<Event> _eventosoriginales = new List<Event>();
        private Dictionary<String, Boolean> _camposcorrectos = new Dictionary<String, Boolean>();
        private Event _evento = new Event();

        public Event Evento
        {
            get { return _evento; }
            set { _evento = value; }
        }


        public Dictionary<String, Boolean> CamposCorrectos
        {
            get { return _camposcorrectos; }
            set { _camposcorrectos = value; }
        }


        public List<Event> EventosOriginales
        {
            get { return _eventosoriginales; }
            set { _eventosoriginales = value; }
        }

        public ObservableCollection<Event> Eventos
        {
            get { return _eventos; }
            set { _eventos = value; }
        }

        public Calendario()
        {
            //Define la variable cultural según el archivo de configuración
            string codigocultura = "en-US";
            
            if (ConfiguracionAplicacion.Default.Idioma.Equals("ES"))
            {
                codigocultura = "es-ES";
            }

            //modifica la variable de infórmación cultural del programa en curso
            CultureInfo ci = new CultureInfo(codigocultura);
            CultureInfo.DefaultThreadCurrentCulture = ci;

            //inicializamos el programa
            InitializeComponent();

            ConfiguracionPagina.DefinirIdioma(this, "Calendar");

            //declaramos vacio una lista de fechas para marcar en el calendario
            _significantDates = new List<DateTime>();
            //vinculamos cualquier cambio en el observable eventos al metodo cambiarselección
            _eventos.CollectionChanged += CambiarSeleccion;

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
            var events = _calendariogoogle.GetEvents();      
            _eventos.Clear();

            foreach (Event evento in events.Items)
            {
                _eventos.Add(evento);
                if (evento.Start.DateTime.HasValue) 
                    _significantDates.Add(evento.Start.DateTime.Value.Date);
            }

            //DataContext = null;
            DataContext = this;
        }

        //filtra los eventos según la fecha actual
        private void FiltrarEventos(DateTime fecha)
        {
            _eventos.Clear();

            foreach (Event evento in EventosOriginales)
            {
                if (evento.Start.DateTime.HasValue)
                {
                    int comparacion = DateTime.Compare(Calendar.DisplayDate, evento.Start.DateTime.Value);
                    if (comparacion <= 0)
                        _eventos.Add(evento);
                }
            }

            //DataContext = null;
            //DataContext = this;
        }

        //Cuando marcamos en google, trata de conectar con nuestra cuenta si no lo consigue nos manda a internet a indicar los datos
        private void botonGoogleCalendar_Click(object sender, RoutedEventArgs e)
        {
            if (_calendariogoogle is null)
            {
                _calendariogoogle = new GoogleCalendar();
                ObtenerEventos();
                EventosOriginales = _eventos.ToList();
                Calendar.IsEnabled = true;
                ListaEventosGoogleCalendar.IsEnabled = true;
                GridFormulario.IsEnabled = true;
            }
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
            EventosOriginales = _eventos.ToList();
            FiltrarEventos(Calendar.DisplayDate);
            ICollectionView view = CollectionViewSource.GetDefaultView(DatosEvento.ItemsSource);
            view.Refresh();
            MessageBox.Show("El elemento calendario de la izquierda no muestra los cambios automaticamente hay que cambiar de fecha y volver a esta");
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
            if (_significantDates.Contains(date))
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
            if (_calendariogoogle != null)
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

        //Cuando cambiamos el evento comprueba si es igual a esta fecha o posterior en caso afirmativo
        //Desbloquea el botón editar
        private void DatosEvento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Event evento = (Event)DatosEvento.SelectedItem;
            if ((evento != null) && (evento.End.DateTime.HasValue))
                if (DateTime.Compare(DateTime.Now, evento.End.DateTime.Value) > 0)
                {
                    BotonEditarEvento.IsEnabled = false;
                }
                else
                {
                    BotonEditarEvento.IsEnabled = true;
                }
        }

        //Permite editar los eventos si estos son iguales a la fecha actual o posterior
        //Este botón se encuentra en el listview
        private void BotonEditarEvento_Click(object sender, RoutedEventArgs e)
        {
            var item = DatosEvento.SelectedItem;          
            Event eventoseleccionado = (Event)item;

            Evento.Id = eventoseleccionado.Id;
            Evento.Summary = eventoseleccionado.Summary;
            Evento.Location = eventoseleccionado.Location;
            Evento.Description = eventoseleccionado.Description;
            switch (eventoseleccionado.Status)
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

            FechaInicioDatePicker.SelectedDate = eventoseleccionado.Start.DateTime.Value.Date;
            Evento.Start = eventoseleccionado.Start;
            FechaFinDatePicker.SelectedDate = eventoseleccionado.End.DateTime.Value.Date;
            Evento.End = eventoseleccionado.End;
            ComboBoxHoras.Text = eventoseleccionado.Start.DateTime.Value.Hour.ToString();
            ComboBoxMinutos.Text = eventoseleccionado.Start.DateTime.Value.Minute.ToString();

            BotonAgregarEvento.IsEnabled = false;
            BotonFormularioEditar.IsEnabled = true;

            RefrescarGrid();
            
        }

        //Permite eliminar eventos, este botón se encuentra en el listview
        private void BotonEliminarEvento_Click(object sender, RoutedEventArgs e)
        {
            if (_calendariogoogle.DeleteEvent((Event)DatosEvento.SelectedItem))
            {
                MessageBox.Show("Eliminado con exito");
            }
        }

        //Comprueba que el campo de texto no esta vacio y en caso de no estarlo lo añade como correcto a la lista de campos
        private void TextoFormulario_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            if (textbox.Text != String.Empty)
            {
                if (!CamposCorrectos.ContainsKey(textbox.Name))
                {
                    CamposCorrectos.Add(textbox.Name, true);
                    ValidarCampos();
                }       
            }
            else
            {
                if (CamposCorrectos.ContainsKey(textbox.Name))
                {
                    CamposCorrectos.Remove(textbox.Name);
                    if (BotonAgregarEvento.IsEnabled == true)
                        BotonAgregarEvento.IsEnabled = false;
                }           
            }
        }

        //Se encarga de convertir el estado a los valores admitidos por google
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combobox = (ComboBox)sender;
            switch (ComboBoxEstado.SelectedIndex)
            {
                case 0:
                    Evento.Status = "tentative";
                    break;
                case 1:
                    Evento.Status = "confirmed";
                    break;
                case 2:
                    Evento.Status = "cancelled";
                    break;
            }

            if (!CamposCorrectos.ContainsKey(combobox.Name))
            {
                CamposCorrectos.Add(combobox.Name, true);
                ValidarCampos();
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
                if (CamposCorrectos.ContainsKey(FechaFinDatePicker.Name))
                {
                    CamposCorrectos.Remove(FechaFinDatePicker.Name);
                }
            }
            finally
            {
                if (!CamposCorrectos.ContainsKey(FechaInicioDatePicker.Name))
                {
                    CamposCorrectos.Add(FechaInicioDatePicker.Name, true);
                    DateTime fecha = new DateTime();
                    fecha = FechaInicioDatePicker.SelectedDate.Value;

                    Evento.Start = new EventDateTime();
                    Evento.Start.DateTime = new DateTime(fecha.Year, fecha.Month, fecha.Day, 00, 0, 0);
                }
                ValidarCampos();
            }

        }

        //Se encarga de asignar el valor de la fecha y además de que no pueda ser la fecha fin incorrecta
        private void FechaFinDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FechaFinDatePicker.SelectedDate.HasValue)
            {
                DateTime fecha = new DateTime();
                fecha = FechaFinDatePicker.SelectedDate.Value;

                Evento.End = new EventDateTime();
                Evento.End.DateTime = new DateTime(fecha.Year, fecha.Month, fecha.Day, 00, 0, 0);

                if (!CamposCorrectos.ContainsKey(FechaFinDatePicker.Name))
                {
                    CamposCorrectos.Add(FechaFinDatePicker.Name, true);
                    ValidarCampos();
                }
            }
            else
            {
                if (CamposCorrectos.ContainsKey(FechaFinDatePicker.Name))
                {
                    CamposCorrectos.Remove(FechaFinDatePicker.Name);
                    ValidarCampos();
                }
            }           
        }

        //Añade el campo hora tal como seleccionamos uno de los combobos de hora
        private void ComboBoxHoras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox hora = (ComboBox)sender;

            if (!CamposCorrectos.ContainsKey(hora.Name))
            {
                CamposCorrectos.Add(hora.Name, true);
                ValidarCampos();
            }
        }

        //Valida que los campos del formulario estan todos agregados como correctos y desbloquea el botón añadir
        private void ValidarCampos()
        {
            if (CamposCorrectos.Count == 8 && BotonFormularioEditar.IsEnabled == false)
            {
                BotonAgregarEvento.IsEnabled = true;
            }
            else
            {
                BotonAgregarEvento.IsEnabled = false;
            }
        }

        //Agrega un evento al calendario del usuario
        private void BotonAgregarEvento_Click(object sender, RoutedEventArgs e)
        {
            Event evento = Evento;


            TimeSpan hora = new TimeSpan(int.Parse(ComboBoxHoras.SelectedItem.ToString()), int.Parse(ComboBoxMinutos.SelectedIndex.ToString()), 00);
            evento.Start.DateTime = evento.Start.DateTime + hora;


            _calendariogoogle.CreateEvent(evento);
            LimpiarCampos();
        }

        private void BotonFormularioEditar_Click(object sender, RoutedEventArgs e)
        {
            Event evento = Evento;
            DateTime fecha = FechaInicioDatePicker.SelectedDate.Value;
            TimeSpan hora = new TimeSpan(int.Parse(ComboBoxHoras.SelectedItem.ToString()), int.Parse(ComboBoxMinutos.SelectedIndex.ToString()), 00);
            fecha = fecha + hora;

            evento.Start.DateTime = fecha;
            evento.End.DateTime = FechaFinDatePicker.SelectedDate.Value;

            _calendariogoogle.EditEvent(evento);

            BotonFormularioEditar.IsEnabled = false;
            BotonAgregarEvento.IsEnabled = true;
            LimpiarCampos();

        }

        private void BotonLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            Evento = null;
            ComboBoxEstado.Text = "";
            FechaInicioDatePicker.SelectedDate = DateTime.Now;
            FechaFinDatePicker.Text = "";
            ComboBoxHoras.Text = "00";
            ComboBoxMinutos.Text = "00";

            RefrescarGrid();
        }

        private void RefrescarGrid()
        {
            GridFormulario.DataContext = null;
            GridFormulario.DataContext = Evento;
        }

        
    }
}
