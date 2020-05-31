using System.Collections.Generic;

namespace ElEscribaDelDJ.Classes
{
    public class Campana
    {
		private string nombre;
		private string descripcion;
        private string imagen;
        private List<RecursosCampana> listarecursos = new List<RecursosCampana>();

		public string DireccionImagen
		{
			get { return imagen; }
			set { imagen = value; }
		}

		public List<RecursosCampana> RecursosCampana
		{
			get { return listarecursos; }
			set { listarecursos = value; }
		}

		public string Descripcion
		{
			get { return descripcion; }
			set { descripcion = value; }
		}

		public string NombreCampana
		{
			get { return nombre; }
			set { nombre = value; }
		}
    }
}
