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

        public new void AnadirArchivo(string seccion, Archivos archivo)
        {
            switch (seccion.ToLower())
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

        public new void EditarArchivo(string seccion, Archivos archivo, int indice)
        {
            switch (seccion.ToLower())
            {
                case "documentos":
                    DatosAplicacion.EscenarioSeleccionado.Recursos.Documentos[indice] = archivo;
                    break;
                case "lore":
                    DatosAplicacion.EscenarioSeleccionado.Recursos.Lore[indice] = archivo;
                    break;
                case "media":
                    DatosAplicacion.EscenarioSeleccionado.Recursos.Media[indice] = archivo;
                    break;
                case "fichas":
                    DatosAplicacion.EscenarioSeleccionado.Recursos.Fichas[indice] = archivo;
                    break;

                default:
                    break;
            }
            RecursosAplicacion.SesionUsuario.ActualizarCampana();
        }

        public new void EliminarArchivo(string seccion, int indice)
        {
            switch (seccion.ToLower())
            {
                case "documentos":
                    DatosAplicacion.EscenarioSeleccionado.Recursos.Documentos.RemoveAt(indice);
                    break;
                case "lore":
                    DatosAplicacion.EscenarioSeleccionado.Recursos.Lore.RemoveAt(indice);
                    break;
                case "media":
                    DatosAplicacion.EscenarioSeleccionado.Recursos.Media.RemoveAt(indice);
                    break;
                case "fichas":
                    DatosAplicacion.EscenarioSeleccionado.Recursos.Fichas.RemoveAt(indice);
                    break;

                default:
                    break;
            }
            RecursosAplicacion.SesionUsuario.ActualizarCampana();
        }

        public new void AnadirResumen(Resumenes resumen)
        {
            DatosAplicacion.EscenarioSeleccionado.Recursos.Resumenes.Add(resumen);
            RecursosAplicacion.SesionUsuario.ActualizarCampana();
        }

        public new void EditarResumen(Resumenes resumen)
        {
            DatosAplicacion.EscenarioSeleccionado.Recursos.Resumenes.Remove(resumen);
            DatosAplicacion.EscenarioSeleccionado.Recursos.Resumenes.Add(resumen);
            RecursosAplicacion.SesionUsuario.ActualizarCampana();
        }

        public new void EliminarResumen(Resumenes resumen)
        {
            DatosAplicacion.EscenarioSeleccionado.Recursos.Resumenes.Remove(resumen);
            RecursosAplicacion.SesionUsuario.ActualizarCampana();
        }
    }
}
