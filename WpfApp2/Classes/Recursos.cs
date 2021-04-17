using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ElEscribaDelDJ.Classes
{
    public class Recursos: INotifyPropertyChanged
    {
        private List<Archivos> _documentos = new List<Archivos>();

        public List<Archivos> Documentos
        {
            get { return _documentos; }
            set { 
                _documentos = value;
                OnPropertyChanged("Documentos");
            }
        }

        private List<Resumenes> _resumenes = new List<Resumenes>();

        public List<Resumenes> Resumenes
        {
            get { return _resumenes; }
            set { _resumenes = value; }
        }


        private List<Archivos> _lore = new List<Archivos>();

        public List<Archivos> Lore
        {
            get { return _lore; }
            set { _lore = value; }
        }

        private List<Archivos> _media = new List<Archivos>();

        public List<Archivos> Media
        {
            get { return _media; }
            set { _media = value; }
        }

        private List<Archivos> _fichas = new List<Archivos>();

        public List<Archivos> Fichas
        {
            get { return _fichas; }
            set { _fichas = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

    }
}
