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
    
    public void AddCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
        _context.SaveChanges();
    }
}