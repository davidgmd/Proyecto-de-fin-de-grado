using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp2.Classes
{
    class Campaña
    {
		private string nombre;
		private string descripcion;
		private List<RecursosCampana> listarecursos;

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

		public string NombreCampaña
		{
			get { return nombre; }
			set { nombre = value; }
		}

    }
}
