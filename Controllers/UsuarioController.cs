using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tl2_tp09_2023_TomasDLV.Models;
using tl2_tp09_2023_TomasDLV.Repositorios;
using tl2_tp10_2023_TomasDLV.ViewModels;


namespace tl2_tp10_2023_TomasDLV.Controllers
{

    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private UsuarioRepository _manejo;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
            _manejo = new UsuarioRepository();
        }

        public IActionResult Index()
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            var usuarios = new List<Usuario>();
            if (esAdmin())
            {
                usuarios = _manejo.GetAllUser();
            }else
            {
                usuarios.Add(_manejo.GetByIdUser((int)HttpContext.Session.GetInt32("id")));
            }
            
            return View(new ListarUsuariosViewModel(usuarios));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        [HttpGet]

        public IActionResult createUser()
        {

            return View(new CrearUsuarioViewModel());
        }

        [HttpPost]
        public IActionResult createUser(CrearUsuarioViewModel usuario)
        {
            _manejo.CreateUser(new Usuario(usuario));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult editUser(int id)
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            Usuario usuario = _manejo.GetByIdUser(id);
            if (esAdmin() || (int)HttpContext.Session.GetInt32("id") == id)
            {
                usuario = _manejo.GetByIdUser(id);
                return View(new ModificarUsuarioViewModel(usuario));
            }else
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            
        }

        [HttpPost]
        public IActionResult editUser(ModificarUsuarioViewModel usuario)
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            _manejo.UpdateUser(usuario.Id, new Usuario(usuario));

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult removeUser(int id)
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            if (esAdmin() || (int)HttpContext.Session.GetInt32("id") == id)
            {
                _manejo.RemoveUser(id);
                if (id == (int)HttpContext.Session.GetInt32("id"))
                {
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                return RedirectToAction("Index");
            }else
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
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