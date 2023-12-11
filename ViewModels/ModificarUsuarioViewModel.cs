using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tl2_tp09_2023_TomasDLV.Models;

namespace tl2_tp10_2023_TomasDLV.ViewModels
{
    public class ModificarUsuarioViewModel
    {  
        public int Id {get;set;}        
        public string Nombre {get;set;}        
        public string Contrasenia {get;set;}        
        public NivelAcceso Rol {get;set;}

        public ModificarUsuarioViewModel(Usuario usuario){
            this.Id = usuario.Id;
            this.Nombre = usuario.Nombre_de_usuario;
            this.Contrasenia = usuario.Contrasenia;
            this.Rol = usuario.Rol;
        }
        public ModificarUsuarioViewModel(){}
    }
}