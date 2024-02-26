using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tl2_proyecto_TomasDLV.Models;

namespace tl2_proyecto_TomasDLV.Repositorios
{
    public interface ITableroRepository
    {

        public void CreateBoard(Tablero tablero);

        public void UpdateBoard(int id, Tablero tablero);
        public List<Tablero> GetAllBoard();
        public List<Tablero> GetAllBoardsByIdUser(int idUser);
        public Tablero GetByIdBoard(int idTablero);
        public List<Tablero> GetAllBoardsHaveTask(int idUser);
        public void RemoveBoard(int id);
        
    }
}