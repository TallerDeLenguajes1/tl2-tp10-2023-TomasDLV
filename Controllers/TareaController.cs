using Microsoft.AspNetCore.Mvc;
using tl2_tp09_2023_TomasDLV.Models;
using tl2_tp09_2023_TomasDLV.Repositorios;

namespace tl2_tp10_2023_TomasDLV.Controllers
{

    public class TareaController : Controller
    {
        private readonly ILogger<TareaController> _logger;
        private TareaRepository _manejo;
        public TareaController(ILogger<TareaController> logger)
        {
            _logger = logger;
            _manejo = new TareaRepository();
        }

        public IActionResult Index()
        {
            var tasks = _manejo.GetAllTask();
            return View(tasks);
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        [HttpGet]
        
        public IActionResult createTask() 
        {
            return View(new Tarea());
        }

        [HttpPost]
        public IActionResult createTask(Tarea task)
        {
            _manejo.CreateTask(task);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult editTask(int id)
        {
            var task = _manejo.GetTaskById(id);
            
            return View(task);
        }

        [HttpPost]
        public IActionResult editTask(Tarea tarea)
        {   

            _manejo.UpdateTask(tarea.Id,tarea);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult removeTask(int id)
        {
            _manejo.RemoveTask(id);
            return RedirectToAction("Index");

        }
    }
}