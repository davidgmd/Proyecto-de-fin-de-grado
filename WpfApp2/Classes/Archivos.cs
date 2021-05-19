using ElEscribaDelDJ.Classes.Utilidades;
using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using ElEscribaDelDJ.View.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ElEscribaDelDJ.Classes
{
    public class Archivos : INotifyPropertyChanged
    {
        private string _nombre;

        public string NombreArchivo
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _extension;

        public string Extension
        {
            get { return _extension; }
            set 
            { 
                _extension = value;
                OnPropertyChanged("Extension");
            }
        }

        private string _direccion;

        public string Direccion
        {
            get { return _direccion; }
            set 
            { 
                _direccion = value;
                OnPropertyChanged("Direccion");
             }
        }       

        private string _url;

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        public void AgregarArchivo(string tipoaventura, Archivos archivo, string seccion)
        {
            switch (tipoaventura.ToLower())
            {
                case "campana":
                    DatosAplicacion.CampanaSeleccionada.AnadirArchivo(seccion, archivo);
                    break;
                case "escenario":
                    DatosAplicacion.EscenarioSeleccionado.AnadirArchivo(seccion, archivo);
                    break;
                case "aventura":
                    DatosAplicacion.AventuraSeleccionada.AnadirArchivo(seccion, archivo);
                    break;
                default:
                    break;
            }
        }

        public void EditarArchivo(string tipoaventura, Archivos archivo, int indice, string seccion)
        {
            switch (tipoaventura.ToLower())
            {
                case "campana":
                    DatosAplicacion.CampanaSeleccionada.EditarArchivo(seccion, archivo, indice);
                    break;
                case "escenario":
                    DatosAplicacion.EscenarioSeleccionado.EditarArchivo(seccion, archivo, indice);
                    break;
                case "aventura":
                    DatosAplicacion.AventuraSeleccionada.EditarArchivo(seccion, archivo, indice);
                    break;
                default:
                    break;
            }
        }

        public void EliminarArchivo(string tipoaventura, Archivos archivo, string seccion, int indice)
        {
            switch (tipoaventura.ToLower())
            {
                case "campana":
                    DatosAplicacion.CampanaSeleccionada.EliminarArchivo(seccion, indice);
                    break;
                case "escenario":
                    DatosAplicacion.EscenarioSeleccionado.EliminarArchivo(seccion, indice);
                    break;
                case "aventura":
                    DatosAplicacion.AventuraSeleccionada.EliminarArchivo(seccion, indice);
                    break;
                default:
                    break;
            }
        }
    }
}
