using Microsoft.AspNetCore.Mvc;
using tl2_tp09_2023_TomasDLV.Models;
using tl2_tp09_2023_TomasDLV.Repositorios;
using tl2_tp10_2023_TomasDLV.ViewModels;

namespace tl2_tp10_2023_TomasDLV.Controllers
{

    public class TareaController : Controller
    {
        private readonly ILogger<TareaController> _logger;
        private ITareaRepository _tareaRepository;
        
        public TareaController(ILogger<TareaController> logger,ITareaRepository tareaRepository)
        {
            _logger = logger;
            _tareaRepository = tareaRepository;
        }

        public IActionResult Index()
        {
            try
            {
                if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
                var tasks = _tareaRepository.GetAllTask();
                return View(new ListarTareasViewModel(tasks));
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

        public IActionResult createTask()
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });

            return View(new CrearTareaViewModel());
        }

        [HttpPost]
        public IActionResult createTask(CrearTareaViewModel task)
        {
            try
            {
                if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
                if(!ModelState.IsValid) return RedirectToAction("Index");
                _tareaRepository.CreateTask(new Tarea(task));
                return RedirectToAction("Index");
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
            return View(new ModificarTareaViewModel(task));
        }

        [HttpPost]
        public IActionResult editTask(ModificarTareaViewModel tarea)
        {
            try
            {
                if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
                if(!ModelState.IsValid) return RedirectToAction("Index");
    
                _tareaRepository.UpdateTask(tarea.Id,new Tarea(tarea));
    
                return RedirectToAction("Index");
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
                _tareaRepository.RemoveTask(id);
                return RedirectToAction("Index");
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