using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BrickWells.Models;

namespace BrickWells.Controllers;

public class HomeController : Controller
{
    private IBrickRepository _repo;
    
    public HomeController(IBrickRepository temp)
    {
        _repo = temp;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Products()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }
}