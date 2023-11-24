using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tl2_tp09_2023_TomasDLV.Models;
using tl2_tp09_2023_TomasDLV.Repositorios;


namespace tl2_tp10_2023_TomasDLV.Controllers
{
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private UsuarioRepository  _manejo;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
            _manejo = new UsuarioRepository();
        }
        [Route("Index")]
        public IActionResult Index()
        {
            var usuarios = _manejo.GetAllUser();
            return View(usuarios);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        [HttpGet]
        [Route("CrearUsuario")]
        public IActionResult Crearusuario() 
        {
            return View(new Usuario());
        }

        [HttpPost]
        [Route("CrearUsuario")]
        public IActionResult Crearusuario(Usuario u)
        {
            _manejo.CreateUser(u);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("EditarUsuario")]
        public IActionResult EditarUsuario(int id)
        {
            var usuario = _manejo.GetByIdUser(id);
            return View(usuario);
        }

        [HttpPost]
        [Route("EditarUsuario")]
        public IActionResult EditarUsuario(Usuario usuario)
        {   
            var usuarioMod = _manejo.GetByIdUser(usuario.Id);
            usuarioMod.Nombre_de_usuario = usuario.Nombre_de_usuario;

            _manejo.UpdateUser(usuario.Id,usuario);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("EliminarUsuario")]
        public IActionResult EliminarUsuario(int id)
        {
            _manejo.RemoveUser(id);
            return RedirectToAction("Index");

        }
    }
}