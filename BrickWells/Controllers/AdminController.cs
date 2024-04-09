using Microsoft.AspNetCore.Mvc;
using BrickWells.Models;
using Microsoft.AspNetCore.Authorization;

namespace BrickWells.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private IBrickRepository _repo;
    
    public AdminController(IBrickRepository temp)
    {
        _repo = temp;
    }
    
    public IActionResult Index()
    {
        return View();
    }
}