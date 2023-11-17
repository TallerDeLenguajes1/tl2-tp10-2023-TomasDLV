using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tl2_tp09_2023_TomasDLV.Models;

namespace tl2_tp09_2023_TomasDLV.Repositorios
{
    public interface ITareaRepository
    {

        public void CreateTask(Tarea tarea);

        public void UpdateTask(int id, Tarea tarea);
        public List<Tarea> GetAllTask();
        public List<Tarea> GetAllTaskByIdBoard(int idBoard);
        public List<Tarea> GetAllTaskByIdUser(int idUser);
        public Tarea GetTaskById(int idTarea);
        public void RemoveTask(int id);
        public void AssignTask(int idUsuario, int idTarea);
    }
}