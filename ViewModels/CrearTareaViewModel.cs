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

    public class CrearTareaViewModel
    {
        public int IdTablero { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(30)]
        [Display(Name = "Nombre de la tarea")]
        public string Nombre { get; set; }

        [StringLength(50)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [StringLength(20)]
        [Display(Name = "Color")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Estado")]
        public EstadoTarea Estado { get; set; }

        [Display(Name = "ID Usuario asignado")]
        public int IdUsuarioAsignado { get; set; }

        public List<Usuario> Usuarios { get; set; }

        
        public CrearTareaViewModel(int idBoard,List<Usuario> users) {
            IdTablero = idBoard;
            Usuarios = users;
         }
         public CrearTareaViewModel() {
            
         }

    }

}