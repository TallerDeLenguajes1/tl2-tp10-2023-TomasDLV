using Microsoft.AspNetCore.Mvc;
using tl2_proyecto_TomasDLV.Models;
using tl2_proyecto_TomasDLV.Repositorios;
using tl2_proyecto_TomasDLV.ViewModels;

namespace tl2_proyecto_TomasDLV.Controllers
{

    public class TareaController : Controller
    {
        private readonly ILogger<TareaController> _logger;
        private ITareaRepository _tareaRepository;
        private ITableroRepository _tableroRepository;
        private IUsuarioRepository _usuarioRepository;

        public TareaController(ILogger<TareaController> logger, ITareaRepository tareaRepository,ITableroRepository tableroRepository,IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _tareaRepository = tareaRepository;
            _tableroRepository = tableroRepository;
            _usuarioRepository = usuarioRepository;
        }

        public IActionResult Index(int idBoard)
        {
            try
            {
                if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
                
                var board = _tableroRepository.GetByIdBoard(idBoard);
                var users = _usuarioRepository.GetAllUser();
                int idUser = (int)HttpContext.Session.GetInt32("id");
                var tasks = _tareaRepository.GetAllTaskByIdBoard(idBoard);
                return View(new ListarTareasViewModel(tasks,users,board,idUser));
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

        public IActionResult createTask(int idBoard)
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            var usuarios = _usuarioRepository.GetAllUser();
            return View(new CrearTareaViewModel(idBoard,usuarios));
        }

        [HttpPost]
        public IActionResult createTask(CrearTareaViewModel task)
        {
            try
            {
                if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
                if (!ModelState.IsValid) return RedirectToAction("Index");
                
                var tablero = _tableroRepository.GetByIdBoard(task.IdTablero);
                if (esAdmin() || tablero.IdUsuarioPropietario == (int)HttpContext.Session.GetInt32("id"))
                {
                    _tareaRepository.CreateTask(new Tarea(task));
                return RedirectToAction("Index", "Tarea", new { idBoard = task.IdTablero });

                
                }else
                {
                    //Agregar error
                    return RedirectToAction("Index", "Tarea", new { idBoard = task.IdTablero });
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public IActionResult editTask(int id)
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            var task = _tareaRepository.GetTaskById(id);
            var users = _usuarioRepository.GetAllUser();
            var idBoardsOwner = _tableroRepository.GetByIdBoard(task.IdTablero).IdUsuarioPropietario;
            return View(new ModificarTareaViewModel(task,users,idBoardsOwner));

            
        }

        [HttpPost]
        public IActionResult editTask(ModificarTareaViewModel tarea)
        {
            try
            {
                if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
                if (!ModelState.IsValid) return RedirectToAction("Index");

                var tablero = _tableroRepository.GetByIdBoard(tarea.IdTablero);

                if (esAdmin() || tarea.IdUsuarioAsignado == (int)HttpContext.Session.GetInt32("id") || tablero.IdUsuarioPropietario == (int)HttpContext.Session.GetInt32("id"))
                {
                    _tareaRepository.UpdateTask(tarea.Id, new Tarea(tarea));

                
                }
                return RedirectToAction("Index", "Tarea", new { idBoard = tarea.IdTablero });

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public IActionResult removeTask(int id)
        {
            try
            {
                if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
                var task = _tareaRepository.GetTaskById(id);
                var IdTab = task.IdTablero;
                var tablero = _tableroRepository.GetByIdBoard(IdTab);
                
                

                if (esAdmin())
                {
                    _tareaRepository.RemoveTask(id);

                    return RedirectToAction("Index", "Tarea", new { idBoard = IdTab });
                }
                else
                {
                    if (tablero.IdUsuarioPropietario == (int)HttpContext.Session.GetInt32("id"))
                    {
                        var tarea = _tareaRepository.GetTaskById(id);
                        tarea.Id_usuario_asignado = -1;
                        _tareaRepository.UpdateTask(id,tarea);
                        return RedirectToAction("Index", "Tarea", new { idBoard = IdTab });
                    }
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
            return HttpContext.Session.Keys.Any() && (int)HttpContext.Session.GetInt32("rol") == 1;
        }
    }
}