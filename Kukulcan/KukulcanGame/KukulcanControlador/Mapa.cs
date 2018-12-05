using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KukulcanGame.KukulcanControlador
{
    class Mapa
    {
        /// <summary>
        /// Definimos un numero de estados que puede tomar nuestro GameState que serian 
        /// nuestras ventanas e inicializamos en SplashScreen
        /// </summary>
        public enum GameState
        {
            SplashScreen,
            Lobyy,
            SelectHand,
            Game,
            Scores,
            SaveScores,
            Credits
        }
        /// <summary>
        /// Hacemos referencia al enum de estados de juego
        /// </summary>
        public GameState pantallas; 

        public Mapa()
        {
            pantallas = GameState.SplashScreen;
        }
        /// <summary>
        /// Metodo para cambiar de estado de juego
        /// </summary>
        /// <returns>Splash Screen</returns>
        public GameState splashScreen()
        {
            return GameState.SplashScreen;
        }
        /// <summary>
        /// Metodo para cambiar de estado de juego
        /// </summary>
        /// <returns>Menu</returns>
        public GameState lobby()
        {
            return GameState.Lobyy;
        }
        /// <summary>
        /// Metodo para cambiar de estado de juego
        /// </summary>
        /// <returns>Seleccion de manos</returns>
        public GameState selectHand()
        {
            return GameState.SelectHand;
        }
        /// <summary>
        /// Metodo para cambiar de estado de juego
        /// </summary>
        /// <returns>Juego</returns>
        public GameState game()
        {
            return GameState.Game;
        }
        /// <summary>
        /// Metodo para cambiar de estado de juego
        /// </summary>
        /// <returns>Puntajes</returns>
        public GameState scores()
        {
            return GameState.Scores;
        }
        /// <summary>
        /// Metodo para cambiar de estado de juego
        /// </summary>
        /// <returns>Guardar Puntajes</returns>
        public GameState saveScores()
        {
            return GameState.SaveScores;
        }
        /// <summary>
        /// Metodo para cambiar de estado de juego
        /// </summary>
        /// <returns>Creditos</returns>
        public GameState credits()
        {
            return GameState.Credits;
        }

    }
}
