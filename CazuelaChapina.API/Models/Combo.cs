namespace CazuelaChapina.API.Models;

    public class Combo
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public bool IsEditable { get; set; }
        public Season Season { get; set; } = Season.None;
        public ICollection<ComboItemTamale> Tamales { get; set; } = new List<ComboItemTamale>();
        public ICollection<ComboItemBeverage> Beverages { get; set; } = new List<ComboItemBeverage>();
    }
