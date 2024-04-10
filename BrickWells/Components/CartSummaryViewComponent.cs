using BrickWells.Models;
using Microsoft.AspNetCore.Mvc;

namespace BrickWells.Components;

public class CartSummaryViewComponent : ViewComponent
{
    private Cart cart;

    public CartSummaryViewComponent(Cart cartService)
    {
        cart = cartService;
    }
    
    public IViewComponentResult Invoke()
    {
        return View(cart);
    }

}