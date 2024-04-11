namespace BrickWells.Models;

public interface IUserRepository
{
    public IQueryable<Customer> Customers { get; }
    
    public IQueryable<Order> Orders { get; }
    public IQueryable<LineItem> OrderDetails { get; }
    public IQueryable<Product> Products { get; }
    public IQueryable<ItemBasedRec> ItemBasedRecs { get; }
    
    //Adding a user to the customers table
    public void AddCustomer(Customer customer);
}