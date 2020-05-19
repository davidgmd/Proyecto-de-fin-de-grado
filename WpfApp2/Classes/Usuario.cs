using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp2.Classes
{
    class Usuario
    {
		private string nombre;
		private string password;
		private DatosUsuario datosusuario;

		public DatosUsuario datosUsuario
		{
			get { return datosusuario; }
			set { datosusuario = value; }
		}

		public string Password
		{
			get { return password; }
			set { password = value; }
		}

		public string NombreUsuario
		{
			get { return nombre; }
			set { 
				nombre = value;
			}
		}
	}
}
