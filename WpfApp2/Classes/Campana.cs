﻿using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ElEscribaDelDJ.Classes
{
    public class Campana
    {
		protected string _nombre;
		protected string _descripcion;
        private string _imagen;
		protected Recursos _recursos = new Recursos();
		private List<EscenarioCampana> _listaescenarios = new List<EscenarioCampana>();

        public List<EscenarioCampana> ListaEscenarios
        {
            get { return _listaescenarios; }
            set { _listaescenarios = value; }
        }


        public string DireccionImagen
		{
			get { return _imagen; }
			set { _imagen = value ?? "/Images/icons/icons8-escudopregunta.png"; }
		}

		public Recursos Recursos
		{
			get { return _recursos; }
			set { _recursos = value; }
		}

		public string Descripcion
		{
			get { return _descripcion; }
			set { _descripcion = value; }
		}

		public string Nombre
		{
			get { return _nombre; }
			set { _nombre = value; }
		}

		public Campana ExisteCampanaSesion(Campana campana1)
		{
			var resultado = RecursosAplicacion.SesionUsuario.ListCampaigns.Find(c => c.Nombre.Equals(campana1.Nombre) && c.Descripcion.Equals(campana1.Descripcion));
			return resultado;
		}

		public Campana ExisteCampanaObservable(Campana campana1, ObservableCollection<Campana> observable)
		{
			var resultado = observable.Where(c => c.Nombre.Equals(campana1.Nombre) && c.Descripcion.Equals(campana1.Descripcion));
			return resultado.FirstOrDefault();
		}

        public void AnadirArchivo(string seccion, Archivos archivo)
        {
            switch (seccion)
            {
                case "Documentos":
                    DatosAplicacion.CampanaSeleccionada.Recursos.Documentos.Add(archivo);
                    break;
                case "Lore":
                    DatosAplicacion.CampanaSeleccionada.Recursos.Lore.Add(archivo);
                    break;
                case "Media":
                    DatosAplicacion.CampanaSeleccionada.Recursos.Media.Add(archivo);
                    break;
                case "Fichas":
                    DatosAplicacion.CampanaSeleccionada.Recursos.Fichas.Add(archivo);
                    break;

                default:
                    break;
            }
            RecursosAplicacion.SesionUsuario.ReemplazarCampana();
        }

        public void EditarArchivo(string seccion, Archivos archivoviejo, Archivos archivo)
        {
            int indice = 0;
            switch (seccion)
            {
                case "documentos":
                    indice = DatosAplicacion.CampanaSeleccionada.Recursos.Documentos.FindIndex(a => a.NombreArchivo.Equals(archivoviejo.NombreArchivo));
                    DatosAplicacion.CampanaSeleccionada.Recursos.Documentos[indice] = archivo;
                    break;
                case "lore":
                    indice = DatosAplicacion.CampanaSeleccionada.Recursos.Lore.FindIndex(a => a.NombreArchivo.Equals(archivoviejo.NombreArchivo));
                    DatosAplicacion.CampanaSeleccionada.Recursos.Lore[indice] = archivo;
                    break;
                case "media":
                    indice = DatosAplicacion.CampanaSeleccionada.Recursos.Media.FindIndex(a => a.NombreArchivo.Equals(archivoviejo.NombreArchivo));
                    DatosAplicacion.CampanaSeleccionada.Recursos.Media[indice] = archivo;
                    break;
                case "fichas":
                    indice = DatosAplicacion.CampanaSeleccionada.Recursos.Fichas.FindIndex(a => a.NombreArchivo.Equals(archivoviejo.NombreArchivo));
                    DatosAplicacion.CampanaSeleccionada.Recursos.Fichas[indice] = archivo;
                    break;

                default:
                    break;
            }
            RecursosAplicacion.SesionUsuario.ReemplazarCampana();
        }

        public void EliminarArchivo(string seccion, Archivos archivo)
        {
            switch (seccion)
            {
                case "documentos":
                    DatosAplicacion.CampanaSeleccionada.Recursos.Documentos.Remove(archivo);
                    break;
                case "lore":
                    DatosAplicacion.CampanaSeleccionada.Recursos.Lore.Remove(archivo);
                    break;
                case "media":
                    DatosAplicacion.CampanaSeleccionada.Recursos.Media.Remove(archivo);
                    break;
                case "fichas":
                    DatosAplicacion.CampanaSeleccionada.Recursos.Fichas.Remove(archivo);
                    break;

                default:
                    break;
            }
            RecursosAplicacion.SesionUsuario.ReemplazarCampana();
        }
    }
}
