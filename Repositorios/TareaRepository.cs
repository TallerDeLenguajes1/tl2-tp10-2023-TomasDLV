using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tl2_tp09_2023_TomasDLV.Models;

namespace tl2_tp09_2023_TomasDLV.Repositorios
{
    public class TareaRepository : ITareaRepository
    {
        private string cadenaConexion = "Data Source=DB/kanban.db;Cache=Shared";
        public void CreateTask(Tarea tarea)
        {
            var query = $"INSERT INTO Tarea (id,id_tablero, nombre,estado,descripcion,color,id_usuario_asignado) VALUES (@id,@idtab,@nombre,@estado,@descripcion,@color,@id_usuario_asignado)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {


                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@id", tarea.Id));
                command.Parameters.Add(new SQLiteParameter("@idtab", tarea.IdTablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@id_usuario_asignado", tarea.Id_usuario_asignado));

                connection.Open();
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public void UpdateTask(int id, Tarea tarea)
        {
            var query = $"UPDATE Tarea SET (id_tablero, nombre,estado,descripcion,color,id_usuario_asignado) VALUES (@idtab,@nombre,@estado,@descripcion,@color,@id_usuario_asignado) WHERE id = @id";

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@id", id));
                command.Parameters.Add(new SQLiteParameter("@idtab", tarea.IdTablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@id_usuario_asignado", tarea.Id_usuario_asignado));

                connection.Open();
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public Tarea GetTaskById(int idTarea)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            var tarea = new Tarea();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Tarea WHERE id = @idtarea";
            command.Parameters.Add(new SQLiteParameter("@idtablero", idTarea));
            connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tarea.Id = Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Descripcion = reader["color"].ToString();
                    tarea.IdTablero = Convert.ToInt32(reader["id_usuario_asignado"]);
                }
            }
            connection.Close();

            return (tarea);
        }

        public List<Tarea> GetAllTask()
        {
            var queryString = @"SELECT * FROM Tarea;";
            List<Tarea> tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tarea = new Tarea();

                        tarea.Id_usuario_asignado = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Descripcion = reader["color"].ToString();
                        tarea.IdTablero = Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);

                    }
                }
                connection.Close();
            }
            return tareas;
        }
        public List<Tarea> GetAllTaskByIdUser(int idUser)
        {
            var queryString = @"SELECT * FROM Tarea WHERE id_usuario_asignado = @idUser;";
            List<Tarea> tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@id_usuario_asignado", idUser));
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tarea = new Tarea();

                        tarea.Id_usuario_asignado = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Descripcion = reader["color"].ToString();
                        tarea.IdTablero = Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);

                    }
                }
                connection.Close();
            }
            return tareas;
        }
        public List<Tarea> GetAllTaskByIdBoard(int idBoard)
        {
            var queryString = @"SELECT * FROM Tarea WHERE id_Tablero = @idBoard;";
            List<Tarea> tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idBoard", idBoard));
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Descripcion = reader["color"].ToString();
                        tarea.IdTablero = Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            return tareas;
        }
        public void RemoveTask(int id)
        {

            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE * FROM Tarea WHERE id = @idtarea";
            command.Parameters.Add(new SQLiteParameter("@idtarea", id));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void AssignTask(int idUsuario, int idTarea)
        {
            var query = $"UPDATE Tarea SET (id_usuario_asignado) VALUES (@id_usuario_asignado) WHERE id='{idTarea}'";

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@id_usuario_asignado", idUsuario));

                connection.Open();
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}