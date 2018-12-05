using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KukulcanGame.KukulcanControlador
{
    class Frutita
    {

        /// <summary>
        /// Definimos el random para generar posiciones aleatorias para la comida
        /// </summary>
        Random rPosition = new Random();
        public Vector2 food;
        public Frutita()
        {
            resetComida();
        }

        /// <summary>
        /// Este metodo reinicia la posicion de la comida en la pantalla
        /// </summary>
        public void resetComida()
        {
            food = new Vector2(
                rPosition.Next(1, (900 - 1) / 50),
                rPosition.Next(1, (650 - 1) / 50));
        }
    }
}
