using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
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

        public new void AnadirArchivo(string seccion, Archivos archivo)
        {
            switch (seccion.ToLower())
            {
                case "documentos":
                    DatosAplicacion.AventuraSeleccionada.Recursos.Documentos.Add(archivo);
                    break;
                case "lore":
                    DatosAplicacion.AventuraSeleccionada.Recursos.Lore.Add(archivo);
                    break;
                case "media":
                    DatosAplicacion.AventuraSeleccionada.Recursos.Media.Add(archivo);
                    break;
                case "fichas":
                    DatosAplicacion.AventuraSeleccionada.Recursos.Fichas.Add(archivo);
                    break;

                default:
                    break;
            }
            RecursosAplicacion.SesionUsuario.ActualizarEscenario();
        }

        public new void EditarArchivo(string seccion, Archivos archivo, int indice)
        {
            switch (seccion.ToLower())
            {
                case "documentos":
                    DatosAplicacion.AventuraSeleccionada.Recursos.Documentos[indice] = archivo;
                    break;
                case "lore":
                    DatosAplicacion.AventuraSeleccionada.Recursos.Lore[indice] = archivo;
                    break;
                case "media":
                    DatosAplicacion.AventuraSeleccionada.Recursos.Media[indice] = archivo;
                    break;
                case "fichas":
                    DatosAplicacion.AventuraSeleccionada.Recursos.Fichas[indice] = archivo;
                    break;

                default:
                    break;
            }
            RecursosAplicacion.SesionUsuario.ActualizarEscenario();
        }

        public new void EliminarArchivo(string seccion, int indice)
        {
            switch (seccion.ToLower())
            {
                case "documentos":
                    DatosAplicacion.AventuraSeleccionada.Recursos.Documentos.RemoveAt(indice);
                    break;
                case "lore":
                    DatosAplicacion.AventuraSeleccionada.Recursos.Lore.RemoveAt(indice);
                    break;
                case "media":
                    DatosAplicacion.AventuraSeleccionada.Recursos.Media.RemoveAt(indice);
                    break;
                case "fichas":
                    DatosAplicacion.AventuraSeleccionada.Recursos.Fichas.RemoveAt(indice);
                    break;

                default:
                    break;
            }
            RecursosAplicacion.SesionUsuario.ActualizarEscenario();
        }

        public new void AnadirResumen (Resumenes resumen)
        {
            DatosAplicacion.AventuraSeleccionada.Recursos.Resumenes.Add(resumen);
            RecursosAplicacion.SesionUsuario.ActualizarEscenario();
        }

        public new void EditarResumen (Resumenes resumen)
        {
            DatosAplicacion.AventuraSeleccionada.Recursos.Resumenes.Remove(resumen);
            DatosAplicacion.AventuraSeleccionada.Recursos.Resumenes.Add(resumen);
            RecursosAplicacion.SesionUsuario.ActualizarEscenario();
        }

        public new void EliminarResumen (Resumenes resumen)
        {
            DatosAplicacion.AventuraSeleccionada.Recursos.Resumenes.Remove(resumen);
            RecursosAplicacion.SesionUsuario.ActualizarEscenario();
        }
    }
}
