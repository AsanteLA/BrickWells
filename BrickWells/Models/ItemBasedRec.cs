using System.ComponentModel.DataAnnotations.Schema;

namespace BrickWells.Models;

public partial class ItemBasedRec
{
    [ForeignKey("ProductId")]
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public string? Name { get; set; }
    public string? FirstRec { get; set; }
    public string? SecondRec { get; set; }
    public string? ThirdRec { get; set; }
    public string? FourthRec { get; set; }
    public string? FifthRec { get; set; }
    public string? SixthRec { get; set; }
    public string? SeventhRec { get; set; }
    public string? EighthRec { get; set; }
    public string? NinthRec { get; set; }
    public string? TenthRec { get; set; }
}