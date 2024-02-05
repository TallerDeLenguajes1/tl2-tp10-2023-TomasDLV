using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tl2_tp10_2023_TomasDLV.ViewModels;

namespace tl2_tp09_2023_TomasDLV.Models
{
    public enum EstadoTarea
    {
        Ideas,
        Pendientes,
        EnProceso,
        Revisar,
        Terminado
    }

    public class Tarea
    {
        private int id;
        private int idTablero;
        private string nombre;
        private EstadoTarea estado;
        private string descripcion;
        private string color;
        private int id_usuario_asignado;

        public int Id { get => id; set => id = value; }
        public int IdTablero { get => idTablero; set => idTablero = value; }
        public string Nombre { get => nombre; set => nombre = value; }

        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Color { get => color; set => color = value; }
        public int Id_usuario_asignado { get => id_usuario_asignado; set => id_usuario_asignado = value; }
        public EstadoTarea Estado { get => estado; set => estado = value; }

        
        public Tarea(){}
    public Tarea(CrearTareaViewModel tarea){
        this.IdTablero = tarea.IdTablero;
        this.Nombre = tarea.Nombre;
        this.Descripcion = tarea.Descripcion;
        this.Color = tarea.Color;
        this.Estado = tarea.Estado;
        this.Id_usuario_asignado = tarea.IdUsuarioAsignado;
    }
    public Tarea(ModificarTareaViewModel tarea){
        this.Id = tarea.Id;
        this.IdTablero = tarea.IdTablero;
        this.Nombre = tarea.Nombre;
        this.Descripcion = tarea.Descripcion;
        this.Color = tarea.Color;
        this.Estado = tarea.Estado;
        this.Id_usuario_asignado = tarea.IdUsuarioAsignado;
    }
    }
}