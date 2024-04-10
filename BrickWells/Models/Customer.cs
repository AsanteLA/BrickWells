using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BrickWells.Models;

public partial class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "Please enter a first name")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Please enter a last name")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Please enter a Birth Date")]
    public string BirthDate { get; set; }

    [Required(ErrorMessage = "Please enter a valid Country of Residence")]
    public string CountryOfResidence { get; set; }

    [Required(ErrorMessage = "Please enter a Gender")]
    public string? Gender { get; set; }

    [Required(ErrorMessage = "Please enter a valid Age")]
    public double Age { get; set; }
}
