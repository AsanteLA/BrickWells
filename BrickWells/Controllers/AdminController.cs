using Microsoft.AspNetCore.Mvc;
using BrickWells.Models;
using Microsoft.AspNetCore.Authorization;
using BrickWells.Models.ViewModels;

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

    //product Information and Methods
    public IActionResult ProductList()
    {
        var products = _repo.Products
            .OrderBy(x => x.ProductId)
            .ToList();
        
        return View(products);
    }
    
    [HttpGet]
    public IActionResult AddProduct()
    {
        return View("ProductEntryForm", new Product());
    }

    [HttpPost]
    public IActionResult AddProduct(Product response)
    {
        if(ModelState.IsValid)
        {
            _repo.AddProduct(response);
            return View("Confrmation", response);
        }
        else
        {
            return View(response);
        }
    }

    [HttpGet]
    public IActionResult PEdit(int Id)
    {
        var updatedprodInfo = _repo.Products
            .Single(x => x.ProductId == Id);
        
        return View("ProductEntryForm",updatedprodInfo);
    }
    
    [HttpPost]
    public IActionResult PEdit(Product updatedprodInfo)
    {
        _repo.EditProduct(updatedprodInfo);

        return RedirectToAction("ProductList");
    }
    
    [HttpGet]
    public IActionResult PDelete(int Id)
    {
        var prodtoDelete = _repo.Products
            .Single(x => x.ProductId == Id);
        
        return View(prodtoDelete);
    }
    
    [HttpPost]
    public IActionResult PDelete(Product Delete)
    {
        _repo.DeleteProduct(Delete);

        return RedirectToAction("ProductList");
    }
    
    
    //Customer Information and Methods
    public IActionResult CustomerInfo()
    {
        var customers = _repo.Customers
            .OrderBy(x => x.CustomerId)
            .ToList();
        
        return View(customers);
    }
    

    [HttpGet]
    public IActionResult CEdit(int Id)
    {
        var updatedcustInfo = _repo.Customers
            .Single(x => x.CustomerId == Id);
        
        return View("CustomerEntryForm",updatedcustInfo);
    }
    
    [HttpPost]
    public IActionResult CEdit(Customer updatedcustInfo)
    {
        _repo.EditCustomer(updatedcustInfo);

        return RedirectToAction("CustomerInfo");
    }
    
    [HttpGet]
    public IActionResult CDelete(int Id)
    {
        var prodtoDelete = _repo.Customers
            .Single(x => x.CustomerId == Id);
        
        return View(prodtoDelete);
    }
    
    [HttpPost]
    public IActionResult CDelete(Customer Delete)
    {
        _repo.DeleteCustomer(Delete);

        return RedirectToAction("CustomerInfo");
    }
    
    
    public IActionResult OrderReview()
    {
        var orders = _repo.Orders
            .Where(x => x.Fraud == 1)
            .OrderBy(x => x.TransactionId)
            .ToList();
        
        var pageInfo = new PaginationInfo
        {
            currentPage  = ,
            itemsPerPage = pageSize,
            totalItems = _repo.Orders.Count()
        };

        var viewModel = new OrderListViewModel
        {
            Orders = orders,
            PaginationInfo = pageInfo
        };
        
        return View(viewModel);
        
    }
    
    


    
}