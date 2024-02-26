using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tl2_proyecto_TomasDLV.Models;

//agregar para validacion
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_proyecto_TomasDLV.ViewModels
{
    public class CrearTableroViewModel
    {
        public int IdUsuarioPropietario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre del tablero")]
        public string Nombre { get; set; }

        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        public CrearTableroViewModel(Tablero tablero){
            this.IdUsuarioPropietario = (int)tablero.IdUsuarioPropietario;
            this.Nombre = tablero.Nombre;
            this.Descripcion = tablero.Descripcion;
        }
        public CrearTableroViewModel(){}
    }
}