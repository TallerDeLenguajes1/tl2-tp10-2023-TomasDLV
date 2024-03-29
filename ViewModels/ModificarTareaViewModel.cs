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

    public class ModificarTareaViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "ID")]
        public int Id { get; set; }
        public int IdBoardsOwner { get; set; }
        public int IdTablero { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(60)]
        [Display(Name = "Nombre de la tarea")]
        public string Nombre { get; set; }

        [StringLength(150)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [StringLength(7)]
        [Display(Name = "Color")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Estado")]
        public EstadoTarea Estado { get; set; }

        [Display(Name = "ID Usuario asignado")]
        public int IdUsuarioAsignado { get; set; }

        public List<Usuario> Usuarios { get; set; }

        public ModificarTareaViewModel(Tarea tarea, List<Usuario> usuarios,int idBoardsOwner)
        {
            this.Id = tarea.Id;
            this.IdTablero = tarea.IdTablero;
            this.Nombre = tarea.Nombre;
            this.Descripcion = tarea.Descripcion;
            this.Color = tarea.Color;
            this.Estado = tarea.Estado;
            this.IdUsuarioAsignado = tarea.Id_usuario_asignado;
            this.Usuarios = usuarios;
            this.IdBoardsOwner = idBoardsOwner;
        }
        public ModificarTareaViewModel() { }
    }

}