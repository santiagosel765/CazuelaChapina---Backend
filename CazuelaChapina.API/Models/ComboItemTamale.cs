namespace CazuelaChapina.API.Models;

public class ComboItemTamale
{
    public int ComboId { get; set; }
    public Combo Combo { get; set; } = null!;
    public int TamaleId { get; set; }
    public Tamale Tamale { get; set; } = null!;
    public int Quantity { get; set; }
}
