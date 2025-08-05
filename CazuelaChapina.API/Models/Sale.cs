namespace CazuelaChapina.API.Models;

public class Sale
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Total { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
    public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
}
