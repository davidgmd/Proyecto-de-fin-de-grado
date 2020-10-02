using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ElEscribaDelDJ.Classes.Utilidades
{
    class SmtpServer
    {
        private String _direccion;
        private String _usuario;
        private String _clave;
        private int _puerto;

        public int Puerto
        {
            get { return _puerto; }
            set { _puerto = value; }
        }


        public String Clave
        {
            get { return _clave; }
            set { _clave = value; }
        }


        public String Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }


        public String Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

    }
}
