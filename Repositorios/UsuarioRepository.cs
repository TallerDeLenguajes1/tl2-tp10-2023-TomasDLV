using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tl2_proyecto_TomasDLV.Models;
using Microsoft.AspNetCore.Identity;
using tl2_proyecto_TomasDLV.ViewModels;

namespace tl2_proyecto_TomasDLV.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string cadenaConexion;
        public UsuarioRepository(string CadenaConexion)
        {
            cadenaConexion = CadenaConexion;
        }

        public void CreateUser(Usuario usuario)
        {
            var query = $"INSERT INTO Usuario (nombre_de_usuario,contrasenia,rol) VALUES (@name,@contrasenia,@rol)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {


                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@name", usuario.Nombre_de_usuario));
                command.Parameters.Add(new SQLiteParameter("@contrasenia", usuario.Contrasenia));
                command.Parameters.Add(new SQLiteParameter("@rol", usuario.Rol));
                connection.Open();
                var filas = command.ExecuteNonQuery();

                connection.Close();
                if(filas == 0) throw new Exception("Hubo un problema al crear el usuario");
            }
        }

        public void UpdateUser(int id, Usuario usuario)
        {
            var query = "UPDATE Usuario SET nombre_de_usuario = @nombre,contrasenia = @contrasenia,rol = @rol WHERE id = @id";

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@nombre", usuario.Nombre_de_usuario));
                command.Parameters.Add(new SQLiteParameter("@id", id));
                command.Parameters.Add(new SQLiteParameter("@contrasenia", usuario.Contrasenia));
                command.Parameters.Add(new SQLiteParameter("@rol", usuario.Rol));

                connection.Open();
                var filas = command.ExecuteNonQuery();
                connection.Close();
                if(filas == 0) throw new Exception("Hubo un problema al modificar el usuario");
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

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.Nombre_de_usuario = reader["nombre_de_usuario"].ToString();
                        usuario.Contrasenia = reader["contrasenia"].ToString();
                        usuario.Rol = (NivelAcceso)Convert.ToInt32(reader["rol"]);
                        usuarios.Add(usuario);
                    }
                }
                connection.Close();
            }
            if (usuarios == null)
                throw new Exception("No existen usuarios.");
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
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.Nombre_de_usuario = reader["nombre_de_usuario"].ToString();
                    usuario.Contrasenia = reader["contrasenia"].ToString();
                    usuario.Rol = (NivelAcceso)Convert.ToInt32(reader["rol"]);
                }
            }
            connection.Close();
            if (usuario == null)
                throw new Exception("Usuario no encontrado.");
            return usuario;
        }

        public void RemoveUser(int id)
        {

            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM Usuario WHERE id = @idusuario";
            command.Parameters.Add(new SQLiteParameter("@idusuario", id));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        internal void CreateUser(CrearUsuarioViewModel u)
        {
            throw new NotImplementedException();
        }
    }
}