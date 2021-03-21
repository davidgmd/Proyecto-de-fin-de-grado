using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ElEscribaDelDJ.Classes
{
    public class Campana
    {
		protected string nombre;
		protected string descripcion;
        private string imagen;
        protected List<Archivos> listaarchivos = new List<Archivos>();
		protected List<Resumenes> listaresumenes = new List<Resumenes>();
		private List<EscenarioCampana> listaescenarios = new List<EscenarioCampana>();

        public List<EscenarioCampana> ListaEscenarios
        {
            get { return listaescenarios; }
            set { listaescenarios = value; }
        }


        public string DireccionImagen
		{
			get { return imagen; }
			set { imagen = value; }
		}

		public List<Archivos> ListaArchivos
		{
			get { return listaarchivos; }
			set { listaarchivos = value; }
		}

		public List<Resumenes> ListaResumenes
		{
			get { return listaresumenes; }
			set { listaresumenes = value; }
		}

		public string Descripcion
		{
			get { return descripcion; }
			set { descripcion = value; }
		}

		public string Nombre
		{
			get { return nombre; }
			set { nombre = value; }
		}

		public Campana ExisteCampanaSesion(Campana campana1)
        {
			var resultado = RecursosAplicacion.SesionUsuario.ListCampaigns.Find(c => c.nombre.Equals(campana1.nombre) && c.descripcion.Equals(campana1.descripcion));
			return resultado;
        }

		public Campana ExisteCampanaObservable(Campana campana1, ObservableCollection<Campana> observable)
		{
			var resultado = observable.Where(c => c.nombre.Equals(campana1.nombre) && c.descripcion.Equals(campana1.descripcion));
			return resultado.FirstOrDefault();
		}
	}
}
