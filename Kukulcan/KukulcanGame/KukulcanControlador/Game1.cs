using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using KukulcanGame.KukulcanControlador;
using KukulcanGame.KukulcanVista;

namespace KukulcanGame
{
 
    public class Game1 : Game
    {
        /// <summary>
        /// El graphics se genera por defecto para tomar los recursos de la pantalla al igual que 
        /// el spriteBatch c:
        /// </summary>
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        /// <summary>
        /// Esta variable nos ayuda a determinar si en la seleccion de mano el usuario levanto 
        /// ambas manos, cosa que no debe hacer
        /// Variable para determinar si el juego termina
        /// </summary>
        bool manosArriba = false, gameOver = false;
        
        /// <summary>
        /// Lista donde se almacenan los puntajes que retorna un metodo de la clase BDcomun
        /// </summary>
        List<string> lstPuntajes;

        /// <summary>
        /// Definimos los vectores para los objetos que vamos a utilizar en el juego
        /// Los vectores 2D es donde se van a guardar
        /// las posiciones de cada objeto
        /// </summary>
        
        Vector2  vcJugar, vcScores, vcExit;
        Vector2 vcSplash, vcCreditos,menu,vcMenu;
        Vector2 vcAumentarLetra1, vcAumentarLetra2, vcDisminuirLetra1, vcDisminuirLetra2, vcGuardarPuntaje, vcLetra1, vcLetra2 ; 


        /// <summary>
        /// Definimos los rectangulos para poder generar las intersecciones del evento del kinect
        /// </summary>
        Rectangle rcJugar, rcScores, rcExit, rcCreditos,rcMenu, rcMenu2;
        Rectangle rcAumentarLetra1, rcAumentarLetra2, rcDisminuirLetra1, rcDisminuirLetra2, rcGuardarPuntaje; 

        /// <summary>
        ///bSize es un valor generico para los tiles que manejamos que son de 50x50 pixeles
        ///sSize es el size de la culebrita y las variables de ajuste son para pintar el cuerpo 
        ///del jugador
        /// </summary>
        int   bSize = 50,sSize=49;
        /// <summary>
        /// Estas variables son necesarias para la animacion de la mano cuando se poscisiona sobre un boton en 
        /// la pantalla del juego 
        /// </summary>
        int frames = 0, fps = 24, increase = 0, velocidad = 450;

        /// <summary>
        /// Definimos variables donde guardaremos los sonidos que hemos de usar para el juego 
        /// tanto como la parte de comer, como los bricks, como el splash y la musica de fondo
        /// </summary>
        SoundEffect audioSplash;
        SoundEffectInstance SplashAudio;
        SoundEffect[] sonidos;
        SoundEffectInstance[] efectosSonidos;
        Song fondo;
        /// <summary>
        /// Lista de texturas para el cursor
        /// </summary>
        List<Texture2D> lstManoCargando;
        /// <summary>
        /// Variables usadas para guardar el nombre del usuario
        /// </summary>
        char letra1, letra2;
        string nombreUsuario="";
        /// <summary>
        /// Variable de texto para la impresion de letras en la pantalla
        /// </summary>
        SpriteFont texto, textoAlerta;
        SpriteFont mayan, Title;
        SpriteFont mayanBig;
        /// <summary>
        /// Referencia de las clases utilizadas
        /// </summary>
        Mapa mapa;
        Body snake;
        KinectTools tracker;
        Frutita f;
        Sprites tiles;

        /// <summary>
        /// Nuestro constructor del juego ejecuta en estos graficos el juego y 
        /// define la carpeta de contenido de donde se tomaran los recursos del juego
        /// Tambien se definen las dimensiones de la ventana dadas en Pixeles de 1500x750 pixeles
        /// </summary>
        public Game1()
        {
            /// <summary>
            /// Este codigo hace cosas muy simples, como generar la ventana de graficos
            /// ponerle nombre al juego y definir el nombre del Content usado para las imagenes
            /// algunas cosas se hacen por defecto
            /// </summary>
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Kukulcan Game";
            
            graphics.PreferredBackBufferHeight = 750;
            graphics.PreferredBackBufferWidth = 1500;
            /// <summary>
            /// tomamos valores para las letras del nombre del jugador
            /// 
            /// Tambien posicionamos el juego en el centro de la pantalla
            /// </summary>
            letra1 = (char)65;
            letra2 =(char)65;

            Window.Position = new Point(
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (graphics.PreferredBackBufferWidth / 2),
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - (graphics.PreferredBackBufferHeight / 2));
            /// Aqui iniciamos la lista de imagenes que tomara el cursor cuando haga un mouseOver
            lstManoCargando = new List<Texture2D>();
            mapa = new Mapa();
            snake = new Body();
            f = new Frutita();
            tracker = new KinectTools();
            tiles = new Sprites();
        }
        
