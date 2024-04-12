using Microsoft.AspNetCore.Mvc;
using BrickWells.Models;

namespace BrickWells.Components;

public class CategoryViewComponent : ViewComponent
{

        
        public string Invoke ()
        {
                return "Hello from the Navigation Menu";
        }
}