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
        private IUsuarioRepository _usuario;

        public UsuarioController(ILogger<UsuarioController> logger,IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _usuario = usuarioRepository;
        }

        public IActionResult Index()
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            var usuarios = new List<Usuario>();
            if (esAdmin())
            {
                usuarios = _usuario.GetAllUser();
            }else
            {
                usuarios.Add(_usuario.GetByIdUser((int)HttpContext.Session.GetInt32("id")));
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
            if(!ModelState.IsValid) return RedirectToAction("Index");
            _usuario.CreateUser(new Usuario(usuario));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult editUser(int id)
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            if(!(id>0)) return RedirectToAction("Index");

            Usuario usuario = _usuario.GetByIdUser(id);
            if (esAdmin() || (int)HttpContext.Session.GetInt32("id") == id)
            {
                usuario = _usuario.GetByIdUser(id);
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
            if(!ModelState.IsValid) return RedirectToAction("Index");
            _usuario.UpdateUser(usuario.Id, new Usuario(usuario));

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult removeUser(int id)
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            if(!(id>0)) return RedirectToAction("Index");
            if (esAdmin() || (int)HttpContext.Session.GetInt32("id") == id)
            {
                _usuario.RemoveUser(id);
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