using System.Collections.Generic;

namespace ElEscribaDelDJ.Classes
{
    class Usuario
    {
        private string nombre;
        private string clave;      
        private List<Campana> listacampaigne;
        private string correoelectronico;

        public string Correo
        {
            get { return correoelectronico; }
            set { correoelectronico = value; }
        }

        public List<Campana> ListCampaignes
        {
            get { return listacampaigne; }
            set { listacampaigne = value; }
        }

        public string NombreUsuario
        {
            get { return nombre; }
            set { nombre = value; }
        }
      
        public string Clave
        {
            get { return clave; }
            set { clave = value; }
        }

    }
}
