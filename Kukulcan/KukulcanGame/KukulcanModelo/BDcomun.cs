using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace KukulcanGame
{
    /// <summary>
    /// Clase que realiza la conexion con la base de datos y su gestion 
    /// </summary>
    public class BDcomun
    {

        /// <summary>
        /// Este metodo retorna una lista de datos que se toman de la base de datos
        /// tomando en cuenta que es el nombre y el puntaje del jugador
        /// </summary>
        /// <returns>lstPuntajes</returns>
        public static List<String> cargarPuntajes()
        {

            List<String> lstPuntajes = new List<string>();

            // Creamos la apertura de la base de datos, mandando el localHost por defecto en phpMyAdmin con MySql en XAMPP,
            // pasamos el nombre de la BD y el User id, que por defecto es root

            using (MySqlConnection conexion = new MySqlConnection("server=127.0.0.1; database=bdkukulcan; Uid=root; pwd=;"))
            {
                conexion.Open();

                // Se realiza una consulta a la base de datos en la tabla consulta y tomamos el nombre y el puntaje de `puntajes` ordenada de 
                // manera ascendente delimitado a los primeros 5 

                MySqlCommand comando = new MySqlCommand(String.Format("SELECT nombre, puntaje FROM `puntajes` ORDER BY puntaje DESC LIMIT 5"), conexion);
                MySqlDataReader reader = comando.ExecuteReader();
                int i = 1;
                while (reader.Read())
                {
                    String row = i + ".-               " + reader.GetString(0) + "                  " + reader.GetInt32(1);
                    i++;
                    lstPuntajes.Add(row);
                }
                conexion.Close();
            }
            return lstPuntajes;
        }

        /// <summary>
        /// Este metodo nos guarda los datos del jugador recibiendo el nombre y el puntaje del jugador
        /// recibe nombre y puntaje como parametro y genera el insert en la base de datos
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="puntaje"></param>
        public static void guardarPuntaje(String nombre, int puntaje)
        {

            using (MySqlConnection conexion = new MySqlConnection("server=127.0.0.1; database=bdkukulcan; Uid=root; pwd=;"))
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand(String.Format("INSERT INTO `puntajes`(`nombre`, `puntaje`) VALUES('{0}',{1})",nombre,puntaje), conexion);
                comando.ExecuteNonQuery();
                conexion.Close();
            }
        }
    }
}
