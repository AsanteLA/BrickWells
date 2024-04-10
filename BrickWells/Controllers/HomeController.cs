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
        var brick = new BrickListViewModel
        {
            Products = _repo.Products
                .OrderBy(x => x.Rank)
                .Take(5)
        };
        return View(brick);
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
                totalItems = category == null
                    ? _repo.Products.Count()
                    : _repo.Products.Where(x => x.Category == category).Count()
            },

            currentCategory = category
        };

        return View(brick);
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult ProductDetails()
    {
        // add the id 
        
        // var productToGet = _repo.Products
        //     .Single(x => x.ProductId == id);
        //
        // // Fetch the recommendations for the specified product ID
        // var itemBasedRecs = _repo.ItemBasedRecs
        //     .Single(x => x.ProductId == id);
        //
        // // Retrieve the IDs of the recommended products
        // var recommendedProductIds = new List<int?>()
        // {
        //     itemBasedRecs.FirstRecId,
        //     itemBasedRecs.SecondRecId,
        //     itemBasedRecs.ThirdRecId,
        //     itemBasedRecs.FourthRecId,
        //     itemBasedRecs.FifthRecId
        // };
        //
        // // Fetch additional details for the recommended products using the IDs
        // var recommendedProducts = _repo.Products
        //     .Where(x => recommendedProductIds.Contains(x.ProductId))
        //     .ToList();
        //
        // ViewBag.ProductToGet = productToGet;
        // ViewBag.RecommendedProducts = recommendedProducts;

        return View();
    }
}