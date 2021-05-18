using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace ElEscribaDelDJ.Classes.Utilidades.Conversores
{
    class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (ConfiguracionAplicacion.Default.Idioma.Equals("ES"))
            {
                switch (value.ToString().ToLower())
                {
                    case "tentative":
                        return "Por confirmar";
                    case "confirmed":
                        return "Confirmado";
                    case "cancelled":
                        return "Cancelado";
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (ConfiguracionAplicacion.Default.Idioma.Equals("ES"))
            {
                switch (value.ToString().ToLower())
                {
                    case "Por confirmar":
                        return "tentative";
                    case "Confirmado":
                        return "confirmed";
                    case "Cancelado":
                        return "cancelled";
                }
            }

            return value;
        }
    }
}
