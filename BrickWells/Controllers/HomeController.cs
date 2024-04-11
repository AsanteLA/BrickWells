using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BrickWells.Models;
using BrickWells.Models.ViewModels;

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

    public IActionResult Products(int pageNum, string? category, string? color)
    {
        int pageSize = 3;

        var brick = new BrickListViewModel
        {
            Products = _repo.Products
                .Where(x => x.Category == category || category == null)
                .OrderBy(x => x.ProductId)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

            PaginationInfo = new PaginationInfo
            {
                currentPage = pageNum,
                itemsPerPage = pageSize,
                totalItems = category == null ? _repo.Products.Count() : _repo.Products.Where(x => x.Category == category).Count()
            },

            currentCategory = category
        };
            
        return View(brick);
    }

    public IActionResult About()
    {
        return View();
    }
}