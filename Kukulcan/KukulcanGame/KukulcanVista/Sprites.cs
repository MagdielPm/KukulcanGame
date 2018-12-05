using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KukulcanGame.KukulcanVista
{
    class Sprites
    {
        /// <summary>
        /// Declaramos los tipos de texturas que vamos a manejar en el proyecto
        /// las texturas son las imagenes que vamos a estar usando
        /// </summary>
        public Texture2D genericTile, btJugar, btScores, btExit, head, bg, tile, U, D, R, L;
        public Texture2D btnJugarMouse, btnScoresMouse, btnExitMouse, btnCreditosMosue;
        public Texture2D brick, natureLeftTop, natureLeftBot, natureRightTop;
        public Texture2D splashScrren, logoMonkey, creditos, backGroundMenu;
        public Texture2D btnAuxJugar, btnAuxScores, btnAuxExit, btnAuxCreditos, btnAuxMenu;
        public Texture2D br1, br2, br3, br4, br5, natureLeftTop2, btnMenu, btnMenuMouse;
        public Texture2D ImagenManoDerecha, top5, logoSnake, flechaUpAux, flechaUpMouse, flechaDownAux, flechaDownMouse;
        public Texture2D flechaUp, flechaDown, Rh, Lh, RhS, LhS, RhA, LhA, comida;
        public Texture2D flechaUp2, flechaDown2;
        int bSize = 50;
        public Sprites()
        {

        }
        /// <summary>
        /// Recibe como parámetro la dirección del content manager como referencia
        /// Este método carga los sprites del juego
        /// </summary>
        /// <param name="Content"></param>
        public void loadSprites(ContentManager Content)
        {
            genericTile = Content.Load<Texture2D>("Tile");
            comida = Content.Load<Texture2D>("elote");
            bg = Content.Load<Texture2D>("Bg");
            Rh = Content.Load<Texture2D>("Rh");
            RhA = Rh;
            RhS = Content.Load<Texture2D>("RhSelect");
            Lh = Content.Load<Texture2D>("Lh");
            LhA = Lh;
            LhS = Content.Load<Texture2D>("LhSelect");
            logoSnake = Content.Load<Texture2D>("logo");
            head = Content.Load<Texture2D>("HeadR");
            R = Content.Load<Texture2D>("HeadR");
            L = Content.Load<Texture2D>("HeadL");
            D = Content.Load<Texture2D>("HeadD");
            U = Content.Load<Texture2D>("HeadU");
            tile = Content.Load<Texture2D>("TileSnake");
            btJugar = Content.Load<Texture2D>("btnJugar");
            btnMenu = Content.Load<Texture2D>("btnMenu");
            btnMenuMouse = Content.Load<Texture2D>("btnMenuMouse");
            btnAuxMenu = Content.Load<Texture2D>("btnMenu");
            btnAuxJugar = Content.Load<Texture2D>("btnJugar");
            btnJugarMouse = Content.Load<Texture2D>("btnJugarMouse");
            btScores = Content.Load<Texture2D>("btnPuntajes");
            btnAuxScores = Content.Load<Texture2D>("btnPuntajes");
            btnScoresMouse = Content.Load<Texture2D>("btnPuntajesMouse");
            btExit = Content.Load<Texture2D>("btnSalir");
            btnAuxExit = Content.Load<Texture2D>("btnSalir");
            btnExitMouse = Content.Load<Texture2D>("btnSalirMouse");
            brick = Content.Load<Texture2D>("brick");
            natureLeftBot = Content.Load<Texture2D>("planLeftbot");
            natureLeftTop = Content.Load<Texture2D>("planLeftTop");
            natureRightTop = Content.Load<Texture2D>("planRightTop");
            natureLeftTop2 = Content.Load<Texture2D>("planRightLeft2");
            splashScrren = Content.Load<Texture2D>("imagenSplash");
            logoMonkey = Content.Load<Texture2D>("MonkeyLogo");
            creditos = Content.Load<Texture2D>("btnCreditos");
            btnAuxCreditos = Content.Load<Texture2D>("btnCreditos");
            btnCreditosMosue = Content.Load<Texture2D>("btnCreditosMouse");
            backGroundMenu = Content.Load<Texture2D>("backGroundMenu");
            br1 = Content.Load<Texture2D>("brick1");
            br2 = Content.Load<Texture2D>("brick2");
            br3 = Content.Load<Texture2D>("brick3");
            br4 = Content.Load<Texture2D>("brick4");
            br5 = Content.Load<Texture2D>("brick5");
            top5 = Content.Load<Texture2D>("top5");
            flechaUp = Content.Load<Texture2D>("brickTop");
            flechaDown = Content.Load<Texture2D>("brickBot");
            flechaDownMouse = Content.Load<Texture2D>("brickBotMouse");
            flechaUpMouse = Content.Load<Texture2D>("brickTopMouse");
            flechaDownAux = flechaDown;
            flechaUpAux = flechaUp;
            flechaDown2 = flechaDown;
            flechaUp2 = flechaUp;
        }
        /// <summary>
        /// /// Este metodo dibuja los contornos de la pantalla en el state de juego y seleccion de mano
        /// cuando @dato == true dibuja la seccion del juego
        /// cuando @dato == false dibuja la seccion de seleccion de manos
        /// Recibe referencia de spriteBatch
        /// </summary>
        /// <param name="dato"></param>
        /// <param name="spriteBatch"></param>
        public void border(bool dato, SpriteBatch spriteBatch)
        {
            if (dato)
            {
                for (int i = 1; i < 14; i++)
                {
                    if (i % 2 == 0)
                    {
                        spriteBatch.Draw(br3, new Rectangle(1500 - (bSize * 11), i * bSize, bSize, bSize), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(brick, new Rectangle(1500 - (bSize * 11), i * bSize, bSize, bSize), Color.White);
                    }
                }
                for (int i = 20; i < 29; i++)
                {
                    if (i % 2 == 0)
                    {
                        spriteBatch.Draw(br3, new Rectangle(i * bSize, 0 + (bSize * 3), bSize, bSize), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(brick, new Rectangle(i * bSize, 0 + (bSize * 3), bSize, bSize), Color.White);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    if (i % 2 == 0)
                    {
                        spriteBatch.Draw(br3, new Rectangle(0 + (bSize * 15) - 25, i * bSize, bSize, bSize), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(brick, new Rectangle(0 + (bSize * 15) - 25, i * bSize, bSize, bSize), Color.White);
                    }
                }
            }
            for (int i = 1; i < 14; i++)
            {
                if (i % 2 == 0)
                {
                    spriteBatch.Draw(br4, new Rectangle(0, i * bSize, bSize, bSize), Color.White);
                }
                else
                {
                    spriteBatch.Draw(brick, new Rectangle(0, i * bSize, bSize, bSize), Color.White);
                }
                if (i % 2 == 0)
                {
                    spriteBatch.Draw(br1, new Rectangle(1500 - bSize, i * bSize, bSize, bSize), Color.White);
                }
                else
                {
                    spriteBatch.Draw(brick, new Rectangle(1500 - bSize, i * bSize, bSize, bSize), Color.White);
                }
            }
            for (int i = 0; i < 30; i++)
            {
                if (i % 2 == 0)
                {
                    spriteBatch.Draw(br5, new Rectangle(i * bSize, 0, bSize, bSize), Color.White);
                }
                else
                {
                    spriteBatch.Draw(brick, new Rectangle(i * bSize, 0, bSize, bSize), Color.White);
                }
                if (i % 2 == 0)
                {
                    spriteBatch.Draw(br2, new Rectangle(i * bSize, 750 - bSize, bSize, bSize), Color.White);
                }
                else
                {
                    spriteBatch.Draw(brick, new Rectangle(i * bSize, 750 - bSize, bSize, bSize), Color.White);
                }
            }
        }
        /// <summary>
        /// Este metodo solo dibuja el cuadro utilizado en la pantalla puntajes, que es el cuadrado de los tiles
        /// Recibe como parámetro una referencia de objeto spriteBatch
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void cuadroPuntajes(SpriteBatch spriteBatch)
        {
            for (int i = 8; i < 22; i++)
            {
                spriteBatch.Draw(brick, new Rectangle(i * bSize, 150, bSize, bSize), Color.White);
                spriteBatch.Draw(brick, new Rectangle(i * bSize, 550, bSize, bSize), Color.White);
            }
            for (int i = 4; i < 11; i++)
            {
                spriteBatch.Draw(brick, new Rectangle(1050, i * bSize, bSize, bSize), Color.White);
                spriteBatch.Draw(brick, new Rectangle(400, i * bSize, bSize, bSize), Color.White);
            }
        }
        /// <summary>
        /// Metodo para generar la vista de guardar puntajes con parametros establecitos
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="menu"></param>
        /// <param name="rcAumentarLetra1"></param>
        /// <param name="rcAumentarLetra2"></param>
        /// <param name="rcDisminuirLetra1"></param>
        /// <param name="rcDisminuirLetra2"></param>
        /// <param name="vcMenu"></param>
        public void saveScores(SpriteBatch spriteBatch, Vector2 menu, Rectangle rcAumentarLetra1, Rectangle rcAumentarLetra2, Rectangle rcDisminuirLetra1,
            Rectangle rcDisminuirLetra2, Vector2 vcMenu)
        {
            spriteBatch.Draw(bg, menu, Color.White);
            spriteBatch.Draw(natureLeftTop2, new Rectangle(-220, -57, 912, 538), Color.White);
            spriteBatch.Draw(natureRightTop, new Rectangle(808, -57, 912, 538), Color.White);
            spriteBatch.Draw(flechaUp, rcAumentarLetra1, new Color(208, 196, 188));
            spriteBatch.Draw(flechaUp2, rcAumentarLetra2, new Color(208, 196, 188));
            spriteBatch.Draw(flechaDown, rcDisminuirLetra1, new Color(208, 196, 188));
            spriteBatch.Draw(flechaDown2, rcDisminuirLetra2, new Color(208, 196, 188));
            spriteBatch.Draw(btScores, vcMenu, Color.White);
        }
    }
}
