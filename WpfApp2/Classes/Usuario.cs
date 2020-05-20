using System.Collections.Generic;

namespace ElEscribaDelDJ.Classes
{
    class Usuario
    {
        private string nombre;
        private string clave;
        private List<Campaign> listacampaigne;

        public List<Campaign> ListCampaignes
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
