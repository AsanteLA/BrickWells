using BrickWells.Data;
using BrickWells.Models.ViewModels;

namespace BrickWells.Models;

public class EFBrickRepository : IBrickRepository
{
    private BrickContext _context;
    
    public EFBrickRepository(BrickContext temp)
    {
        _context = temp;
    }
    
    public IQueryable<Product> Products => _context.Products;
    public IQueryable<Customer> Customers => _context.Customers;
    public IQueryable<Order> Orders => _context.Orders;
    public IQueryable<LineItem> OrderDetails => _context.LineItems;
    public IQueryable<ItemBasedRec> ItemBasedRecs => _context.ItemBasedRecs;

}