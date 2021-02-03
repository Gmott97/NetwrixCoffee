using NetwrixCoffee.Models.Enums;
using System.ComponentModel;

namespace NetwrixCoffee.Models
{
    public class CoffeeStatus
    {
        public bool IsOn { get; set; }
        public bool IsMakingCoffee { get; set; }
        [DisplayName("Water Level")]
        public State WaterLevelState { get; set; }
        [DisplayName("Bean Feed")]
        public State BeanFeedState { get; set; }
        [DisplayName("Waste Coffee")]
        public State WasteCoffeeState { get; set; }
        [DisplayName("Water Tray")]
        public State WaterTrayState { get; set; }
        public bool AlertState { get; set; }
    }
}
