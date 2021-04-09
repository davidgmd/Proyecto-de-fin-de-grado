using System;
using System.Collections.Generic;
using System.Text;

namespace ElEscribaDelDJ.Classes
{
    public class Aventura:EscenarioCampana
    {        
        private int _numeroaventura;

        public int NumeroAventura
        {
            get { return _numeroaventura; }
            set { _numeroaventura = value; }
        }

        public Aventura RecuperarAventura(EscenarioCampana escenario,string nombre)
        {
            Aventura aventura1 = escenario.ListaAventuras.Find(a => a.Nombre.Equals(nombre));
            return aventura1;
        }

        public void AnadirArchivo(List<string> nombres, string seccion, List<Archivos> archivos)
        {
            Campana campana1 = new Campana();
            campana1= campana1.RecuperarCampana(nombres[0]);

            EscenarioCampana escenario1 = new EscenarioCampana();
            escenario1 = escenario1.RecuperarEscenario(campana1, nombres[1]);

            Aventura aventura1 = new Aventura();
            aventura1 = RecuperarAventura(escenario1, nombres[2]);

            switch (seccion)
            {
                case "documentos":
                    aventura1.Recursos.Documentos = archivos;
                    break;
                case "lore":
                    aventura1.Recursos.Lore = archivos;
                    break;
                case "media":
                    aventura1.Recursos.Media = archivos;
                    break;
                case "fichas":
                    aventura1.Recursos.Fichas = archivos;
                    break;

                default:
                    break;
            }

        }
    }
}
