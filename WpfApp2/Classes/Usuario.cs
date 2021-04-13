using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using Octokit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public void ReemplazarCampana()
        {
            this.listacampaign[DatosAplicacion.IndiceCampana] = DatosAplicacion.CampanaSeleccionada;
            GestionArchivos.EscribirUsuarioLocal();
        }

        //Cuando se añade un escenario nuevo y hay que actualizar campaña para que tenga este nuevo escenario y luego escribir
        public void ActualizarCampana()
        {
            this.listacampaign[DatosAplicacion.IndiceCampana].ListaEscenarios[DatosAplicacion.IndiceEscenario] = DatosAplicacion.EscenarioSeleccionado;
            ReemplazarCampana();
        }

        //Cuando se añade una nueva aventura y hay que actualizar el escenario y la campana
        public void ActualizarEscenario()
        {
            this.listacampaign[DatosAplicacion.IndiceCampana].ListaEscenarios[DatosAplicacion.IndiceEscenario].ListaAventuras[DatosAplicacion.IndiceEscenario] = DatosAplicacion.AventuraSeleccionada;
            ActualizarCampana();
            ReemplazarCampana();
        }
    }
}
