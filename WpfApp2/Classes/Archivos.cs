using ElEscribaDelDJ.Classes.Utilidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElEscribaDelDJ.Classes
{
    public class Archivos
    {
        private string _seccion;

        public string Seccion
        {
            get { return _seccion; }
            set { _seccion = value; }
        }


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
            set { _extension = value; }
        }

        private string _direccion;

        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }       

        private string _url;

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
    }
}
