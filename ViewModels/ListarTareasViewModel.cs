using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tl2_proyecto_TomasDLV.Models;

namespace tl2_proyecto_TomasDLV.ViewModels
{
    public class ListarTareasViewModel
    {
        public List<Tarea> Tareas {get;set;}
        public List<Usuario> Usuarios {get;set;}
        public Tablero TableroInfo {get;set;}
        public int IdUser{get;set;}
        public ListarTareasViewModel(List<Tarea> listaTareas,List<Usuario> listaUsuarios,Tablero tablero,int idUser){
            Tareas = listaTareas;
            TableroInfo = tablero;
            Usuarios = listaUsuarios;
            IdUser = idUser;
        }
    }
}