﻿using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
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

        public void EditarArchivo(string seccion, Archivos archivo, int indice)
        {
            switch (seccion)
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

        public void EliminarArchivo(string seccion, Archivos archivo, int indice)
        {
            switch (seccion)
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
    }
}
