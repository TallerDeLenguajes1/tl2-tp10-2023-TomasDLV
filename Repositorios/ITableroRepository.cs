using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tl2_tp09_2023_TomasDLV.Models;

namespace tl2_tp09_2023_TomasDLV.Repositorios
{
    public interface ITableroRepository
    {

        public void CreateBoard(Tablero tablero);

        public void UpdateBoard(int id, Tablero tablero);
        public List<Tablero> GetAllBoard();
        public List<Tablero> GetAllByIdBoard(int id);
        public Tablero GetByIdBoard(int idTablero);
        public void RemoveBoard(int id);
    }
}