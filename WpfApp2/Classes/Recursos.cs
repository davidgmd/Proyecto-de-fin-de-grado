using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace ElEscribaDelDJ.Classes
{
    public class Recursos: INotifyPropertyChanged
    {
        private ObservableCollection<Archivos> _documentos = new ObservableCollection<Archivos>();

        public ObservableCollection<Archivos> Documentos
        {
            get { return _documentos; }
            set { 
                _documentos = value;
                OnPropertyChanged("Documentos");
            }
        }

        private HashSet<Resumenes> _resumenes = new HashSet<Resumenes>();

        public HashSet<Resumenes> Resumenes
        {
            get { return _resumenes; }
            set { _resumenes = value; }
        }


        private ObservableCollection<Archivos> _lore = new ObservableCollection<Archivos>();

        public ObservableCollection<Archivos> Lore
        {
            get { return _lore; }
            set { _lore = value; }
        }

        private ObservableCollection<Archivos> _media = new ObservableCollection<Archivos>();

        public ObservableCollection<Archivos> Media
        {
            get { return _media; }
            set { _media = value; }
        }

        private ObservableCollection<Archivos> _fichas = new ObservableCollection<Archivos>();

        public ObservableCollection<Archivos> Fichas
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
