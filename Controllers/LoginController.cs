using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tl2_tp09_2023_TomasDLV.Models;
using tl2_tp09_2023_TomasDLV.Repositorios;
using tl2_tp10_2023_TomasDLV.ViewModels;

namespace tl2_tp10_2023_TomasDLV.Controllers
{

    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private IUsuarioRepository _usuario;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
            _usuario = new UsuarioRepository();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel usuarioLogged)
        {
            var usuario = _usuario.GetAllUser().FirstOrDefault(u => u.Nombre_de_usuario == usuarioLogged.Nombre && u.Contrasenia == usuarioLogged.Contrasenia);
            if (usuario == null)
            {
                return RedirectToAction("Index");
            }
            LoguearUsuario(usuario);
            return RedirectToRoute(new{controller = "Home", action = "Index"});
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public void LoguearUsuario(Usuario usuario){
            HttpContext.Session.SetInt32("id", usuario.Id);
            HttpContext.Session.SetString("usuario", usuario.Nombre_de_usuario);
            HttpContext.Session.SetInt32("rol", (int)usuario.Rol);
        }
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}