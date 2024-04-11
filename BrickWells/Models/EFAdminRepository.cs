using BrickWells.Data;

namespace BrickWells.Models;

public class EFAdminRepository : IAdminRepository
{
    private BrickContext _context;
    
    public EFAdminRepository(BrickContext temp)
    {
        _context = temp;
    }
    
    public IQueryable<Order> Orders => _context.Orders;
    public IQueryable<Product> Products => _context.Products;
    public IQueryable<Customer> Customers => _context.Customers;
    
    //Product Methods
    public void AddProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }
    
    public void EditProduct(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
    }
    
    public void DeleteProduct(Product product)
    {
        _context.Products.Remove(product);
        _context.SaveChanges();
    }
    
    //Customer Methods
    public void EditCustomer(Customer customer)
    {
        _context.Customers.Update(customer);
        _context.SaveChanges();
    }
    
    public void DeleteCustomer(Customer customer)
    {
        _context.Customers.Remove(customer);
        _context.SaveChanges();
    }
    
    //Order Methods
}