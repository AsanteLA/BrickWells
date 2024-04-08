using System;
using System.Collections.Generic;

namespace BrickWells.Models;

public partial class Product
{
    public double? ProductId { get; set; }

    public string? Name { get; set; }

    public double? Year { get; set; }

    public double? NumParts { get; set; }

    public double? Price { get; set; }

    public string? ImgLink { get; set; }

    public string? PrimaryColor { get; set; }

    public string? SecondaryColor { get; set; }

    public string? Description { get; set; }

    public string? Category { get; set; }
}
