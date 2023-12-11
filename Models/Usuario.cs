using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tl2_tp10_2023_TomasDLV.ViewModels;

namespace tl2_tp09_2023_TomasDLV.Models
{
    public enum NivelAcceso
    {
        administrador = 1,
        operador = 2
    }
    public class Usuario
    {
        private int id;
        private string nombre_de_usuario;
        private string contrasenia;
        private NivelAcceso rol;


        public int Id { get => id; set => id = value; }
        public string Nombre_de_usuario { get => nombre_de_usuario; set => nombre_de_usuario = value; }
        public string Contrasenia { get => contrasenia; set => contrasenia = value; }
        public NivelAcceso Rol { get => rol; set => rol = value; }
        public Usuario(){}
        public Usuario(CrearUsuarioViewModel usuario)
        {
            this.Nombre_de_usuario = usuario.Nombre;
            this.Contrasenia = usuario.Contrasenia;
            this.Rol = usuario.Rol;
        }
        public Usuario(ModificarUsuarioViewModel usuario)
        {
            this.Nombre_de_usuario = usuario.Nombre;
            this.Contrasenia = usuario.Contrasenia;
            this.Rol = usuario.Rol;

        }

    }
}