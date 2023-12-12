using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tl2_tp09_2023_TomasDLV.Models;
//agregar para validacion
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_TomasDLV.ViewModels
{
    public class ModificarUsuarioViewModel
    {  
        public int Id {get;set;}      
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(30)]
        [Display(Name = "Nombre de usuario")]  
        public string Nombre {get;set;}        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [PasswordPropertyText]
        [StringLength(6, MinimumLength = 0, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [Display(Name = "Contraseña")] 
        public string Contrasenia {get;set;}      
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Rol")]    
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