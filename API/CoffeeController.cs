using Microsoft.AspNetCore.Mvc;
using NetwrixCoffee.DAL.Services;
using NetwrixCoffee.Models;
using NetwrixCoffee.Models.Enums;
using NetwrixCoffee.Services.Interfaces;

namespace NetwrixCoffee.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeController 
        : ControllerBase
    {
        private readonly ICoffeeMachine _coffeeMachine;
        private readonly ICoffeeMachineRecordService _coffeeMachineRecordService;

        public CoffeeController(
            ICoffeeMachine coffeeMachine,
            ICoffeeMachineRecordService coffeeMachineRecordService)
        {
            _coffeeMachine = coffeeMachine;
            _coffeeMachineRecordService = coffeeMachineRecordService;
        }


        [HttpGet("On")]
        public ActionResult<bool> GetOn()
        {
            return _coffeeMachine.IsOn;
        }


        [HttpGet("MakingCoffee")]
        public ActionResult<bool> GetMakingCoffee()
        {
            return _coffeeMachine.IsMakingCoffee;
        }


        [HttpGet("WaterLevel")]
        public ActionResult<State> GetWaterLevel()
        {
            return _coffeeMachine.WaterLevelState;
        }


        [HttpGet("BeanFeed")]
        public ActionResult<State> GetBeanFeed()
        {
            return _coffeeMachine.BeanFeedState;
        }


        [HttpGet("WasteCoffee")]
        public ActionResult<State> GetWasteCoffee()
        {
            return _coffeeMachine.WasteCoffeeState;
        }


        [HttpGet("WaterTray")]
        public ActionResult<State> GetWaterTray()
        {
            return _coffeeMachine.WaterTrayState;
        }


        [HttpGet("Alert")]
        public ActionResult<bool> GetAlert()
        {
            return _coffeeMachine.GetAlertState();
        }


        [HttpGet("Status")]
        public ActionResult<CoffeeStatus> GetStatus()
        {
            return new CoffeeStatus
            {
                IsOn = _coffeeMachine.IsOn,
                IsMakingCoffee = _coffeeMachine.IsMakingCoffee,
                WaterLevelState = _coffeeMachine.WaterLevelState,
                BeanFeedState = _coffeeMachine.BeanFeedState,
                WasteCoffeeState = _coffeeMachine.WasteCoffeeState,
                WaterTrayState = _coffeeMachine.WaterTrayState,
                AlertState = _coffeeMachine.GetAlertState()
            };
        }
    }
}