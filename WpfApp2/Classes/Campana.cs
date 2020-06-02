using System.Collections.Generic;

namespace ElEscribaDelDJ.Classes
{
    public class Campana
    {
		protected string nombre;
		protected string descripcion;
        private string imagen;
        protected List<Recursos> listarecursos = new List<Recursos>();
        protected List<EscenarioCampana> listaescenarios;

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

		public List<Recursos> RecursosCampana
		{
			get { return listarecursos; }
			set { listarecursos = value; }
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
    }
}
