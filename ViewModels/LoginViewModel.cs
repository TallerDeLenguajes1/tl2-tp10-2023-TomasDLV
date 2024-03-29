using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tl2_proyecto_TomasDLV.Models;
//Librerias para validacion IMPORTANTES
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_proyecto_TomasDLV.ViewModels
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre de Usuario")] 
        public string Nombre {get;set;}        
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [PasswordPropertyText]
        [Display(Name = "Contraseña")]
        public string Contrasenia {get;set;}
    }
}