using BrickWells.Models;
using BrickWells.Data;

namespace BrickWells.Models;

public class EFUserRepository : IUserRepository
{
    private BrickContext _context;
    
    public EFUserRepository(BrickContext temp)
    {
        _context = temp;
    }
    
    public IQueryable<Customer> Customers => _context.Customers;
    public IQueryable<Order> Orders => _context.Orders;
    public IQueryable<LineItem> OrderDetails => _context.LineItems;
    public IQueryable<Product> Products => _context.Products;
    public IQueryable<ItemBasedRec> ItemBasedRecs => _context.ItemBasedRecs;
    
    public void AddCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
        _context.SaveChanges();
    }
}