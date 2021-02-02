using Microsoft.AspNetCore.Mvc;
using NetwrixCoffee.DAL.Services;
using NetwrixCoffee.Models;
using NetwrixCoffee.Models.Enums;
using NetwrixCoffee.Services.Interfaces;
using System;
using System.Threading.Tasks;

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


        [HttpGet("IsOn")]
        public ActionResult<bool> GetIsOn()
        {
            return _coffeeMachine.IsOn;
        }


        [HttpPost("Power")]
        public async Task<ActionResult<bool>> PostPowerAsync(
            [FromBody] Power power)
        {
            if (power.IsOn == _coffeeMachine.IsOn)
            {
                return power.IsOn;
            }
            else if (power.IsOn)
            {
                await _coffeeMachine.TurnOnAsync();
            }
            else
            {
                await _coffeeMachine.TurnOffAsync();
            }
            return _coffeeMachine.IsOn;
        }


        [HttpGet("MakingCoffee")]
        public ActionResult<bool> GetMakingCoffee()
        {
            return _coffeeMachine.IsMakingCoffee;
        }


        [HttpPost("MakingCoffee")]
        public async Task<IActionResult> PostMakingCoffeeAsync(
            [FromBody] CoffeeCreationOptions vm)
        {
            try
            {
                await _coffeeMachine.MakeCoffeeAsync(vm);

                //await _coffeeMachineRecordService.AddCoffeeRecord(vm);

                return Ok("Coffee Made");
            }
            catch (InvalidOperationException ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
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