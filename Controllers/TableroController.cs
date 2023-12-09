using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tl2_tp10_2023_TomasDLV.Models;
using tl2_tp09_2023_TomasDLV.Repositorios;
using tl2_tp09_2023_TomasDLV.Models;

namespace tl2_tp10_2023_TomasDLV.Controllers
{
    public class TableroController : Controller
    {
        private readonly ILogger<TableroController> _logger;
        private TableroRepository  _manejo;


        public TableroController(ILogger<TableroController> logger)
        {
            _logger = logger;
            _manejo = new TableroRepository();
        }
        public IActionResult Index()
        {
            var boards = _manejo.GetAllBoard();
            return View(boards);
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        [HttpGet]
        
        public IActionResult createBoard() 
        {
            return View(new Tablero());
        }

        [HttpPost]
        public IActionResult createBoard(Tablero board)
        {
            _manejo.CreateBoard(board);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult editBoard(int id)
        {
            var board = _manejo.GetByIdBoard(id);
            
            return View(board);
        }

        [HttpPost]
        public IActionResult editBoard(Tablero tablero)
        {   

            _manejo.UpdateBoard(tablero.Id,tablero);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult removeBoard(int id)
        {
            _manejo.RemoveBoard(id);
            return RedirectToAction("Index");

        }
    }
}