using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp2.Classes
{
    class DatosUsuario
    {
		private string nombreusuario;
		private int myVar;
		private Campaña campana;

		public Campaña Campana
		{
			get { return campana; }
			set { campana = value; }
		}


		public int MyProperty
		{
			get { return myVar; }
			set { myVar = value; }
		}


		public string nombreUsuario
		{
			get { return nombreusuario; }
			set { nombreusuario = value; }
		}

	}
}
