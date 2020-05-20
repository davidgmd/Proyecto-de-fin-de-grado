namespace ElEscribaDelDJ.Classes
{
    class RecursosCampana
    {
		private string nombrerecurso;
		private string direccionrecurso;
		private string tiporecurso;
		private string extensionrecurso;

		public string ExtensionRecurso
		{
			get { return extensionrecurso; }
			set { extensionrecurso = value; }
		}

		public string TipoRecurso
		{
			get { return tiporecurso; }
			set { tiporecurso = value; }
		}

		public string DireccionRecurso
		{
			get { return direccionrecurso; }
			set { direccionrecurso = value; }
		}

		public string NombreRecurso
		{
			get { return nombrerecurso; }
			set { nombrerecurso = value; }
		}

	}
}
