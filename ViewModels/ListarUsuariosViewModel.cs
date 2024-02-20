using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tl2_proyecto_TomasDLV.Models;


namespace tl2_proyecto_TomasDLV.ViewModels
{
    public class ListarUsuariosViewModel
    {
        public List<Usuario> Usuarios {get;set;}
        public ListarUsuariosViewModel(List<Usuario> listaUsuarios){
            Usuarios = listaUsuarios;
        }
    }
}