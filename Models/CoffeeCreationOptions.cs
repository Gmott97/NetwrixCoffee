using System.ComponentModel.DataAnnotations;

namespace NetwrixCoffee.Models
{
    public class CoffeeCreationOptions
    {
        public int NumEspressoShots { get; set; }
        public bool AddMilk { get; set; }
    }
}
