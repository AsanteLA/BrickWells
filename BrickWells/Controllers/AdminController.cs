using Microsoft.AspNetCore.Mvc;
using BrickWells.Models;
using Microsoft.AspNetCore.Authorization;

namespace BrickWells.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private IAdminRepository _repo;
    
    public AdminController(IAdminRepository temp)
    {
        _repo = temp;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ProductList()
    {
        return View();
    }
    
    public IActionResult OrderReview()
    {
        return View();
    }
    
    public IActionResult Customer()
    {
        return View();
    }
}