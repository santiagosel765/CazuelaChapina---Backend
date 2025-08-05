namespace CazuelaChapina.API.Models;

public class Tamale
{
    public int Id { get; set; }
    public int TamaleTypeId { get; set; }
    public TamaleType TamaleType { get; set; } = null!;
    public int FillingId { get; set; }
    public Filling Filling { get; set; } = null!;
    public int WrapperId { get; set; }
    public Wrapper Wrapper { get; set; } = null!;
    public int SpiceLevelId { get; set; }
    public SpiceLevel SpiceLevel { get; set; } = null!;
    public decimal Price { get; set; }
    public ICollection<ComboItemTamale> ComboItems { get; set; } = new List<ComboItemTamale>();
}
