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
    
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private UsuarioRepository  _manejo;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
            _manejo = new UsuarioRepository();
        }
       
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
        
        public IActionResult createUser() 
        {
            return View(new Usuario());
        }

        [HttpPost]
        public IActionResult createUser(Usuario u)
        {
            _manejo.CreateUser(u);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult editUser(int id)
        {
            var usuario = _manejo.GetByIdUser(id);
            return View(usuario);
        }

        [HttpPost]
        public IActionResult editUser(Usuario usuario)
        {   
        
            _manejo.UpdateUser(usuario.Id,usuario);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult removeUser(int id)
        {
            _manejo.RemoveUser(id);
            return RedirectToAction("Index");

        }
    }
}