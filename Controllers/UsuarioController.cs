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
            
            try
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

        public IActionResult createUser()
        {

            return View(new CrearUsuarioViewModel());
        }

        [HttpPost]
        public IActionResult createUser(CrearUsuarioViewModel usuario)
        {
            try
            {
                if(!ModelState.IsValid) return RedirectToAction("Index");
                _usuario.CreateUser(new Usuario(usuario));
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public IActionResult editUser(int id)
        {
            try
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
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }
            
        }

        [HttpPost]
        public IActionResult editUser(ModificarUsuarioViewModel usuario)
        {
            try
            {
                if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
                if(!ModelState.IsValid) return RedirectToAction("Index");
                _usuario.UpdateUser(usuario.Id, new Usuario(usuario));
    
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public IActionResult removeUser(int id)
        {
            try
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
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }
            

        }
        [HttpPost]
public IActionResult Logout()
{
    try
    {
        if (logueado())
        {
            HttpContext.Session.Clear(); // Elimina todas las claves de la sesión

            // Puedes agregar más lógica de limpieza si es necesario
        }

        return RedirectToAction("Index", "Home");
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