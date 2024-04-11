namespace BrickWells.Models;

public interface IAdminRepository
{
    public IQueryable<Order> Orders { get; }
    public IQueryable<Product> Products { get; }
    public IQueryable<Customer> Customers { get; }
    
    //Product Methods
    public void AddProduct(Product product);
    public void EditProduct(Product product);
    public void DeleteProduct(Product product);
    
    //Customer Methods
    public void EditCustomer(Customer customer);
    public void DeleteCustomer(Customer customer);
    
    //Order Methods
    
    
}