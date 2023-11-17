using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tl2_tp09_2023_TomasDLV.Models;

namespace tl2_tp09_2023_TomasDLV.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string cadenaConexion = "Data Source=DB/kanban.db;Cache=Shared";

        

        

        public void CreateUser(Usuario usuario)
        {
            var query = $"INSERT INTO Usuario (nombre_de_usuario) VALUES (@name)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                
                var command = new SQLiteCommand(query, connection);

                //command.Parameters.Add(new SQLiteParameter("@uid", usuario.Id));
                command.Parameters.Add(new SQLiteParameter("@name", usuario.Nombre_de_usuario));
                connection.Open();
                command.ExecuteNonQuery();

                connection.Close();   
            }
        }
        
        public void UpdateUser (int id,Usuario usuario)
        {
            var query = $"UPDATE Usuario SET nombre_de_usuario = '@nombre' WHERE id = '{id}';";

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@nombre", usuario.Nombre_de_usuario));

                connection.Open();
                command.ExecuteNonQuery();

                connection.Close();   
            }
        }
        public List<Usuario> GetAllUser()
        {
            var queryString = @"SELECT * FROM Usuario;";
            List<Usuario> usuarios = new List<Usuario>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();
            
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.Nombre_de_usuario = reader["nombre_de_usuario"].ToString();
                        usuarios.Add(usuario);
                    }
                }
                connection.Close();
            }
            return usuarios;
        }
        public Usuario GetByIdUser(int idUsuario)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            var usuario = new Usuario();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Usuario WHERE id = @idusuario";
            command.Parameters.Add(new SQLiteParameter("@idusuario", idUsuario));
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.Nombre_de_usuario = reader["nombre_de_usuario"].ToString();
                }
            }
            connection.Close();

            return (usuario);
        }

        public void RemoveUser(int id)
        {
            
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE * FROM Usuario WHERE id = @idusuario";
            command.Parameters.Add(new SQLiteParameter("@idusuario", id));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}