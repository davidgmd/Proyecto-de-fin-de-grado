using System;
using System.Collections.Generic;
using System.Text;

namespace ElEscribaDelDJ.Classes
{
    public class Resumenes
    {

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string[] _etiquetas;

        public string[] Etiquetas
        {
            get { return _etiquetas; }
            set { _etiquetas = value; }
        }

        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        private int _pagina;

        public int Pagina
        {
            get { return _pagina; }
            set { _pagina = value; }
        }

        private string _manual;

        public string Manual
        {
            get { return _manual; }
            set { _manual = value; }
        }
    }
}
