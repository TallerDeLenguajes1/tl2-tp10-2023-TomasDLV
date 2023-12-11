using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tl2_tp09_2023_TomasDLV.Models;

namespace tl2_tp10_2023_TomasDLV.ViewModels
{
    public class CrearUsuarioViewModel
    {
        public string Nombre {get;set;}        
        public string Contrasenia {get;set;}        
        public NivelAcceso Rol {get;set;}

        public CrearUsuarioViewModel(){}
        public CrearUsuarioViewModel(Usuario usuario){
            this.Nombre = usuario.Nombre_de_usuario;
            this.Contrasenia = usuario.Contrasenia;
            this.Rol = usuario.Rol;
        }
    }
}