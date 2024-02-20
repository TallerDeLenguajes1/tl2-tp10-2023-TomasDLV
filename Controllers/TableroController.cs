using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tl2_proyecto_TomasDLV.Models;
using tl2_proyecto_TomasDLV.Repositorios;
using tl2_proyecto_TomasDLV.ViewModels;

namespace tl2_proyecto_TomasDLV.Controllers
{
    public class TableroController : Controller
    {
        private readonly ILogger<TableroController> _logger;
        private readonly ITableroRepository _tableroRepository;
        private readonly ITareaRepository _tareaRepository;


        public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository,ITareaRepository tareaRepository)
        {
            _logger = logger;
            _tableroRepository = tableroRepository;
            _tareaRepository = tareaRepository;
        }
        public IActionResult Index()
        {
            try
            {
                if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });

                var OwnBoards = new List<Tablero>();
                var BoardsWithTask = new List<Tablero>();
                var tasks = _tareaRepository.GetAllTask();
                if (esAdmin())
                {
                    OwnBoards = _tableroRepository.GetAllBoard();
                }
                else
                {
                    OwnBoards = _tableroRepository.GetAllBoardsByIdUser((int)HttpContext.Session.GetInt32("id"));
                    BoardsWithTask = _tableroRepository.GetAllBoardsHaveTask((int)HttpContext.Session.GetInt32("id"));
                }


                return View(new ListarTablerosViewModel(OwnBoards,BoardsWithTask));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        [HttpGet]

        public IActionResult createBoard()
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });

            return View(new CrearTableroViewModel { IdUsuarioPropietario = (int)HttpContext.Session.GetInt32("id") });
        }

        [HttpPost]
        public IActionResult createBoard(CrearTableroViewModel board)
        {
            try
            {
                if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
                if (!ModelState.IsValid) return RedirectToAction("Index");
                _tableroRepository.CreateBoard(new Tablero(board));
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public IActionResult editBoard(int idBoard)
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            var board = _tableroRepository.GetByIdBoard(idBoard);
            return View(new ModificarTableroViewModel(board));

        }

        [HttpPost]
        public IActionResult editBoard(ModificarTableroViewModel tablero)
        {
            try
            {
                if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
                if (!ModelState.IsValid) return RedirectToAction("Index");
                //Checkeo que el tablero sea de quien inicio la sesion o que sea un admin
                if (esAdmin() || (tablero.IdUsuarioPropietario == (int)HttpContext.Session.GetInt32("id")))
                {
                    _tableroRepository.UpdateBoard(tablero.Id, new Tablero(tablero));

                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public IActionResult removeBoard(int id)
        {
            try
            {
                if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });

                if (esAdmin() || (_tableroRepository.GetByIdBoard(id).IdUsuarioPropietario == (int)HttpContext.Session.GetInt32("id")))
                {
                    _tableroRepository.RemoveBoard(id);

                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }

        }

        private bool logueado()
        {
            return HttpContext.Session.Keys.Any();
        }

        private bool esAdmin()
        {
            return HttpContext.Session.Keys.Any() && ((int)HttpContext.Session.GetInt32("rol") == 1);
        }
    }
}