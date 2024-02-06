using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tl2_tp09_2023_TomasDLV.Models;

namespace tl2_tp09_2023_TomasDLV.Repositorios
{
    public class TableroRepository : ITableroRepository
    {
        private readonly string cadenaConexion;
        public TableroRepository(string CadenaConexion)
        {
            this.cadenaConexion = CadenaConexion;
        }
        public void CreateBoard(Tablero tablero)
        {
            var query = $"INSERT INTO Tablero (id_usuario_propietario, nombre,descripcion) VALUES (@idusu,@nombre,@descripcion)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SQLiteCommand(query, connection);


                command.Parameters.Add(new SQLiteParameter("@idusu", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public void UpdateBoard(int id, Tablero tablero)
        {
            var query = $"UPDATE Tablero SET nombre = @nombre,descripcion = @descripcion WHERE id = {id};";

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));

                connection.Open();
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public Tablero GetByIdBoard(int idTablero)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            var tablero = new Tablero();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Tablero WHERE id = @idtablero";
            command.Parameters.Add(new SQLiteParameter("@idtablero", idTablero));
            connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tablero.Id = Convert.ToInt32(reader["id"]);
                    tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                }
            }
            connection.Close();

            return tablero;
        }
        public List<Tablero> GetAllBoard()
        {
            var queryString = @"SELECT * FROM Tablero;";
            List<Tablero> tableros = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            if (tableros == null)
                throw new Exception("No hay tableros");
            return tableros;
        }
        public List<Tablero> GetAllBoardsByIdUser(int idUser)
        {
            var queryString = @"SELECT * FROM Tablero;";
            List<Tablero> tableros = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero();

                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);

                        if (tablero.IdUsuarioPropietario == idUser)
                        {
                            tablero.Id = Convert.ToInt32(reader["id"]);
                            tablero.Nombre = reader["nombre"].ToString();
                            tablero.Descripcion = reader["descripcion"].ToString();
                            tableros.Add(tablero);
                        }
                    }
                }
                connection.Close();
            }
            if (tableros == null)
                throw new Exception("El usuario no posee tableros");
            return tableros;
        }
        public List<Tablero> GetAllBoardsHaveTask(int idUser)
        {
            var queryString = @"SELECT DISTINCT tb.id, tb.nombre, tb.descripcion, tb.id_usuario_propietario
                        FROM Tarea t
                        INNER JOIN Tablero tb ON t.id_Tablero = tb.id
                        WHERE t.id_usuario_asignado = @idUser
                        AND t.id_Tablero NOT IN (SELECT id FROM Tablero WHERE id_usuario_propietario = @idUser)";

            List<Tablero> tableros = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@idUser", idUser);
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tableros.Add(tablero);
                    }
                }
            }

        
            return tableros;
        }


        public void RemoveBoard(int id)
        {

            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM Tablero WHERE id = @idtablero";
            command.Parameters.Add(new SQLiteParameter("@idtablero", id));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}