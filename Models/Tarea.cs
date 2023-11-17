using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tl2_tp09_2023_TomasDLV.Models
{
    public enum EstadoTarea
    {
        Ideas,
        ToDo,
        Doing,
        Review,
        Done
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
        
        // public Tarea(int Id, int IdTablero, string Nombre, int Estado, string Descripcion, string Color, int Id_usuario_asignado)
        // {
        //     id = Id;
        //     idTablero = IdTablero;
        //     nombre = Nombre;
        //     estado = (EstadoTarea)Estado;
        //     descripcion = Descripcion;
        //     color = Color;
        //     id_usuario_asignado = Id_usuario_asignado;
        // }
        // public void SetEstado(EstadoTarea Estado)
        // {
        //     estado = Estado;
        // }
        // public void SetEstado(int Estado)
        // {
        //     estado = (EstadoTarea)Estado;
        // }
    }
}