        /// <summary>
        /// Iniciamos la lectura del kinect en este metodo
        /// </summary>
        protected override void Initialize()
        {
            tracker.leerKinect();
            base.Initialize();
        }
        /// <summary>
        /// Cargamos el contenido de cada textura con los graficos guardados en el content y sonidos
        /// </summary>
        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            /// <summary>
            /// Realizamos la carga de contenidos que estan en el content para asignarselos a las variables definidas
            /// en la parte superior del codigo 
            /// </summary>
            #region Texturas
            mayan = Content.Load<SpriteFont>("Mayan");
            mayanBig = Content.Load<SpriteFont>("MayanBig");
            Title = Content.Load<SpriteFont>("Titulo");
            tiles.loadSprites(Content);
            audioSplash = Content.Load<SoundEffect>("sonido");
            SplashAudio = audioSplash.CreateInstance();
            sonidos= new SoundEffect[3];
            sonidos[0] = Content.Load<SoundEffect>("boton");
            sonidos[1] = Content.Load<SoundEffect>("comida");
            sonidos[2] = Content.Load<SoundEffect>("explosion");

            efectosSonidos = new SoundEffectInstance[3];
            efectosSonidos[0] = sonidos[0].CreateInstance();
            efectosSonidos[1] = sonidos[1].CreateInstance();
            efectosSonidos[2] = sonidos[2].CreateInstance();

            tiles.ImagenManoDerecha = base.Content.Load<Texture2D>("handRight");

            for (int i = 1; i < 6; i++)
            {
                lstManoCargando.Add(base.Content.Load<Texture2D>("manoLoad" + i));
            }
            texto = base.Content.Load<SpriteFont>("File");
            textoAlerta = base.Content.Load<SpriteFont>("FontAlert");
                                                                     

            #endregion
            /// <summary>
            /// Definimos los rectangulos de algunos botones que usaremos en las pantallas del juego
            /// al igual que los sonidos y musica de fondo 
            /// </summary>
            vcSplash = new Vector2(365,20);
            vcJugar = new Vector2(215, 390);
            vcScores = new Vector2(615,390);
            vcExit = new Vector2(1015,390);
            vcCreditos = new Vector2(1100, 650);
            vcMenu = new Vector2(605,630);
            menu = new Vector2(0,0);

            vcAumentarLetra1 = new Vector2(550, 75); 
            vcAumentarLetra2 = new Vector2(850, 75); 
            vcDisminuirLetra1 = new Vector2(550, 425); 
            vcDisminuirLetra2 = new Vector2(850, 425); 
            vcLetra1 = new Vector2(550, 250);
            vcLetra2 = new Vector2(850, 250);
            vcGuardarPuntaje = new Vector2(600, 650); 

            fondo = Content.Load<Song>("fondoCancion");
            MediaPlayer.Play(fondo);
            MediaPlayer.Volume = 0.5f;
            MediaPlayer.IsRepeating = true;

        }
  
       /// <summary>
       /// Este metodo termina la ejecucion del kinect
       /// </summary>
        protected override void EndRun()
        {
            tracker.kinect.Stop();
        }
        
