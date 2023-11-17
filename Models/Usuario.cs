using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tl2_tp09_2023_TomasDLV.Models
{
    public class Usuario
    {
        private int id;
        private string nombre_de_usuario;

        public int Id { get => id; set => id = value; }
        public string Nombre_de_usuario { get => nombre_de_usuario; set => nombre_de_usuario = value; }
        public Usuario(){
        }
        public Usuario(int Id,string Nombre_de_usuario){
            id=Id;
            nombre_de_usuario=Nombre_de_usuario;
        }
    }
}