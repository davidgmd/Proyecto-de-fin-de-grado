using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
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
		protected Recursos _recursos;
		private List<EscenarioCampana> _listaescenarios = new List<EscenarioCampana>();

        public List<EscenarioCampana> ListaEscenarios
        {
            get { return _listaescenarios; }
            set { _listaescenarios = value; }
        }


        public string DireccionImagen
		{
			get { return _imagen; }
			set { _imagen = value; }
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
	}
}
