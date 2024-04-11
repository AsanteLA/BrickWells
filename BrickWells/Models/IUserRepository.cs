namespace BrickWells.Models;

public interface IUserRepository
{
    public IQueryable<Customer> Customers { get; }
    
    public IQueryable<Order> Orders { get; }
    
    //Adding a user to the customers table
    public void AddCustomer(Customer customer);
}