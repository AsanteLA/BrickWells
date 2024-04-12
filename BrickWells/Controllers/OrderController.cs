using BrickWells.Models;
using BrickWells.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrickWells.Controllers;

[Authorize]
public class OrderController : Controller
{
    private IOrderRepository repository;
    private Cart cart;
    
    public OrderController(IOrderRepository repo, Cart cartService)
    {
        repository = repo;
        cart = cartService;
    }
    public ViewResult Checkout() => View(new Order());
    
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
            repository.AddCustomer(response);
            TempData["CustomerId"] = response.CustomerId; // Store CustomerId in TempData
            return RedirectToAction("Checkout");
        }
        else
        {
            return View(response);
        }
    }
    
    [HttpPost]
    public IActionResult Checkout(Order order)
    {
        if (cart.Lines.Count() == 0)
        {
            ModelState.AddModelError("", "Sorry, your cart is empty!");
        }
        if (ModelState.IsValid)
        {
            if (TempData["CustomerId"] != null && int.TryParse(TempData["CustomerId"].ToString(), out int customerId))
            {
                order.CustomerId = customerId;
            }
            // else
            // {
            //     // Handle case where CustomerId is not found in TempData
            // }
            // order.Amount = cart.Lines.Sum(x => x.Product.Price * x.Quantity);
            // order.CustomerId = response.CustomerId;
            repository.SaveOrder(order);
            cart.Clear();
            return RedirectToPage("/Completed", new { orderId = order.TransactionId });
        }
        
        {
            return View(order);
        }
        
        
    }


}