using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tl2_proyecto_TomasDLV.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_proyecto_TomasDLV.ViewModels
{
    public class ModificarTableroViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "ID")]
        public int Id { get ; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "ID Usuario Propietario")]
        public int IdUsuarioPropietario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(30)]
        [Display(Name = "Nombre del tablero")]
        public string Nombre { get; set; }

        [StringLength(50)]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }
        public List<Usuario> Usuarios {get; set;}
        public ModificarTableroViewModel(){}
        public ModificarTableroViewModel(Tablero tablero,List<Usuario> usuarios){
            this.Id = tablero.Id;
            this.IdUsuarioPropietario = tablero.IdUsuarioPropietario;
            this.Nombre = tablero.Nombre;
            this.Descripcion = tablero.Descripcion;
            this.Usuarios =usuarios;
        }

    }
}