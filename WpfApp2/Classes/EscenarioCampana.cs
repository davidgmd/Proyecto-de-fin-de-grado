using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElEscribaDelDJ.Classes
{
    public class EscenarioCampana:Campana
    {
        private List<Aventura> _listaaventuras = new List<Aventura>();
      
        public List<Aventura> ListaAventuras
        {
            get { return _listaaventuras; }
            set { _listaaventuras = value; }
        }

        public void AnadirArchivo(string seccion, Archivos archivo)
        {
            switch (seccion)
            {
                case "documentos":
                    DatosAplicacion.EscenarioSeleccionado.Recursos.Documentos.Add(archivo);
                    break;
                case "lore":
                    DatosAplicacion.EscenarioSeleccionado.Recursos.Lore.Add(archivo);
                    break;
                case "media":
                    DatosAplicacion.EscenarioSeleccionado.Recursos.Media.Add(archivo);
                    break;
                case "fichas":
                    DatosAplicacion.EscenarioSeleccionado.Recursos.Fichas.Add(archivo);
                    break;

                default:
                    break;
            }
            RecursosAplicacion.SesionUsuario.ActualizarCampana();
        }

        public void EditarArchivo(string seccion, Archivos archivoviejo, Archivos archivo)
        {
            int indice = 0;
            switch (seccion)
            {
                case "documentos":
                    indice = DatosAplicacion.EscenarioSeleccionado.Recursos.Documentos.FindIndex(a => a.NombreArchivo.Equals(archivoviejo.NombreArchivo));
                    DatosAplicacion.EscenarioSeleccionado.Recursos.Documentos[indice] = archivo;
                    break;
                case "lore":
                    indice = DatosAplicacion.EscenarioSeleccionado.Recursos.Lore.FindIndex(a => a.NombreArchivo.Equals(archivoviejo.NombreArchivo));
                    DatosAplicacion.EscenarioSeleccionado.Recursos.Lore[indice] = archivo;
                    break;
                case "media":
                    indice = DatosAplicacion.EscenarioSeleccionado.Recursos.Media.FindIndex(a => a.NombreArchivo.Equals(archivoviejo.NombreArchivo));
                    DatosAplicacion.EscenarioSeleccionado.Recursos.Media[indice] = archivo;
                    break;
                case "fichas":
                    indice = DatosAplicacion.EscenarioSeleccionado.Recursos.Fichas.FindIndex(a => a.NombreArchivo.Equals(archivoviejo.NombreArchivo));
                    DatosAplicacion.EscenarioSeleccionado.Recursos.Fichas[indice] = archivo;
                    break;

                default:
                    break;
            }
            RecursosAplicacion.SesionUsuario.ActualizarCampana();
        }

        public void EliminarArchivo(string seccion, Archivos archivo)
        {
            switch (seccion)
            {
                case "documentos":
                    DatosAplicacion.EscenarioSeleccionado.Recursos.Documentos.Remove(archivo);
                    break;
                case "lore":
                    DatosAplicacion.EscenarioSeleccionado.Recursos.Lore.Remove(archivo);
                    break;
                case "media":
                    DatosAplicacion.EscenarioSeleccionado.Recursos.Media.Remove(archivo);
                    break;
                case "fichas":
                    DatosAplicacion.EscenarioSeleccionado.Recursos.Fichas.Remove(archivo);
                    break;

                default:
                    break;
            }
            RecursosAplicacion.SesionUsuario.ActualizarCampana();
        }
    }
}
