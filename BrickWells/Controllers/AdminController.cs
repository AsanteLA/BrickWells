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
    public IActionResult ProductList(int pageNum)
    {
        int pageSize = 5;

        var brickProducts = new ProductListViewModel
        {
            Products = _repo.Products
                .OrderBy(x => x.ProductId)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

            PaginationInfo = new PaginationInfo
            {
                currentPage = pageNum,
                itemsPerPage = pageSize,
                totalItems = _repo.Products.Count()
            }
        };
        return View(brickProducts);
    }
    
    public IActionResult ProductDetails(int Id)
    {
        var productDetails = _repo.Products
            .Single(x => x.ProductId == Id);
        
        return View(productDetails);
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
    public IActionResult CustomerInfo(int pageNum)
    {
        int pageSize = 25;

        var brickCustomers = new CustomerListViewModel()
        {
            Customers = _repo.Customers
                .OrderBy(x => x.CustomerId)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

            PaginationInfo = new PaginationInfo
            {
                currentPage = pageNum,
                itemsPerPage = pageSize,
                totalItems = 500 // _repo.Orders.Count()
            }
        };

        return View(brickCustomers);
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
    
    
    //Order Information and Methods
    public IActionResult OrderReview(int pageNum)
    {
        int pageSize = 25;

        var brickOrders = new OrderListViewModel()
        {
            Orders = _repo.Orders
                .OrderBy(x => x.TransactionId)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

            PaginationInfo = new PaginationInfo
            {
                currentPage = pageNum,
                itemsPerPage = pageSize,
                totalItems = 500 // _repo.Orders.Count()
            }
        };
        
        return View(brickOrders);
    }
    
    public IActionResult OrderDetails(int Id)
    {
        var orderDetails = _repo.Orders
            .Single(x => x.TransactionId == Id);
        
        return View(orderDetails);
    }
    
}