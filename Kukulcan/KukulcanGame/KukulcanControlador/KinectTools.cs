using Microsoft.Kinect;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KukulcanGame.KukulcanControlador
{
    class KinectTools
    {
        /// <summary>
        /// Declaramos un objeto de tipo Kinect para leer su informacion
        /// </summary>
        public KinectSensor kinect;
        /// <summary>
        /// Definimos los rectangulos del skeleton para seguirlo y poder dibujarlo 
        /// </summary>
        public Rectangle rectanguloCabeza, rectanguloHombroDerecho
            , rectanguloHombroIzquierdo, rectanguloCodoIzquierdo, rectanguloCodoDerecho, rectanguloRodillaDerecho, rectanguloRodillaIzquierdo
            , rectangulopPieDerecho, rectanguloPieIzquierdo, rectanguloTorso, rectanguloManoIzquierda, rectanguloHombroCentro, rectanguloCursor;
        /// <summary>
        /// Spritesfots y puntos de las articulaciones del cuerpo del jugador tomados del kinect
        /// </summary>
        /// 
        public ColorImagePoint puntoManoDerecha;
        public ColorImagePoint puntoManoIzquierda;
        public ColorImagePoint puntoTorso;
        public ColorImagePoint puntoHombroDerecho;
        public ColorImagePoint puntoHombroIzquierdo;
        public ColorImagePoint puntoCabeza;
        public ColorImagePoint puntoCodoIzquierdo;
        public ColorImagePoint puntoCodoDerecho;
        public ColorImagePoint puntoRodillaDerecho;
        public ColorImagePoint puntoRodillaIzquierdo;
        public ColorImagePoint puntoPieDerecho;
        public ColorImagePoint puntoPieIzquierdo;
        public ColorImagePoint puntoHombroCentro;
        /// <summary>
        /// Variables usadas para guardar la profundidad de algunos puntos del cuerpo necesarios
        /// </summary>
        public float ZDerecha, ZIzquierda, ZTorso;
        public int control=0, ajuste = 910, ajusteV = 220;
        bool pantallaJuego = false;
        Mapa pantallas;
        

        public KinectTools()
        {
            pantallas = new Mapa();
        }
        /// <summary>
        /// En este metodo generamos la lectura del kinect Mediante un manejo de exepciones
        /// </summary>
        public void leerKinect()
        {
            try
            {
                kinect = KinectSensor.KinectSensors.FirstOrDefault();
                kinect.SkeletonStream.Enable();
                kinect.Start();
                kinect.SkeletonFrameReady += Kinect_SkeletonFrameReady;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("No se pudo establcer conexión con el Kinect " + ex.Message, ". Por favor conecte el Kinect");
                leerKinect();
            }
        }
        /// <summary>
        /// Ese método nos ayuda a determinar el estado de juego, recibiendo como parámetro
        /// la referencia de la clase Mapa
        /// </summary>
        /// <param name="GameMap"></param>
        public void uptade(Mapa GameMap)
        {
            if (GameMap.pantallas.Equals(GameMap.game()))
            {
                pantallaJuego = true;
            }
            else
            {
                pantallaJuego = false;
            }
        }
        
        /// <summary>
        /// Este método nos ayuda a controlar el Snake recibiendo una referencia de la clase Snake
        /// mediante el cuál se determina la dirección de la culebrita dependiendo del movimiento del jugador
        /// </summary>
        /// <param name="snake"></param>
        public void controlKinect(Body snake)
        {
            if (control == 0)
            {
                if (puntoManoDerecha.Y < 77 && snake.ltd != 1)
                    snake.direccion = 0;
                else
                if (puntoManoDerecha.Y >= 340 && snake.ltd != 0)
                    snake.direccion = 1;
                else
                if (puntoManoDerecha.X <= 159 && snake.ltd != 3)
                    snake.direccion = 2;
                else
                if (puntoManoDerecha.X >= 434 && snake.ltd != 2)
                    snake.direccion = 3;
            }
            else if (control == 1)
            {
                if (puntoManoIzquierda.Y < 77 && snake.ltd != 1)
                    snake.direccion = 0;
                else
            if (puntoManoIzquierda.Y >= 340 && snake.ltd != 0)
                    snake.direccion = 1;
                else
            if (puntoManoIzquierda.X <= 159 && snake.ltd != 3)
                    snake.direccion = 2;
                else
            if (puntoManoIzquierda.X >= 434 && snake.ltd != 2)
                    snake.direccion = 3;
            }
        }

        /// <summary>
        ///  En este metodo se realiza el trackeo de las articulaciones del jugador
        /// definiendo las articulaciones del jugador, trakeando sus movimientos, recibe como parámetros
        /// los eventos de lectura del kinect y un objeto sender 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Kinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            Skeleton[] esqueletos = null;
            using (SkeletonFrame frameEsqueleto = e.OpenSkeletonFrame())
            {
                if (frameEsqueleto != null)
                {
                    esqueletos = new Skeleton[frameEsqueleto.SkeletonArrayLength];
                    frameEsqueleto.CopySkeletonDataTo(esqueletos);
                }
            }
            if (esqueletos == null)
            {
                return;
            }
            foreach (Skeleton esqueleto in esqueletos)
            {
                if (esqueleto.TrackingState == SkeletonTrackingState.Tracked)
                {
                    /// <summary>
                    /// Aqui solo estan trackeando todas las articulaciones del cuerpo del jugador
                    /// </summary>
                    Joint handJointRight = esqueleto.Joints[JointType.HandRight];
                    Joint handJointLeft = esqueleto.Joints[JointType.HandLeft];
                    Joint Torso = esqueleto.Joints[JointType.Spine];
                    Joint hombroDerecho = esqueleto.Joints[JointType.ShoulderRight];
                    Joint hombroIzquierdo = esqueleto.Joints[JointType.ShoulderLeft];
                    Joint cabeza = esqueleto.Joints[JointType.Head];
                    Joint codoIzquierdo = esqueleto.Joints[JointType.ElbowLeft];
                    Joint codoDerecho = esqueleto.Joints[JointType.ElbowRight];
                    Joint rodillaIzquierdo = esqueleto.Joints[JointType.KneeLeft];
                    Joint rodillaDerecho = esqueleto.Joints[JointType.KneeRight];
                    Joint pieIzquierdo = esqueleto.Joints[JointType.FootLeft];
                    Joint pieDerecho = esqueleto.Joints[JointType.FootRight];
                    Joint hombroCentro = esqueleto.Joints[JointType.ShoulderCenter];
                    /// <summary>
                    /// Esta seccion toma los valores de cada articulacion y se las asigna
                    /// a cada ColorImagePointer declarado en la seccion superior
                    /// </summary>
                    ZDerecha = handJointRight.Position.Z;
                    ZIzquierda = handJointLeft.Position.Z;
                    ZTorso = Torso.Position.Z;
                    puntoManoDerecha = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(handJointRight.Position, ColorImageFormat.RgbResolution640x480Fps30);
                    puntoManoIzquierda = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(handJointLeft.Position, ColorImageFormat.RgbResolution640x480Fps30);
                    puntoHombroDerecho = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(hombroDerecho.Position, ColorImageFormat.RgbResolution640x480Fps30);
                    puntoHombroIzquierdo = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(hombroIzquierdo.Position, ColorImageFormat.RgbResolution640x480Fps30);
                    puntoCabeza = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(cabeza.Position, ColorImageFormat.RgbResolution640x480Fps30);
                    puntoCodoDerecho = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(codoDerecho.Position, ColorImageFormat.RgbResolution640x480Fps30);
                    puntoCodoIzquierdo = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(codoIzquierdo.Position, ColorImageFormat.RgbResolution640x480Fps30);
                    puntoRodillaDerecho = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(rodillaDerecho.Position, ColorImageFormat.RgbResolution640x480Fps30);
                    puntoRodillaIzquierdo = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(rodillaIzquierdo.Position, ColorImageFormat.RgbResolution640x480Fps30);
                    puntoPieDerecho = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(pieDerecho.Position, ColorImageFormat.RgbResolution640x480Fps30);
                    puntoPieIzquierdo = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(pieIzquierdo.Position, ColorImageFormat.RgbResolution640x480Fps30);
                    puntoTorso = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(Torso.Position, ColorImageFormat.RawBayerResolution640x480Fps30);
                    puntoHombroCentro = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(hombroCentro.Position, ColorImageFormat.RgbResolution640x480Fps30);

                }
            }
            /// < summary >
            /// En esta seccion lo que hace es redefinir el size del cursor posicionado en pantalla
            /// con la mano, ya que si no, su movimiento seria muy limitado
            /// </ summary >
            if (pantallaJuego == false)
            {
                if (puntoManoDerecha.X < 200) { puntoManoDerecha.X = 200; }
                else
                {
                    if (puntoManoDerecha.X > 500) { puntoManoDerecha.X = 500; }
                }
                puntoManoDerecha.X = (5 * (puntoManoDerecha.X)) - 1000;
                if (puntoManoDerecha.Y < 30) { puntoManoDerecha.Y = 30; }
                else
                {
                    if (puntoManoDerecha.Y > 380) { puntoManoDerecha.Y = 380; }
                }
                puntoManoDerecha.Y = ((15 * puntoManoDerecha.Y) - 450) / 7;
            }
            
        }
        /// <summary>
        /// Este metodo nos dibuja a la persona en el cuadro de abajo del juego
        /// recibe como parámetro los datos necesarios para realizar la impresión, como spriteBatch y el tile
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="genericTile"></param>
        /// <param name="snake"></param>
        public void drawSkeletonTracking(SpriteBatch spriteBatch, Texture2D genericTile, Body snake)
        {
            /// <summary>
            /// Esta seccion solamente dibuja el mapa donde aparece la persona y determina el color 
            /// que debe tomar dependiendo de la direccion que tenga el snake
            /// </summary>
            #region MapaPersona
            if (snake.direccion == 0)
            {
                spriteBatch.Draw(genericTile, new Rectangle(1100, 300, 250, 4), Color.Blue);  // mano <300 arriba
                spriteBatch.Draw(genericTile, new Rectangle(1100, 300, 4, 275), Color.Green); // mano < 1100 izquierda
                spriteBatch.Draw(genericTile, new Rectangle(1100, 575, 250, 4), Color.Red); // mano >575 abajo 
                spriteBatch.Draw(genericTile, new Rectangle(1350, 300, 4, 275), Color.Green); //mano > 1350 derecha
            }
            if (snake.direccion == 1)
            {
                spriteBatch.Draw(genericTile, new Rectangle(1100, 300, 250, 4), Color.Red);  // mano <300 arriba
                spriteBatch.Draw(genericTile, new Rectangle(1100, 300, 4, 275), Color.Green); // mano < 1100 izquierda
                spriteBatch.Draw(genericTile, new Rectangle(1100, 575, 250, 4), Color.Blue); // mano >575 abajo 
                spriteBatch.Draw(genericTile, new Rectangle(1350, 300, 4, 275), Color.Green); //mano > 1350 derecha
            }
            if (snake.direccion == 2)
            {
                spriteBatch.Draw(genericTile, new Rectangle(1100, 300, 250, 4), Color.Green);  // mano <300 arriba
                spriteBatch.Draw(genericTile, new Rectangle(1100, 300, 4, 275), Color.Blue); // mano < 1100 izquierda
                spriteBatch.Draw(genericTile, new Rectangle(1100, 575, 250, 4), Color.Green); // mano >575 abajo 
                spriteBatch.Draw(genericTile, new Rectangle(1350, 300, 4, 275), Color.Red); //mano > 1350 derecha
            }
            if (snake.direccion == 3)
            {
                spriteBatch.Draw(genericTile, new Rectangle(1100, 300, 250, 4), Color.Green);  // mano <300 arriba
                spriteBatch.Draw(genericTile, new Rectangle(1100, 300, 4, 275), Color.Red); // mano < 1100 izquierda
                spriteBatch.Draw(genericTile, new Rectangle(1100, 575, 250, 4), Color.Green); // mano >575 abajo 
                spriteBatch.Draw(genericTile, new Rectangle(1350, 300, 4, 275), Color.Blue); //mano > 1350 derecha
            }

            #endregion
            /// <summary>
            /// Aqui dibujamos el cuerpo de la persona desde sus articulaciones hasta las lineas que 
            /// las unen, o sea, las lineas negritas que aparecen 
            /// </summary>
            #region Skeleton

            if (((puntoCabeza.X + ajuste) >= 1000 && (puntoCabeza.Y + ajusteV) <= 700))
            {
                spriteBatch.Draw(genericTile, new Rectangle(puntoCabeza.X + ajuste,puntoCabeza.Y + ajusteV, 20, 20), Color.Green);

                if (puntoManoDerecha.X < 1450)
                {
                    spriteBatch.Draw(genericTile, new Rectangle(puntoManoDerecha.X + ajuste, puntoManoDerecha.Y + ajusteV, 20, 20), Color.Green);
                    spriteBatch.Draw(genericTile, new Rectangle(puntoHombroDerecho.X + ajuste, puntoHombroDerecho.Y + ajusteV, 20, 20), Color.Green);
                    spriteBatch.Draw(genericTile, new Rectangle(puntoHombroCentro.X + ajuste, puntoHombroCentro.Y + ajusteV, 20, 20), Color.Green);
                    spriteBatch.Draw(genericTile, new Rectangle(puntoCodoDerecho.X + ajuste, puntoCodoDerecho.Y + ajusteV, 20, 20), Color.Green);
                    Primitives2D.DrawLine(spriteBatch, puntoHombroCentro.X + ajuste + 10, puntoHombroCentro.Y + ajusteV + 10, puntoHombroDerecho.X + 10 + ajuste, puntoHombroDerecho.Y + ajusteV, Color.Black, 5f);
                    Primitives2D.DrawLine(spriteBatch, puntoHombroDerecho.X + ajuste + 10, puntoHombroDerecho.Y + ajusteV + 10, puntoCodoDerecho.X + 10 + ajuste, puntoCodoDerecho.Y + ajusteV + 10, Color.Black, 5f);
                    Primitives2D.DrawLine(spriteBatch, puntoCodoDerecho.X + ajuste + 10, puntoCodoDerecho.Y + ajusteV + 10, puntoManoDerecha.X + 10 + ajuste, puntoManoDerecha.Y + ajusteV + 10, Color.Black, 5f);
                }
                else
                { }
                if (puntoManoIzquierda.X < 1000)
                {
                    spriteBatch.Draw(genericTile, new Rectangle(puntoManoIzquierda.X + ajuste, puntoManoIzquierda.Y + ajusteV, 20, 20), Color.Green);
                    spriteBatch.Draw(genericTile, new Rectangle(puntoHombroIzquierdo.X + ajuste, puntoHombroIzquierdo.Y + ajusteV, 20, 20), Color.Green);
                    spriteBatch.Draw(genericTile, new Rectangle(puntoHombroCentro.X + ajuste, puntoHombroCentro.Y + ajusteV, 20, 20), Color.Green);
                    spriteBatch.Draw(genericTile, new Rectangle(puntoCodoIzquierdo.X + ajuste, puntoCodoIzquierdo.Y + ajusteV, 20, 20), Color.Green);
                    Primitives2D.DrawLine(spriteBatch, puntoHombroCentro.X + ajuste + 10, puntoHombroCentro.Y + ajusteV + 10, puntoHombroIzquierdo.X + 10 + ajuste, puntoHombroIzquierdo.Y + ajusteV, Color.Black, 5f);
                    Primitives2D.DrawLine(spriteBatch, puntoHombroIzquierdo.X + ajuste + 10, puntoHombroIzquierdo.Y + ajusteV + 10, puntoCodoIzquierdo.X + 10 + ajuste, puntoCodoIzquierdo.Y + ajusteV + 10, Color.Black, 5f);
                    Primitives2D.DrawLine(spriteBatch, puntoCodoIzquierdo.X + ajuste + 10, puntoCodoIzquierdo.Y + ajusteV + 10, puntoManoIzquierda.X + 10 + ajuste, puntoManoIzquierda.Y + ajusteV + 10, Color.Black, 5f);
                }
                else
                { }
                spriteBatch.Draw(genericTile, new Rectangle(puntoPieDerecho.X + ajuste, puntoPieDerecho.Y + ajusteV, 20, 20), Color.Green);
                spriteBatch.Draw(genericTile, new Rectangle(puntoPieIzquierdo.X + ajuste, puntoPieIzquierdo.Y + ajusteV, 20, 20), Color.Green);
                spriteBatch.Draw(genericTile, new Rectangle(puntoRodillaDerecho.X + ajuste, puntoRodillaDerecho.Y + ajusteV, 20, 20), Color.Green);
                spriteBatch.Draw(genericTile, new Rectangle(puntoRodillaIzquierdo.X + ajuste, puntoRodillaIzquierdo.Y + ajusteV, 20, 20), Color.Green);
                spriteBatch.Draw(genericTile, new Rectangle(puntoTorso.X + ajuste, puntoTorso.Y + ajusteV, 20, 20), Color.Green);

                Primitives2D.DrawLine(spriteBatch, puntoCabeza.X + ajuste + 10, puntoCabeza.Y + ajusteV + 10, puntoHombroCentro.X + 10 + ajuste, puntoHombroCentro.Y + ajusteV, Color.Black, 5f);
                Primitives2D.DrawLine(spriteBatch, puntoTorso.X + ajuste + 10, puntoTorso.Y + ajusteV - 10, puntoRodillaDerecho.X + 10 + ajuste, puntoRodillaDerecho.Y + ajusteV, Color.Black, 5f);
                Primitives2D.DrawLine(spriteBatch, puntoTorso.X + ajuste + 10, puntoTorso.Y + ajusteV - 10, puntoRodillaIzquierdo.X + 10 + ajuste, puntoRodillaIzquierdo.Y + ajusteV, Color.Black, 5f);
                Primitives2D.DrawLine(spriteBatch, puntoRodillaIzquierdo.X + ajuste + 10, puntoRodillaIzquierdo.Y + ajusteV - 10, puntoPieIzquierdo.X + 10 + ajuste, puntoPieIzquierdo.Y + ajusteV, Color.Black, 5f);
                Primitives2D.DrawLine(spriteBatch, puntoRodillaDerecho.X + ajuste + 10, puntoRodillaDerecho.Y + ajusteV - 10, puntoPieDerecho.X + 10 + ajuste, puntoPieDerecho.Y + ajusteV, Color.Black, 5f);
                Primitives2D.DrawLine(spriteBatch, puntoHombroCentro.X + ajuste + 10, puntoHombroCentro.Y + ajusteV - 10, puntoTorso.X + 10 + ajuste, puntoTorso.Y + ajusteV, Color.Black, 5f);

            }
            #endregion
        }

        /// <summary>
        ///  Definimos las coordenadas de los rectangulos del cuerpo del skeleton para dibujarlo en la seccion del juego 
        /// </summary>
        public void rectangulosSkeleton()
        {
            #region RecSkeleton
            rectanguloCursor = new Rectangle((int)puntoManoDerecha.X, (int)puntoManoDerecha.Y, 60, 60);
            rectanguloCabeza = new Rectangle((int)puntoCabeza.X, (int)puntoCabeza.Y, 60, 60);
            rectanguloCodoDerecho = new Rectangle((int)puntoCodoDerecho.X, (int)puntoCodoDerecho.Y, 60, 60);
            rectanguloCodoIzquierdo = new Rectangle((int)puntoCodoIzquierdo.X, (int)puntoCodoIzquierdo.Y, 60, 60);
            rectanguloHombroDerecho = new Rectangle((int)puntoHombroDerecho.X, (int)puntoHombroDerecho.Y, 60, 60);
            rectanguloHombroIzquierdo = new Rectangle((int)puntoHombroIzquierdo.X, (int)puntoHombroIzquierdo.Y, 60, 60);
            rectanguloManoIzquierda = new Rectangle((int)puntoManoIzquierda.X, (int)puntoManoIzquierda.Y, 60, 60);
            rectangulopPieDerecho = new Rectangle((int)puntoPieDerecho.X, (int)puntoPieDerecho.Y, 60, 60);
            rectanguloPieIzquierdo = new Rectangle((int)puntoPieIzquierdo.X, (int)puntoPieIzquierdo.Y, 60, 60);
            rectanguloRodillaDerecho = new Rectangle((int)puntoRodillaDerecho.X, (int)puntoRodillaDerecho.Y, 60, 60);
            rectanguloRodillaIzquierdo = new Rectangle((int)puntoRodillaIzquierdo.X, (int)puntoRodillaIzquierdo.Y, 60, 60);
            rectanguloTorso = new Rectangle((int)puntoTorso.X, (int)puntoTorso.Y, 60, 60);
            rectanguloHombroCentro = new Rectangle((int)puntoHombroCentro.X, (int)puntoHombroCentro.Y, 60, 60);
            #endregion
        }
    }
}
