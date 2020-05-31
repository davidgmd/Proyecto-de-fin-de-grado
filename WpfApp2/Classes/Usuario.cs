using System.Collections.Generic;

namespace ElEscribaDelDJ.Classes
{
    public class Usuario
    {
        private string nombre;
        private string clave;      
        private List<Campana> listacampaign = new List<Campana>();
        private string correoelectronico;

        public string Correo
        {
            get { return correoelectronico; }
            set { correoelectronico = value; }
        }

        public List<Campana> ListCampaigns
        {
            get { return listacampaign; }
            set { listacampaign = value; }
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
