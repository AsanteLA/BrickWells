using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BrickWells.Models;

public partial class Product
{
    [Key]
    [Required]
    public int ProductId { get; set; }
    
    [Required(ErrorMessage = "Please enter a product name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Please enter a production year")]
    public double Year { get; set; }

    [Required(ErrorMessage = "Please enter the number of parts")]
    public double NumParts { get; set; }

    [Required(ErrorMessage = "Please enter the product price")]
    public double Price { get; set; }
    
    [Required(ErrorMessage = "Please a valid product image link")]
    public string ImgLink { get; set; }

    [Required(ErrorMessage = "Please enter the product's primary color")]
    public string PrimaryColor { get; set; }

    [Required(ErrorMessage = "Please enter the product's secondary color")]
    public string SecondaryColor { get; set; }
    
    [Required(ErrorMessage = "Please enter the product's description")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Please enter the product's category")]
    public string Category { get; set; }
    public string? SubCategory { get; set; }
    
    public string Rank { get; set; }
}
