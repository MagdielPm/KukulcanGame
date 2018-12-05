using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KukulcanGame.KukulcanControlador
{
    class Body
    {
        /// <summary>
        /// Lista de vectores, donde posicionamos al snake
        /// </summary>
        public List<Vector2> snakePosition;
        /// <summary>
        /// Variables a utilizar en el juego siendo Direccion:
        /// 0 = arriba, 1 = abajo, 2 = izquierda y 3 = derecha
        /// puntaje, ultima direccion y el tiempo
        /// </summary>
        public int direccion = 3, puntaje, ltd = -1, lastT = -1;
        Frutita food;
        public Body()
        {
            food = new Frutita();
        }
        /// <summary>
        /// Inicializamos el juego cargando la lista de vectores para nuestro snake, le asignamos una direccion
        /// y vamos moviendo esa lista de vectores, creamos la comida e iniciamos el puntaje en 0
        /// </summary>
        public void startGame()
        {
            snakePosition = new List<Vector2>();
            direccion = 3;
            ltd = 3;
            for (int i = 0; i < 5; i++)
            { snakePosition.Add(new Vector2(9 - i, 12)); }
            food.resetComida();
            puntaje = 0;
        }
    }
}
