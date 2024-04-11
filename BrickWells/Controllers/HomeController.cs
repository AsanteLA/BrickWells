using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BrickWells.Models;
using BrickWells.Models.ViewModels;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

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
        
        // PSUEDO CODE TO GRAB THE SUGGESTIONS FOR LOGGED IN USERS
        
        // get the customer id of the logged in user 
        var customerID = 11;
        // look up customer id in the customer table
        var customerQuery = _repo.Customers
            .Single(x => x.CustomerId == customerID);
        
        // if the user is found then go to the orders table and find their most recent order 
        var recentOrder = _repo.Orders
            .Where(x => x.CustomerId == customerID)
            .OrderBy(x => x.Date)
            .Take(1)
            .SingleOrDefault();
        if (customerQuery == null)
        {
            recentOrder = null;
        }
        
        // if they user has no orders here then just display the default suggestions
        
        // else if there IS an order then go ahead and look up the product ID of that order in the item_based_rec table
        // var transactionId = _repo.OrderDetails
        //     .Where(od => od.TransactionId == recentOrder.TransactionId)
        //     .Select(od => od.ProductId) // Adjust this to match your actual property name
        //     .SingleOrDefault(); // Get the transaction ID or null
        
        var transactionId = recentOrder.TransactionId;
        var productId = _repo.OrderDetails
            .Where(od => od.TransactionId == transactionId)
            .Select(od => od.ProductId)
            .FirstOrDefault();
        
        var itemBasedRecs = _repo.ItemBasedRecs
            .Single(x => x.ProductId == productId);
    
        // // Retrieve the IDs of the recommended products
        var recommendedProductIds = new List<string?>()
        {
            itemBasedRecs.FirstRec,
            itemBasedRecs.SecondRec,
            itemBasedRecs.ThirdRec,
            itemBasedRecs.FourthRec,
            itemBasedRecs.FifthRec
        };
 
        // // Fetch additional details for the recommended products using the IDs
        var recommendedProducts = _repo.Products
            .Where(x => recommendedProductIds.Contains(x.Name))
            .ToList();
        
        // OPTIONAL: if there are multiple products in an order then grab the one with the best rank(msot popular)
        // display the top products for the user
        
        // NOTES: There will be a lot of IF statements involved
        ViewBag.RecommendedProducts = recommendedProducts;
        ViewBag.TransactionId = transactionId;
        ViewBag.Customer = customerQuery;
        ViewBag.Orders = recentOrder;

        
        
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

    public IActionResult ProductDetails(int id)
    {
        var productToGet = _repo.Products
            .Single(x => x.ProductId == id);
       
        // // Fetch the recommendations for the specified product ID
        var itemBasedRecs = _repo.ItemBasedRecs
            .Single(x => x.ProductId == id);
    
        // // Retrieve the IDs of the recommended products
        var recommendedProductIds = new List<string?>()
        {
            itemBasedRecs.FirstRec,
            itemBasedRecs.SecondRec,
            itemBasedRecs.ThirdRec,
            itemBasedRecs.FourthRec,
            itemBasedRecs.FifthRec
        };
 
        // // Fetch additional details for the recommended products using the IDs
        var recommendedProducts = _repo.Products
            .Where(x => recommendedProductIds.Contains(x.Name))
            .ToList();
        
        ViewBag.ProductToGet = productToGet;
        ViewBag.RecommendedProducts = recommendedProducts;

        return View();
    }
}