        /// <summary>
        /// Este metodo recibe como @parametro gameTime que es el tiempo de ejecucion del juego, es necesario
        /// para validar los movimientos del juego y controlar las posciones
        /// </summary>
        /// <param name="gameTime"></param>
        public void gameSnake(GameTime gameTime)
        {
            /// <summary>
            /// Validamos si la comida aparece en el cuerpo del snake, si aparece dentro, la 
            /// reseteamos y aparecemos la comida en otro lugar
            /// </summary>
            for (int i = 1; i < snake.snakePosition.Count; i++)
            {
                if (snake.snakePosition[i].X == f.food.X && snake.snakePosition[i].Y == f.food.Y)
                {
                    f.resetComida();
                }
            }
            ///Llamamos a la funcion que controla el snake mediante el kinect
            tracker.controlKinect(snake);
            /// <summary>
            /// Si apretamos ESC salimos automaticamente del juego
            /// </summary>
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            ///Aqui determinamos mediante el Bool si el juego termino, si si nos llevara a la ventana donde
            ///el usuario debe guardad su puntaje, si no, el juego continua
            if (gameOver) {
                gameOver = false;
                mapa.pantallas = mapa.saveScores();
            }
            /// <summary>
            /// En esta seccion del metodo se controla la velocidad del juego, tomamos la velocidad del gameTime
            /// y la distribuimos con los frames para que el snake vaya un poco mas lento, pero por cada 
            /// 5 elititos el snake va mas rapido, tambien se determina la direccion del snake 
            /// mediante los movimientos del jugador
            /// </summary>
            if (gameTime.TotalGameTime.TotalMilliseconds > snake.lastT + velocidad)
            {
                snake.ltd = snake.direccion;
                for (int i = snake.snakePosition.Count - 1; i > 0; i--)
                {
                    snake.snakePosition[i] = snake.snakePosition[i - 1];
                }
                switch (snake.direccion)
                {
                    case 0:
                        tiles.head = tiles.U;
                        snake.snakePosition[0] += new Vector2(0, -1);
                        break;
                    case 1:
                        tiles.head = tiles.D;
                        snake.snakePosition[0] += new Vector2(0, 1);
                        break;
                    case 2:
                        tiles.head = tiles.L;
                        snake.snakePosition[0] += new Vector2(-1, 0);
                        break;
                    case 3:
                        tiles.head = tiles.R;
                        snake.snakePosition[0] += new Vector2(1, 0);
                        break;
                }
                /// <summary>
                /// Agreamos un nuevo cuadrito al snake al determinar si hay una colision entre la comida y la cabeza del snake
                /// incrementamos el puntaje, agregamos un sonido y validamos el puntaje para ver si ira mas 
                /// rapido el snake 
                /// </summary>
                if (snake.snakePosition[0].X == f.food.X && snake.snakePosition[0].Y == f.food.Y)
                {
                    snake.snakePosition.Add(snake.snakePosition[snake.snakePosition.Count - 1]);
                    f.resetComida();
                    efectosSonidos[1].Play();

                    snake.puntaje = snake.puntaje +10;

                    if (snake.puntaje ==50|| snake.puntaje == 100 || snake.puntaje == 150)
                    {
                        velocidad -= 100;
                    }

                }
                /// <summary>
                /// Validamos la colision del snake con su propio cuerpo, si choca la cabeza con alguna parte de su cuerpo
                /// el juego habra terminado y cargamos las letras de la siguiente pantalla
                /// </summary>
                for (int i = 1; i < snake.snakePosition.Count; i++)
                {
                    if (snake.snakePosition[0].X == snake.snakePosition[i].X && snake.snakePosition[0].Y == snake.snakePosition[i].Y)
                    {
                        gameOver = true;
                        efectosSonidos[2].Play();
                        letra1 = (char)65;
                        letra2 = (char)65;
                    }
                }
                /// <summary>
                /// Si el snake choca con las paredes, pierde y cargamos las letras de la siguiente pantalla
                /// </summary>
                if (snake.snakePosition[0].X < 1 || snake.snakePosition[0].Y < 1 ||
                    snake.snakePosition[0].Y  > (650/ bSize) ||
                    snake.snakePosition[0].X  > (900 / bSize))
                {
                    gameOver = true;
                    efectosSonidos[2].Play();
                    letra1 = (char)65;
                    letra2 = (char)65;
                }
                snake.lastT = (int)gameTime.TotalGameTime.TotalMilliseconds;
            }

        }

        /// <summary>
        /// Este metodo de spriteBatches nos sirve para pintar los tiles del snake por cada posicion que tenga del vector
        /// pinta la cabeza, el cuerpo y la comida en el campo
        /// </summary>
        public void drawSnake()
        {
            int cont = 0;
            foreach (Vector2 posicion in snake.snakePosition)
            {
                if (cont != 0)
                {
                    spriteBatch.Draw(tiles.tile, new Rectangle((int)posicion.X * 50, (int)posicion.Y * 50, sSize, sSize), Color.White);
                }
                cont++;
            }
            spriteBatch.Draw(tiles.head, new Rectangle((int)snake.snakePosition[0].X * 50, (int)snake.snakePosition[0].Y * 50, sSize, sSize), Color.White);
            spriteBatch.Draw(tiles.tile, new Rectangle((int)snake.snakePosition[snake.snakePosition.Count - 1].X * 50, (int)snake.snakePosition[snake.snakePosition.Count - 1].Y * 50, sSize, sSize), Color.White);
            spriteBatch.Draw(tiles.comida, new Rectangle((int)f.food.X * 50, (int)f.food.Y * 50, sSize, sSize), Color.White);
        }
        
