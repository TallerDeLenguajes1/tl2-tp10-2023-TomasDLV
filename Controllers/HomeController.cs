using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_TomasDLV.Models;

namespace tl2_tp10_2023_TomasDLV.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    private bool logueado(){
        return HttpContext.Session.Keys.Any();
    }
    private bool esAdmin()
        {
            return HttpContext.Session.Keys.Any() && HttpContext.Session.GetInt32("rol") == 1;
        }
}
