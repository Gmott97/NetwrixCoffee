using NetwrixCoffee.Models;
using NetwrixCoffee.Models.Enums;
using NetwrixCoffee.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetwrixCoffee.Services
{
    public class CoffeeMachine 
        : ICoffeeMachine
    {
        #region Fields
        public bool IsOn { get; private set; }
        public bool IsMakingCoffee { get; private set; }
        public State WaterLevelState { get; private set; }
        public State BeanFeedState { get; private set; }
        public State WasteCoffeeState { get; private set; }
        public State WaterTrayState { get; private set; }

        private readonly Random _randomStateGenerator;
        #endregion Fields
        
        
        
        #region CTORs
        public CoffeeMachine()
        {
            _randomStateGenerator = new Random();
        }
        #endregion CTORs



        #region Public
        public async Task TurnOnAsync()
        {
            if (IsOn)
            {
                throw new InvalidOperationException("Invalid state");
            }

            // Generate sample state for testing
            WaterLevelState = GetRandomState();
            BeanFeedState = GetRandomState();
            WasteCoffeeState = GetRandomState();
            WaterTrayState = GetRandomState();

            // [Machine turned on]
            IsOn = true;
        }


        public async Task TurnOffAsync()
        {
            if (!IsOn || IsMakingCoffee)
            {
                throw new InvalidOperationException("Invalid state");
            }

            // [Machine turned off]
            IsOn = false;
        }


        public async Task MakeCoffeeAsync(
            CoffeeCreationOptions options)
        {
            if (!IsOn || IsMakingCoffee || GetAlertState())
            {
                throw new InvalidOperationException("Invalid state");
            }
            IsMakingCoffee = true;

            // [Make the coffee]
            Thread.Sleep(10000);
            IsMakingCoffee = false;
        }


        public bool GetAlertState() => WaterLevelState == State.Alert
                                    || BeanFeedState == State.Alert
                                    || WasteCoffeeState == State.Alert
                                    || WaterTrayState == State.Alert;
        #endregion Public



        #region Private
        // Randomly create a state for testing. This can be replaced as required.
        private State GetRandomState() =>
            _randomStateGenerator.Next(1, 10) == 9 ? State.Alert : State.Okay;
        #endregion Private
    }
}
 