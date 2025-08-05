namespace CazuelaChapina.API.Models;

public class SaleItem
{
    public int Id { get; set; }
    public int SaleId { get; set; }
    public Sale Sale { get; set; } = null!;

    public int? TamaleId { get; set; }
    public Tamale? Tamale { get; set; }

    public int? BeverageId { get; set; }
    public Beverage? Beverage { get; set; }

    public int Quantity { get; set; }
    public decimal Subtotal { get; set; }
}
