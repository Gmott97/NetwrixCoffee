using System;
using System.ComponentModel.DataAnnotations;

namespace NetwrixCoffee.Models
{
    public class CoffeeMachineRecord
    {
        [Key]
        public int CoffeeMachineRecordId { get; set; }
        public int NumEspressoShots { get; set; }
        public bool AddMilk { get; set; }
        public DateTime CreatedDate { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
    }
}
