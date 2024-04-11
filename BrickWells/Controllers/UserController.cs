using Microsoft.AspNetCore.Mvc;
using BrickWells.Models;
using Microsoft.AspNetCore.Authorization;

namespace BrickWells.Controllers;
[Authorize]
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

    public IActionResult Confirmation(int id)
    {
        var recentOrder = _repo.Orders
            .Where(x => x.CustomerId == id)
            .OrderBy(x => x.Date)
            .Take(1)
            .SingleOrDefault();

        return View(recentOrder);

    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult About()
    {
        return View();
    }
    public IActionResult Products()
    {
        return View();
    }
}