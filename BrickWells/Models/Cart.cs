namespace BrickWells.Models;

public class Cart
{
    public List<CartLine> Lines { get; set; } = new List<CartLine>();

    public virtual void AddItem(Product prod, int quantity)
    {
        CartLine? line = Lines
            .Where(x => x.Product.ProductId == prod.ProductId)
            .FirstOrDefault();
        
        //Check if the item is already in the cart
        if (line == null)
        {
            Lines.Add(new CartLine
            {
                Product = prod,
                Quantity = quantity
            });
        }
        else
        {
            line.Quantity += quantity;
        }
    }
    
    public virtual void RemoveLine(Product p) => Lines.RemoveAll(x => x.Product.ProductId == p.ProductId);
    
    public virtual void Clear() => Lines.Clear();

    public decimal CalculateTotal() => Lines.Sum(x => (decimal)x.Product.Price * x.Quantity);
    
    public class CartLine
    {
        public int CartLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}