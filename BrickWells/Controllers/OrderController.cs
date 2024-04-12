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
            int customerId = response.CustomerId;
            return RedirectToAction("Checkout","Order", new {customerId = customerId});
        }
        else
        {
            return View(response);
        }
    }
    
    [HttpPost]
    public IActionResult Checkout(Order order, int customerId)
    {
        if (cart.Lines.Count() == 0)
        {
            ModelState.AddModelError("", "Sorry, your cart is empty!");
        }
        if (ModelState.IsValid)
        {
            // order.Amount = cart.Lines.Sum(x => x.Product.Price * x.Quantity);
            repository.SaveOrder(order);
            
            int transactionId = order.TransactionId;
            
            foreach (var line in cart.Lines)
            {
                LineItem lineItem = new LineItem
                {
                    TransactionId = transactionId,
                    ProductId = line.Product.ProductId, // Retrieve ProductId from CartLine
                    Qty = line.Quantity,
                    Rating = 0 // Assuming default rating is 0
                };

                // Add line item to repository or context
                repository.AddLineItem(lineItem);
            }
            
            cart.Clear();
            return RedirectToPage("/Completed", new { orderId = order.TransactionId });
        }
        
        {
            return View(order);
        }
        
        
    }


}