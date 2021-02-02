using NetwrixCoffee.Models;
using NetwrixCoffee.Models.Enums;
using System.Threading.Tasks;

namespace NetwrixCoffee.Services.Interfaces
{
    public interface ICoffeeMachine
    {
        bool IsOn { get; }
        bool IsMakingCoffee { get; }
        State WaterLevelState { get; }
        State BeanFeedState { get; }
        State WasteCoffeeState { get; }
        State WaterTrayState { get; }

        Task TurnOnAsync();
        
        
        Task TurnOffAsync();


        Task MakeCoffeeAsync(
            CoffeeCreationOptions options);


        bool GetAlertState();
    }
}
