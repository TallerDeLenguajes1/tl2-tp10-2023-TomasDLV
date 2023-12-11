using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tl2_tp09_2023_TomasDLV.Models;

namespace tl2_tp10_2023_TomasDLV.ViewModels
{
    public class ListarTablerosViewModel
    {
        public List<Tablero> tableros;
        public ListarTablerosViewModel(List<Tablero> tableros){
            this.tableros = tableros;
        }
    }
}