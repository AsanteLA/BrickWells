using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using BrickWells.Models;
using BrickWells.Models.ViewModels;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using SQLitePCL;

namespace BrickWells.Controllers;

public class HomeController : Controller
{
    private IBrickRepository _repo;

    public HomeController(IBrickRepository temp)
    {
        _repo = temp;
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
    
    public IActionResult Index()
{
    // Check if the user is authenticated
    if (User.Identity.IsAuthenticated)
    {
        // Retrieve the user's unique identifier (e.g., email address)
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

        if (userEmail != null)
        {
            // Query the Customers table to find the customerId associated with the logged-in user's email
            var customer = _repo.Customers.FirstOrDefault(c => c.Email == userEmail);

            if (customer != null)
            {
                var customerId = customer.CustomerId;

                // Proceed with retrieving the customer's recent orders and other information
                var recentOrder = _repo.Orders
                    .Where(x => x.CustomerId == customerId)
                    .OrderByDescending(x => x.Date)
                    .FirstOrDefault();

                if (recentOrder != null)
                {
                    // Retrieve the product ID from the recent order
                    var transactionId = recentOrder.TransactionId;
                    var productId = _repo.OrderDetails
                        .Where(od => od.TransactionId == transactionId)
                        .Select(od => od.ProductId)
                        .FirstOrDefault();

                    if (productId != null)
                    {
                        // Retrieve the item-based recommendations based on the product ID
                        var itemBasedRecs = _repo.ItemBasedRecs
                            .SingleOrDefault(x => x.ProductId == productId);

                        if (itemBasedRecs != null)
                        {
                            // Retrieve the IDs of the recommended products
                            var recommendedProductIds = new List<string?>()
                            {
                                itemBasedRecs.FirstRec,
                                itemBasedRecs.SecondRec,
                                itemBasedRecs.ThirdRec,
                                itemBasedRecs.FourthRec,
                                itemBasedRecs.FifthRec
                            };

                            // Fetch additional details for the recommended products using the IDs
                            var recommendedProducts = _repo.Products
                                .Where(x => recommendedProductIds.Contains(x.Name))
                                .ToList();

                            // Set ViewBag variables for the view
                            ViewBag.RecommendedProducts = recommendedProducts;
                            ViewBag.TransactionId = transactionId;
                            ViewBag.Customer = customer;
                            ViewBag.Orders = recentOrder;
                        }
                    }
                }
            }
        }
    }

    // If the user is not authenticated or encountered any errors, proceed with default suggestions
    var brick = new BrickListViewModel
    {
        Products = _repo.Products.OrderBy(x => x.Rank).Take(5)
    };

    return View(brick);
}



}