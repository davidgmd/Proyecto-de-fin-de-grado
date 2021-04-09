using ElEscribaDelDJ.Classes.Utilidades;
using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using ElEscribaDelDJ.View.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElEscribaDelDJ.Classes
{
    public class Archivos
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

        public void ModificarListaArchivos(List<Archivos> listaarchivos, string seccion, string tipoaventura, List<string> nombres, string accion)
        {
            AnadirArchivo anadir = new AnadirArchivo(listaarchivos);
            anadir.Show();
            GuardarLista(listaarchivos, seccion, tipoaventura, nombres);
        }

        public void GuardarLista(List<Archivos> listaarchivos, string seccion, string tipoaventura, List<string> nombres)
        {
            switch (tipoaventura)
            {
                case ("Campana"):
                    break;
                case ("Escenario"):
                    break;
                case ("Aventura"):
                    Aventura aventura1 = new Aventura();
                    aventura1.AnadirArchivo(nombres, seccion, listaarchivos);
                    break;
                default:
                    break;
            }
        }
    }
}
