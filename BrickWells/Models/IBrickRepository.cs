

namespace BrickWells.Models;

public interface IBrickRepository
{
    public IQueryable<Customer> Customers { get; }
    public IQueryable<Order> Orders { get; }
    public IQueryable<Product> Products { get; }
    public IQueryable<LineItem> OrderDetails { get; }
    public IQueryable<ItemBasedRec> ItemBasedRecs { get; }
    
}