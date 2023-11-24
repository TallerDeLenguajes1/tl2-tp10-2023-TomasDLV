using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tl2_tp10_2023_TomasDLV.Models;

namespace tl2_tp10_2023_TomasDLV.Controllers
{
    [Route("[controller]")]
    public class TareaController : Controller
    {
        private readonly ILogger<TareaController> _logger;

        public TareaController(ILogger<TareaController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}