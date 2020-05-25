using System;
using System.Collections.Generic;
using System.Text;

namespace ElEscribaDelDJ.Classes
{
    class Randomizar
    {
        private int longitud;

        public int Longitud
        {
            get { return longitud; }
            set { longitud = value; }
        }


        public Randomizar(int minimo, int maximo)
        {
            Random random = new Random();
            this.longitud = random.Next(minimo, maximo);
        }

        // Generate un numero al azar entre 0 y 9  
        public int NumeroAlAzar()
        {
            Random random = new Random();
            return random.Next(0, 9);
        }

        // Generate una letra al azar  
        public Char LetraAlAzar()
        {
            Random random = new Random();
            char ch;
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            return ch;
        }

    }
}
