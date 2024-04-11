using Microsoft.AspNetCore.Mvc;
using BrickWells.Models;

namespace BrickWells.Controllers;

public class UserController : Controller
{
    private IUserRepository _repo;
    
    public UserController(IUserRepository temp)
    {
        _repo = temp;
    }
    
    [HttpGet]
    public IActionResult UserRegistrationForm()
    {
        return View("UserRegistrationForm", new Customer());
    }

    [HttpPost]
    public IActionResult UserRegistrationForm(Customer response)
    {
        if(ModelState.IsValid)
        {
            _repo.AddCustomer(response);
            return View("Confirmation", response);
        }
        else
        {
            return View(response);
        }
    }

    public IActionResult Index(int customerID)
    {
        // PSUEDO CODE TO GRAB THE SUGGESTIONS FOR LOGGED IN USERS
        
        // get the customer id of the logged in user 
        // var customerID = 11;
        // var customerID = _repo.Customers
        //     .Where(x => x.CustomerId == asdf);
        
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
        // ViewBag.TransactionId = transactionId;
        // ViewBag.Customer = customerQuery;
        // ViewBag.Orders = recentOrder;
        
        
        return View();
    }

    public IActionResult Confirmation(int customerID)
    {
        var recentOrder = _repo.Orders
            .Where(x => x.CustomerId == customerID)
            .OrderBy(x => x.Date)
            .Take(1)
            .SingleOrDefault();

        return RedirectToAction("Index", new { customerID = customerID });

    }
}