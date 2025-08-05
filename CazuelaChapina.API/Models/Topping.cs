namespace CazuelaChapina.API.Models;

public class Topping
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<BeverageTopping> BeverageToppings { get; set; } = new List<BeverageTopping>();
}
