using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Windows;

namespace ElEscribaDelDJ.Classes
{
    public class Resumenes: INotifyPropertyChanged, IEquatable<Resumenes>
    {

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { 
                _nombre = value;
                OnPropertyChanged("Nombre");
            }
        }

        private string _etiquetas;

        public string Etiquetas
        {
            get { return _etiquetas; }
            set { 
                _etiquetas = value;
                OnPropertyChanged("Etiquetas");
            }
        }

        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { 
                _descripcion = value;
                OnPropertyChanged("Descripcion");
            }
        }

        private int _pagina;

        public int Pagina
        {
            get { return _pagina; }
            set {
                if (value <= 0) 
                    _pagina = 1;
                else
                    _pagina = value;
                OnPropertyChanged("Pagina");
            }
        }

        private string _manual;

        public string Manual
        {
            get { return _manual; }
            set { 
                _manual = value;
                OnPropertyChanged("Manual");
            }
        }

        private string _manualurl;

        public string ManualUrl
        {
            get { return _manualurl; }
            set
            {
                _manualurl = value;
                OnPropertyChanged("ManualUrl");
            }
        }

        private string _tipoaventura;

        public string TipoAventura
        {
            get { return _tipoaventura; }
            set { 
                _tipoaventura = value;
                OnPropertyChanged("TipoAventura");
            }
        }

        private string _nombretipoaventura;

        public string NombreTipoAventura
        {
            get { return _nombretipoaventura; }
            set {
                _nombretipoaventura = value;
                OnPropertyChanged("NombreTipoAventura");
            }
        }


        private int _indice;

        public int Indice
        {
            get { return _indice; }
            set
            {
                _indice = value;
                OnPropertyChanged("Indice");
            }
        }

        private List<int> _indiceslibres;

        public List<int> IndicesLibres
        {
            get { return _indiceslibres; }
            set
            {
                _indiceslibres = value;
                OnPropertyChanged("IndicesLibres");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        public override bool Equals(object obj)
        {
            return obj is Resumenes other && Equals(other);
        }

        public bool Equals(Resumenes other)
        {
            return other.Nombre == this.Nombre && other.Descripcion == this.Descripcion;
        }

        public override int GetHashCode()
        {
            return Nombre.GetHashCode();
        }
    }
}
