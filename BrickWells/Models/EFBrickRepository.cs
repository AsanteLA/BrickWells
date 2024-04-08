namespace BrickWells.Models;

public class EFBrickRepository : IBrickRepository
{
    private BrickwellsContext _context;
    
    public EFBrickRepository(BrickwellsContext temp)
    {
        _context = temp;
    }
    
    public IQueryable<Product> Products => _context.Products;
    public IQueryable<Customer> Customers => _context.Customers;
    public IQueryable<Order> Orders => _context.Orders;
    public IQueryable<LineItem> OrderDetails => _context.LineItems;
    
}