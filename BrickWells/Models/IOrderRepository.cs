namespace BrickWells.Models;

public interface IOrderRepository
{
    public IQueryable<Order> Orders { get; }
    
    public IQueryable<Customer> Customers { get; }
    
    public void SaveOrder(Order order);
    
    public void AddCustomer(Customer customer);
    public void AddLineItem(LineItem lineItem);
}