using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tl2_tp09_2023_TomasDLV.Models;

namespace tl2_tp10_2023_TomasDLV.ViewModels
{
    public class ListarTareasViewModel
    {
        public List<Tarea> Tareas {get;set;}
        public Tablero TableroInfo {get;set;}
        public int IdUser{get;set;}
        public ListarTareasViewModel(List<Tarea> listaTareas,Tablero tablero,int idUser){
            Tareas = listaTareas;
            TableroInfo = tablero;
            IdUser = idUser;
        }
    }
}