using NetwrixCoffee.Models.Enums;

namespace NetwrixCoffee.Models
{
    public class CoffeeStatus
    {
        public bool IsOn { get; set; }
        public bool IsMakingCoffee { get; set; }
        public State WaterLevelState { get; set; }
        public State BeanFeedState { get; set; }
        public State WasteCoffeeState { get; set; }
        public State WaterTrayState { get; set; }
        public bool AlertState { get; set; }
    }
}
