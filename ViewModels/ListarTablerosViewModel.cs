using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tl2_proyecto_TomasDLV.Models;

namespace tl2_proyecto_TomasDLV.ViewModels
{
    public class ListarTablerosViewModel
    {
        public List<Tablero> TablerosPropios  {get;set;}
        public List<Tablero> TablerosAjenos {get;set;}
        public ListarTablerosViewModel(List<Tablero> tablerosPropios,List<Tablero> tableroAjenos){

            this.TablerosPropios = tablerosPropios;
            this.TablerosAjenos = tableroAjenos;
        }
    }
}