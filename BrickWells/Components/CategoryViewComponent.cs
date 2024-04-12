using Microsoft.AspNetCore.Mvc;
using BrickWells.Models;

namespace BrickWells.Components;

public class CategoryViewComponent : ViewComponent
{
        private IBrickRepository _repo;
        //Constructor
        public CategoryViewComponent(IBrickRepository temp)
        {
                _repo = temp;
        }
        public IViewComponentResult Invoke ()
        {
                ViewBag.SelectedCategory = RouteData?.Values["Category"];
                
                var Categories = _repo.Products
                        .Select(x => x.Category)
                        .Distinct()
                        .OrderBy(x => x);
                return View(Categories);
        }
}