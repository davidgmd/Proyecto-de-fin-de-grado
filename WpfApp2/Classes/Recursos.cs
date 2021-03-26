using System;
using System.Collections.Generic;
using System.Text;

namespace ElEscribaDelDJ.Classes
{
    public class Recursos
    {
        private List<Archivos> _documentos;

        public List<Archivos> Documentos
        {
            get { return _documentos; }
            set { _documentos = value; }
        }

        private List<Resumenes> _resumenes;

        public List<Resumenes> Resumense
        {
            get { return _resumenes; }
            set { _resumenes = value; }
        }


        private List<Archivos> _lore;

        public List<Archivos> Lore
        {
            get { return _lore; }
            set { _lore = value; }
        }

        private List<Archivos> _media;

        public List<Archivos> Media
        {
            get { return _media; }
            set { _media = value; }
        }

        private List<Archivos> _fichas;

        public List<Archivos> Fichas
        {
            get { return _fichas; }
            set { _fichas = value; }
        }


    }
}
