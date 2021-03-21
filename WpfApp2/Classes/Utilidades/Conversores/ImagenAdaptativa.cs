using ElEscribaDelDJ.Resources.UserControls.CampaignResources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace ElEscribaDelDJ.Classes.Utilidades.Conversores
{
    class ImagenAdaptativa : IValueConverter
    {

        //Este conversor obtiene como primer valor en value el porcentaje, como segundo valor en parameter el ancho del elemento padre
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ElementoRecursos valor = value as ElementoRecursos;
            double porcentaje = valor.ActualHeight;
            return porcentaje * 0.2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