        /// <summary>
        /// Este metodo verifica que no haya dos manos levantadas, ya que no puedes jugar con dos manos y recibe 
        /// como parametro el tiempo del juego
        /// </summary>
        /// <param name="gameTime"></param>
        public void verificarMano(GameTime gameTime)
        {
            bool manoArrriba = false;
            if(tracker.puntoManoDerecha.Y < tracker.puntoCabeza.Y && tracker.puntoManoIzquierda.Y < tracker.puntoCabeza.Y)
            {
                manosArriba = true; 
            }
            if(tracker.puntoManoDerecha.Y > tracker.puntoCabeza.Y && tracker.puntoManoIzquierda.Y > tracker.puntoCabeza.Y)
            {
                manosArriba = false;
            }
            if (tracker.puntoManoDerecha.Y < tracker.puntoCabeza.Y && manosArriba == false)
            {
                increase += gameTime.ElapsedGameTime.Milliseconds;

                tiles.Lh = tiles.LhS;
                manoArrriba = true;
                if (increase >= 24000 / fps)
                {
                    tracker.control = 0;
                    velocidad = 450;
                    snake.startGame();
                    mapa.pantallas = mapa.game();
                    increase = 0;
                }
            }
            if (tracker.puntoManoIzquierda.Y < tracker.puntoCabeza.Y && manosArriba == false)
            {
                increase += gameTime.ElapsedGameTime.Milliseconds;
                tiles.Rh = tiles.RhS;
                manoArrriba = true;
                if (increase >= 24000 / fps)
                {
                    tracker.control = 1;
                    velocidad = 450;
                    snake.startGame();
                    mapa.pantallas = mapa.game();
                    increase = 0;
                }

            }
            if (manoArrriba == false)
            {
                tiles.Rh = tiles.RhA;
                tiles.Lh = tiles.LhA;
                increase = 0;
            }
        }
        /// <summary>
        /// Este método valida las intersecciones del cursor con los botones del lobby
        /// </summary>
        /// <param name="gameTime"></param>
        public void lobbyMap(GameTime gameTime)
        {
            bool interseccionBoton = false;
            rcJugar = new Rectangle((int)vcJugar.X, (int)vcJugar.Y, 300, 97);
            rcScores = new Rectangle((int)vcScores.X, (int)vcScores.Y, 300, 97);
            rcExit = new Rectangle((int)vcExit.X, (int)vcExit.Y, 300, 97);
            rcCreditos = new Rectangle((int)vcCreditos.X, (int)vcCreditos.Y, 300, 57);
            tracker.rectanguloCursor = new Rectangle((int)tracker.puntoManoDerecha.X, (int)tracker.puntoManoDerecha.Y, 60, 60);
            if (rcJugar.Intersects(tracker.rectanguloCursor))
            {
                tiles.btJugar = tiles.btnJugarMouse;
                interseccionBoton = true;
                increase += gameTime.ElapsedGameTime.Milliseconds;
                if (increase >= 10000 / fps)
                {
                    if (frames >= lstManoCargando.Count - 1)
                    {
                        mapa.pantallas = mapa.selectHand();
                        efectosSonidos[0].Play();
                        frames = 0;
                    }
                    else
                    {
                        frames++;
                    }
                    increase = 0;
                }
            }
            if (rcScores.Intersects(tracker.rectanguloCursor))
            {
                tiles.btScores = tiles.btnScoresMouse;
                interseccionBoton = true;
                increase += gameTime.ElapsedGameTime.Milliseconds;
                if (increase >= 10000 / fps)
                {
                    if (frames >= lstManoCargando.Count - 1)
                    {
                        mapa.pantallas = mapa.scores();
                        efectosSonidos[0].Play();
                        frames = 0;
                        try
                        {
                            lstPuntajes = new List<string>();
                            lstPuntajes = BDcomun.cargarPuntajes();
                            nombreUsuario = "";
                        }
                        catch (Exception ex)
                        {
                            nombreUsuario = ex.Message;
                        }
                    }
                    else
                    {
                        frames++;
                    }
                    increase = 0;
                }
            }
            if (rcExit.Intersects(tracker.rectanguloCursor))
            {
                tiles.btExit = tiles.btnExitMouse;
                interseccionBoton = true;
                increase += gameTime.ElapsedGameTime.Milliseconds;
                if (increase >= 10000 / fps)
                {
                    if (frames >= lstManoCargando.Count - 1)
                    {
                        MediaPlayer.Stop();
                        Exit();
                        frames = 0;
                    }
                    else
                    {
                        frames++;
                    }
                    increase = 0;
                }
            }
            if (rcCreditos.Intersects(tracker.rectanguloCursor))
            {
                tiles.creditos = tiles.btnCreditosMosue;
                interseccionBoton = true;
                increase += gameTime.ElapsedGameTime.Milliseconds;
                if (increase >= 10000 / fps)
                {
                    if (frames >= lstManoCargando.Count - 1)
                    {
                        mapa.pantallas = mapa.credits();
                        efectosSonidos[0].Play();
                        frames = 0;
                    }
                    else
                    {
                        frames++;
                    }
                    increase = 0;
                }
            }
            if (interseccionBoton == false)
            {
                tiles.btJugar = tiles.btnAuxJugar;
                tiles.btScores = tiles.btnAuxScores;
                tiles.btExit = tiles.btnAuxExit;
                tiles.creditos = tiles.btnAuxCreditos;
                frames = 0;
                increase = 0;
            }
        }
        /// <summary>
        /// Este método resive como parámetro el tiempo de juego, sólo genera la intesección del cursor con el botón menú
        /// </summary>
        /// <param name="gameTime"></param>
        public void creditosMap(GameTime gameTime)
        {
            bool interseccionBoton2 = false;
            rcMenu = new Rectangle((int)vcMenu.X, (int)vcMenu.Y, 300, 97);

            tracker.rectanguloCursor = new Rectangle((int)tracker.puntoManoDerecha.X, (int)tracker.puntoManoDerecha.Y, 60, 60);

            if (rcMenu.Intersects(tracker.rectanguloCursor))
            {
                tiles.btnMenu = tiles.btnMenuMouse;
                interseccionBoton2 = true;
                increase += gameTime.ElapsedGameTime.Milliseconds;
                if (increase >= 10000 / fps)
                {
                    if (frames >= lstManoCargando.Count - 1)
                    {
                        mapa.pantallas = mapa.lobby();
                        efectosSonidos[0].Play();
                        frames = 0;
                    }
                    else
                    {
                        frames++;
                    }
                    increase = 0;
                }
            }
            if (interseccionBoton2 == false)
            {
                tiles.btnMenu = tiles.btnAuxMenu;
                frames = 0;
                increase = 0;
            }
        }
        /// <summary>
        /// Este método muestra los puntajes del juego tomados desde modelo
        ///  valida las intersecciones del  cursor con el botón menú 
        /// </summary>
        /// <param name="gameTime"></param>
        public void verPuntajes(GameTime gameTime)
        {
            bool interseccionBoton2 = false;
            rcMenu2 = new Rectangle((int)vcMenu.X, (int)vcMenu.Y, 300, 97);
            tracker.rectanguloCursor = new Rectangle((int)tracker.puntoManoDerecha.X, (int)tracker.puntoManoDerecha.Y, 60, 60);

            if (rcMenu2.Intersects(tracker.rectanguloCursor))
            {
                tiles.btnMenu = tiles.btnMenuMouse;
                interseccionBoton2 = true;
                increase += gameTime.ElapsedGameTime.Milliseconds;
                if (increase >= 10000 / fps)
                {
                    if (frames >= lstManoCargando.Count - 1)
                    {
                        mapa.pantallas = mapa.lobby();
                        efectosSonidos[0].Play();
                        frames = 0;
                    }
                    else
                    {
                        frames++;
                    }
                    increase = 0;
                }
            }

            if (interseccionBoton2 == false)
            {
                tiles.btnMenu = tiles.btnAuxMenu;
                frames = 0;
                increase = 0;
            }
        }
        /// <summary>
        /// Guardar puntajes es el método en el cuál se ejecuta la lógica de guardar puntajes, se maneja el rectángulo
        /// del cursor generado por el kinect y resive como parámetro el tiempo de juego
        /// 
        /// El método valida las intersecciones del cursor con los botones y cambia las letras de la pantalla
        /// </summary>
        /// <param name="gameTime"></param>
        public void guardadPuntajes(GameTime gameTime)
        {
            rcAumentarLetra1 = new Rectangle((int)vcAumentarLetra1.X, (int)vcAumentarLetra1.Y, 100, 100);
            rcAumentarLetra2 = new Rectangle((int)vcAumentarLetra2.X, (int)vcAumentarLetra2.Y, 100, 100);
            rcDisminuirLetra1 = new Rectangle((int)vcDisminuirLetra1.X, (int)vcDisminuirLetra1.Y, 100, 100);
            rcDisminuirLetra2 = new Rectangle((int)vcDisminuirLetra2.X, (int)vcDisminuirLetra2.Y, 100, 100);
            rcGuardarPuntaje = new Rectangle((int)vcGuardarPuntaje.X, (int)vcGuardarPuntaje.Y, 300, 57);
            tracker.rectanguloCursor = new Rectangle((int)tracker.puntoManoDerecha.X, (int)tracker.puntoManoDerecha.Y, 60, 60);

            if (rcAumentarLetra1.Intersects(tracker.rectanguloCursor))
            {
                tiles.flechaUp = tiles.flechaUpMouse;
                increase += gameTime.ElapsedGameTime.Milliseconds;
                if (increase >= 20000 / fps)
                {
                    if (letra1 == 90)
                    {
                        letra1 = (char)65;
                    }
                    else
                    {
                        letra1++;
                    }
                    increase = 0;
                }
            }
            else
            {
                tiles.flechaUp = tiles.flechaUpAux;
                if (rcAumentarLetra2.Intersects(tracker.rectanguloCursor))
                {
                    tiles.flechaUp2 = tiles.flechaUpMouse;
                    increase += gameTime.ElapsedGameTime.Milliseconds;
                    if (increase >= 20000 / fps)
                    {
                        if (letra2 == 90)
                        {
                            letra2 = (char)65;
                        }
                        else
                        {
                            letra2++;
                        }
                        increase = 0;
                    }
                }
                else
                {
                    tiles.flechaUp2 = tiles.flechaUpAux;
                    if (rcDisminuirLetra1.Intersects(tracker.rectanguloCursor))
                    {
                        tiles.flechaDown = tiles.flechaDownMouse;
                        increase += gameTime.ElapsedGameTime.Milliseconds;
                        if (increase >= 20000 / fps)
                        {
                            if (letra1 == 65)
                            {
                                letra1 = (char)90;
                            }
                            else
                            {
                                letra1--;
                            }
                            increase = 0;
                        }
                    }
                    else
                    {
                        tiles.flechaDown = tiles.flechaDownAux;
                        if (rcDisminuirLetra2.Intersects(tracker.rectanguloCursor))
                        {
                            tiles.flechaDown2 = tiles.flechaDownMouse;
                            increase += gameTime.ElapsedGameTime.Milliseconds;
                            if (increase >= 20000 / fps)
                            {
                                if (letra2 == 65)
                                {
                                    letra2 = (char)90;
                                }
                                else
                                {
                                    letra2--;
                                }
                                increase = 0;
                            }
                        }
                        else
                        {
                            tiles.flechaDown2 = tiles.flechaDownAux;
                            if (rcGuardarPuntaje.Intersects(tracker.rectanguloCursor))
                            {
                                tiles.btScores = tiles.btnScoresMouse;
                                increase += gameTime.ElapsedGameTime.Milliseconds;
                                if (increase >= 10000 / fps)
                                {
                                    if (frames >= lstManoCargando.Count - 1)
                                    {
                                        nombreUsuario = Convert.ToString(letra1) + Convert.ToString(letra2);
                                        BDcomun.guardarPuntaje(nombreUsuario, snake.puntaje);
                                        efectosSonidos[0].Play();
                                        try
                                        {
                                            lstPuntajes = new List<string>();
                                            lstPuntajes = BDcomun.cargarPuntajes();
                                            nombreUsuario = "";
                                        }
                                        catch (Exception ex)
                                        {
                                            nombreUsuario = ex.Message;
                                        }
                                        mapa.pantallas = mapa.scores();
                                        frames = 0;
                                    }
                                    else
                                    {
                                        frames++;
                                    }
                                    increase = 0;
                                }
                            }
                            else
                            {
                                tiles.btScores = tiles.btnAuxScores;
                                frames = 0;
                                increase = 0;
                            }

                        }
                    }
                }
            }
        }
        /// <summary>
        /// Esta funcion es el bucle para la logica del juego, en este apartado se ejectutan las acciones del juego
        /// actualmente lu usamos para definir los estados del proyecto que vamos a usar 
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if ( Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            tracker.uptade(mapa);
            /// <summary>
            /// Este switch nos sirve para determinar los estados del juego, aqui se desarrolla la logica del juego
            /// y es el loop que nos ayuda a mover los vectores y rectangulos del juego
            /// Todo el juego se desarrolla en este loop y el de draw
            /// Cada case es un estado del juego, en cada case esta la logica utilizada en cada seccion, se realizaron 
            /// metodos para volver menos grande esta seccion, anque se espera que se pueda optimizar y volver modular
            /// </summary>
            switch (mapa.pantallas)
            {
                case Mapa.GameState.SplashScreen:
                    SplashAudio.Play();
                    if (gameTime.TotalGameTime.Seconds >= 3)
                    {
                        SplashAudio.Stop();
                        mapa.pantallas = mapa.lobby();
                    }
                    break;
                case Mapa.GameState.Lobyy:
                    lobbyMap(gameTime);
                    break;
                case Mapa.GameState.SelectHand:
                    IsMouseVisible = !true;
                    verificarMano(gameTime);

                    break;
                case Mapa.GameState.Game:
                    IsMouseVisible = !true;
                    gameSnake(gameTime);
                    break;
                case Mapa.GameState.Credits:
                    creditosMap(gameTime);
                    break;
                case Mapa.GameState.Scores:
                    verPuntajes(gameTime);
                    break;
                case Mapa.GameState.SaveScores:
                    guardadPuntajes(gameTime);
                    break;
            }
            base.Update(gameTime);
        }
        
        
        /// <summary>
        /// Este metodo recibe como parametro el tiempo de ejecucion del juego
        /// otro metodo nos ayuda a dibujar todo, al igual que el Update este esta seccionado por un switch y cases
        /// dependiendo del gameState es la pantalla que va a dibujar, todos los dibujos de sprites tienen que ir en 
        /// este metodo y siempre inicia con un spriteBatch.Begin(); y terminamos con un spriteBatch.End();
        /// 
        /// Cada estructura de spriteBatch.Draw() esta dada de la siguiente manera
        /// spriteBatch.Draw(parametroDeTextura,parametroDeVector,parametroDeColor);
        /// Siempre debe recibir una textura para poder tener algo que dibujar
        /// Siempre debe tener una posicion donde dibujar, para eso se le pasa el vector
        /// El vector se puede inicalizar arriba o bien, en esta seccion, pero es mejor arriba
        /// asi en esta seccion solamente se llama
        /// El color siempre tiene que ir, si no pasas ese parametro tendras un warning, si corre
        /// pero no es lo mas optimo, para que no cambie el color de la textura que tenemos le pasamos
        /// como parametro el Color.White que ya viene por defecto, pero podemos generar directamente 
        /// el color pasando parametros de color RGB al constructor de la clase new Color(Red, Green, Blue)
        /// tomando en cuenta que es del 0 al 255 en cada uno o bien del 00000 al FFFFFF
        /// 
        /// Cada spriteBatch.DrawString() esta dada de la siguiente manera
        /// spriteBatch.DrawString(tipoDeFuente,"el texto a imprimir",vectorDePosicion, colorDelTexto);
        /// La diferencia mas significativa del Draw regular es el tipo de fuente y el texto, lo demas es
        /// muy similar
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            switch (mapa.pantallas)
            {
                case Mapa.GameState.SplashScreen:
                    spriteBatch.Draw(tiles.bg, menu, Color.White);
                    spriteBatch.Draw(tiles.splashScrren, vcSplash, Color.White);
                    break;
                case Mapa.GameState.Lobyy:
                    bool interseccion = false;
                    spriteBatch.Draw(tiles.backGroundMenu, menu, Color.White);
                    spriteBatch.DrawString(Title, "KUKULCAN GAME", new Vector2(470, 250), new Color(208, 196, 188));
                    spriteBatch.Draw(tiles.btScores, vcScores, Color.White);
                    spriteBatch.Draw(tiles.btJugar, vcJugar, Color.White);
                    spriteBatch.Draw(tiles.btExit, vcExit, Color.White);
                    spriteBatch.Draw(tiles.creditos, vcCreditos, Color.White);

                    if (rcJugar.Intersects(tracker.rectanguloCursor))
                    {
                        interseccion = true;
                        spriteBatch.Draw(lstManoCargando[frames], tracker.rectanguloCursor, Color.White);
                    }

                    if (rcScores.Intersects(tracker.rectanguloCursor))
                    {
                        interseccion = true;
                        spriteBatch.Draw(lstManoCargando[frames], tracker.rectanguloCursor, Color.White);
                    }

                    if (rcCreditos.Intersects(tracker.rectanguloCursor))
                    {
                        interseccion = true;
                        spriteBatch.Draw(lstManoCargando[frames], tracker.rectanguloCursor, Color.White);
                    }

                    if (rcExit.Intersects(tracker.rectanguloCursor))
                    {
                        interseccion = true;
                        spriteBatch.Draw(lstManoCargando[frames], tracker.rectanguloCursor, Color.White);
                    }
                    if (interseccion == false)
                    {
                        spriteBatch.Draw(tiles.ImagenManoDerecha, tracker.rectanguloCursor, Color.White);
                    }
                    if (tracker.ZTorso < 1.5 && tracker.ZTorso > 0)
                    {
                        spriteBatch.DrawString(textoAlerta, "RETROCEDA UNOS CENTIMETROS", new Vector2(100, 370), Color.Red);

                    } else {
                        if (tracker.ZTorso > 2.5) {
                            spriteBatch.DrawString(textoAlerta, "AVANCE UNOS CENTIMETROS", new Vector2(100, 370), Color.Red);
                        }
                    }
                    break;
                case Mapa.GameState.SelectHand:
                    spriteBatch.Draw(tiles.bg, menu, Color.White);
                    spriteBatch.Draw(tiles.Rh, new Rectangle(75, 60, 600, 600), Color.White);
                    spriteBatch.Draw(tiles.Lh, new Rectangle(825, 60, 600, 600), Color.White);
                    tiles.border(!true, spriteBatch);
                    if (manosArriba == true)
                    {
                        spriteBatch.DrawString(textoAlerta, "LEVANTE SOLO UNA MANO", new Vector2(100, 370), Color.Red);
                    }
                    break;
                case Mapa.GameState.Game:
                    spriteBatch.Draw(tiles.bg, menu, Color.White);
                    tiles.border(true, spriteBatch);
                    spriteBatch.DrawString(Title, "Puntaje: " + snake.puntaje, new Vector2(1080, 80), new Color(208, 196, 188));
                    tracker.drawSkeletonTracking(spriteBatch, tiles.genericTile,snake);
                    drawSnake();
                    break;
                case Mapa.GameState.Credits:
                    spriteBatch.Draw(tiles.bg, menu, Color.White);
                    spriteBatch.Draw(tiles.natureLeftTop2, new Rectangle(-220, -57, 912, 538), Color.White);
                    spriteBatch.Draw(tiles.natureRightTop, new Rectangle(808, -57, 912, 538), Color.White);
                    spriteBatch.Draw(tiles.creditos, new Rectangle(620, 190, 250, 47), Color.White);
                    spriteBatch.Draw(tiles.logoMonkey, new Rectangle(615, 35, 280, 110), Color.White);
                    spriteBatch.DrawString(mayan, "Líder de proyecto:", new Vector2(170, 315), new Color(208, 196, 188));
                    spriteBatch.DrawString(mayan, "Magdiel Pech ", new Vector2(170, 365), new Color(208, 196, 188));
                    spriteBatch.DrawString(mayan, "Desarrolladores:", new Vector2(480, 315), new Color(208, 196, 188));
                    spriteBatch.DrawString(mayan, "Alvar Peniche", new Vector2(480, 365), new Color(208, 196, 188));
                    spriteBatch.DrawString(mayan, "Rodrigo Euan", new Vector2(480, 425), new Color(208, 196, 188));
                    spriteBatch.DrawString(mayan, "Diseñadores:", new Vector2(780, 315), new Color(208, 196, 188));
                    spriteBatch.DrawString(mayan, "Huriata Bonilla", new Vector2(780, 365), new Color(208, 196, 188));
                    spriteBatch.DrawString(mayan, "Luis Ávila", new Vector2(780, 425), new Color(208, 196, 188));
                    spriteBatch.DrawString(mayan, "Audio y sonidos:", new Vector2(1070, 315), new Color(208, 196, 188));
                    spriteBatch.DrawString(mayan, "Luis Ávila", new Vector2(1070, 365), new Color(208, 196, 188));
                    spriteBatch.DrawString(mayan, "Agradecimientos especiales:   Edgar Cambranes", new Vector2(330, 520), new Color(208, 196, 188));
                    spriteBatch.Draw(tiles.btnMenu, vcMenu, Color.White);

                    if (rcMenu.Intersects(tracker.rectanguloCursor))
                    {
                        interseccion = true;
                        spriteBatch.Draw(lstManoCargando[frames], tracker.rectanguloCursor, Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(tiles.ImagenManoDerecha, tracker.rectanguloCursor, Color.White);
                    }
                    break;
                case Mapa.GameState.Scores:
                    spriteBatch.Draw(tiles.bg, menu, Color.White);
                    spriteBatch.DrawString(mayan, nombreUsuario, new Vector2(100, 50), new Color(208, 196, 188));

                    tiles.cuadroPuntajes(spriteBatch);
                    spriteBatch.DrawString(mayan, "TOP:      NOMBRE:     PUNTAJE:", new Vector2(510, 210), new Color(208, 196, 188));
                    if (lstPuntajes.Count() > 0)
                    {
                        int y = 200;
                        for (int i = 0; i < lstPuntajes.Count(); i++)
                        {
                            spriteBatch.DrawString(mayan, lstPuntajes[i], new Vector2(530, y += 50), new Color(208, 196, 188));
                        }
                    }
                    else {

                        spriteBatch.DrawString(mayan, "CARGANDO PUNTAJES...", new Vector2(530, 400), new Color(208, 196, 188));
                    }
                    spriteBatch.Draw(tiles.natureLeftTop2, new Rectangle(-220, -57, 912, 538), Color.White);
                    spriteBatch.Draw(tiles.natureRightTop, new Rectangle(808, -57, 912, 538), Color.White);
                    spriteBatch.Draw(tiles.top5, new Rectangle(600, 35, 300, 57), Color.White);
                    spriteBatch.Draw(tiles.btnMenu, vcMenu, Color.White);
    
                    if (rcMenu2.Intersects(tracker.rectanguloCursor))
                    {
                        spriteBatch.Draw(lstManoCargando[frames], tracker.rectanguloCursor, Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(tiles.ImagenManoDerecha, tracker.rectanguloCursor,Color.White);
                    }
                    break;

                case Mapa.GameState.SaveScores:
                    tiles.saveScores(spriteBatch, menu, rcAumentarLetra1, rcAumentarLetra2, rcDisminuirLetra1, rcDisminuirLetra2, vcMenu);
                    spriteBatch.DrawString(mayanBig,Convert.ToString(letra1), vcLetra1, new Color(208, 196, 188));
                    spriteBatch.DrawString(mayanBig, Convert.ToString(letra2), vcLetra2, new Color(208, 196, 188));
                    spriteBatch.DrawString(mayan, "Puntaje obtenido: "+ Convert.ToInt32(snake.puntaje), new Vector2(600, 10), Color.White);


                    if (rcGuardarPuntaje.Intersects(tracker.rectanguloCursor))
                    {
                        spriteBatch.Draw(lstManoCargando[frames], tracker.rectanguloCursor, Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(tiles.ImagenManoDerecha, tracker.rectanguloCursor, Color.White);
                    }
                    if (tracker.ZTorso < 1.5 && tracker.ZTorso > 0)
                    {
                        spriteBatch.DrawString(textoAlerta, "RETROCEDA UNOS CENTIMETROS", new Vector2(100, 370), Color.Red);   
                    }
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
