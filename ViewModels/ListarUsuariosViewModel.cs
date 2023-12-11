using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tl2_tp09_2023_TomasDLV.Models;


namespace tl2_tp10_2023_TomasDLV.ViewModels
{
    public class ListarUsuariosViewModel
    {
        public List<Usuario> Usuarios {get;set;}
        public ListarUsuariosViewModel(List<Usuario> listaUsuarios){
            Usuarios = listaUsuarios;
        }
    }
}