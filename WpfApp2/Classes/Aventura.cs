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

        public void AnadirArchivo(string seccion, Archivos archivo)
        {
            switch (seccion)
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

        public void EditarArchivo(string seccion, Archivos archivoviejo, Archivos archivo)
        {
            int indice = 0;
            switch (seccion)
            {
                case "documentos":
                    indice = DatosAplicacion.AventuraSeleccionada.Recursos.Documentos.FindIndex(a=>a.NombreArchivo.Equals(archivoviejo.NombreArchivo));
                    DatosAplicacion.AventuraSeleccionada.Recursos.Documentos[indice] = archivo;
                    break;
                case "lore":
                    indice = DatosAplicacion.AventuraSeleccionada.Recursos.Lore.FindIndex(a => a.NombreArchivo.Equals(archivoviejo.NombreArchivo));
                    DatosAplicacion.AventuraSeleccionada.Recursos.Lore[indice] = archivo;
                    break;
                case "media":
                    indice = DatosAplicacion.AventuraSeleccionada.Recursos.Media.FindIndex(a => a.NombreArchivo.Equals(archivoviejo.NombreArchivo));
                    DatosAplicacion.AventuraSeleccionada.Recursos.Media[indice] = archivo;
                    break;
                case "fichas":
                    indice = DatosAplicacion.AventuraSeleccionada.Recursos.Fichas.FindIndex(a => a.NombreArchivo.Equals(archivoviejo.NombreArchivo));
                    DatosAplicacion.AventuraSeleccionada.Recursos.Fichas[indice] = archivo;
                    break;

                default:
                    break;
            }
            RecursosAplicacion.SesionUsuario.ActualizarEscenario();
        }

        public void EliminarArchivo(string seccion, Archivos archivo)
        {
            switch (seccion)
            {
                case "documentos":
                    DatosAplicacion.AventuraSeleccionada.Recursos.Documentos.Remove(archivo);
                    break;
                case "lore":
                    DatosAplicacion.AventuraSeleccionada.Recursos.Lore.Remove(archivo);
                    break;
                case "media":
                    DatosAplicacion.AventuraSeleccionada.Recursos.Media.Remove(archivo);
                    break;
                case "fichas":
                    DatosAplicacion.AventuraSeleccionada.Recursos.Fichas.Remove(archivo);
                    break;

                default:
                    break;
            }
            RecursosAplicacion.SesionUsuario.ActualizarEscenario();
        }
    }
}
