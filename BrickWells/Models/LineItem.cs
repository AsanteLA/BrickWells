using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrickWells.Models;

public partial class LineItem
{
    [Key]
    [ForeignKey("TransactionId")]
    public int TransactionId { get; set; }
    public Order? Order { get; set; }

    [Key]
    [ForeignKey("ProductId")]
    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public int Qty { get; set; }

    public int Rating { get; set; }
}
