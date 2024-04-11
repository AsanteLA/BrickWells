namespace BrickWells.Models.ViewModels;

public class OrderListViewModel
{
    public IQueryable<Order> Orders { get; set; }
    
    public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
}