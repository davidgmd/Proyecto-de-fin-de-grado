using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
