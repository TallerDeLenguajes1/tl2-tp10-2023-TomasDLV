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
        private string cadenaConexion;
        public TareaRepository(string CadenaConexion){
            cadenaConexion = CadenaConexion;
        }
        public void CreateTask(Tarea tarea)
        {
            var query = $"INSERT INTO Tarea (id_tablero, nombre,estado,descripcion,color,id_usuario_asignado) VALUES (@idtab,@nombre,@estado,@descripcion,@color,@id_usuario_asignado)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {


                var command = new SQLiteCommand(query, connection);

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
            var query = "UPDATE Tarea SET id_tablero = @idtab, nombre = @nombre, estado = @estado, descripcion = @descripcion, color = @color, id_usuario_asignado = @id_usuario_asignado WHERE id = @id";

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
            command.Parameters.Add(new SQLiteParameter("@idtarea", idTarea));
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
                    tarea.Color = reader["color"].ToString();
                    tarea.Id_usuario_asignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                }
            }
            connection.Close();
            if (tarea == null)
                throw new Exception("Tarea no encontrada");

            return tarea;
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

                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.Id_usuario_asignado = Convert.ToInt32(reader["id_usuario_asignado"]);

                        tareas.Add(tarea);

                    }
                }
                connection.Close();
            }
            if (tareas == null)
                throw new Exception("No hay tareas");
            return tareas;
        }
        public List<Tarea> GetAllTaskByIdUser(int idUser)
        {
            var queryString = @"SELECT * FROM Tarea WHERE id_usuario_asignado = @idUser;";
            List<Tarea> tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idUser", idUser));
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
                        tarea.Color = reader["color"].ToString();
                        tarea.IdTablero = Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);

                    }
                }
                connection.Close();
            }
            if (tareas == null)
                throw new Exception("El usuario no posee tareas");
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
                        tarea.Color = reader["color"].ToString();
                        tarea.Id_usuario_asignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            if (tareas == null)
                throw new Exception("El tablero no posee tareas");
            return tareas;
        }
        public void RemoveTask(int id)
        {

            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM Tarea WHERE id = @idtarea";
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
                var filas = command.ExecuteNonQuery();

                connection.Close();
                if(filas == 0) throw new Exception("Hubo un problema al asignar un usuario a la tarea");
            }
        }
    }
}