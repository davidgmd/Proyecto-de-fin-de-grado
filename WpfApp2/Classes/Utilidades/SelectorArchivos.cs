using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ElEscribaDelDJ.Classes.Utilidades
{
    public class SelectorArchivos
    {
        public string SeleccionImagen()
        {
            string filename;
            OpenFileDialog dialog = new OpenFileDialog();
            //En filter primero indicamos el texto y separado por | indicamos las extensiones estas si se aplican
            dialog.Filter = "Image Files (*.png; *.jpg; *bmp)|*.png;*.jpg;*.jpeg;*.bmp";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (dialog.ShowDialog() == true)
            {
                return filename = dialog.FileName;
                
            }
            else
            {
                return null;
            }
        }

        public string MoverImagen(string nombrecampana, string direccionarchivo)
        {
            string carpeta = RecursosAplicacion.Directorios["imagenes_usuario"] + $"\\{RecursosAplicacion.SesionUsuario.NombreUsuario}\\{nombrecampana}\\icon\\";
            string fichero = Path.GetFileName(direccionarchivo);
            string direccionueva = carpeta + fichero;

            System.IO.Directory.CreateDirectory(carpeta);
            File.Copy(direccionarchivo, direccionueva, true);
            return direccionueva;

        }
    }
}
