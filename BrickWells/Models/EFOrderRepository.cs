using BrickWells.Data;
using Microsoft.EntityFrameworkCore;

namespace BrickWells.Models;

public class EFOrderRepository : IOrderRepository
{
    private BrickContext _context;
    
    public EFOrderRepository(BrickContext temp)
    {
        _context = temp;
    }

    public IQueryable<Order> Orders => _context.Orders;

    public IQueryable<Customer> Customers => _context.Customers;
    

    public void SaveOrder(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
    }
    
    public void AddCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
        _context.SaveChanges();
    }
}