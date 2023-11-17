using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tl2_tp09_2023_TomasDLV.Models;

namespace tl2_tp09_2023_TomasDLV.Repositorios
{
    public class TableroRepository: ITableroRepository
    {
        private string cadenaConexion = "Data Source=DB/kanban.db;Cache=Shared";
        public void CreateBoard(Tablero tablero)
        {
            var query = $"INSERT INTO Tablero (id,id_usuario_propietario, nombre,descripcion) VALUES (@id,@idusu,@nombre,@descripcion)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@id", tablero.Id));
                command.Parameters.Add(new SQLiteParameter("@idusu", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
                
                command.ExecuteNonQuery();

                connection.Close();   
            }
        }
        public void UpdateBoard (int id,Tablero tablero)
        {
            var query = $"UPDATE Tablero SET id_usuario_propietario = '@idusu',nombre = '@nombre',descripcion = '@descripcion' WHERE id = '{id}';";

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idusu", tablero.IdUsuarioPropietario));
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
            using(SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tablero.Id = Convert.ToInt32(reader["id"]);
                    tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                }
            }
            connection.Close();

            return (tablero);
        }
        public List<Tablero> GetAllBoard()
        {
            var queryString = @"SELECT * FROM Tablero;";
            List<Tablero> tableros = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();
            
                using(SQLiteDataReader reader = command.ExecuteReader())
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
            return tableros;
        }
        public List<Tablero> GetAllByIdBoard(int id)
        {
            var queryString = @"SELECT * FROM Tablero;";
            List<Tablero> tableros = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();
            
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero();
                        
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        
                        if (tablero.IdUsuarioPropietario == id)
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
            return tableros;
        }
        public void RemoveBoard(int id)
        {
            
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE * FROM Tablero WHERE id = @idtablero";
            command.Parameters.Add(new SQLiteParameter("@idtablero", id));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}