namespace BrickWells.Models;

public interface IOrderRepository
{
    public IQueryable<Order> Orders { get; }
    
    public IQueryable<Customer> Customers { get; }
    
    public void SaveOrder(Order order);
    
    //Adding a user to the customers table
    public void AddCustomer(Customer customer);
}