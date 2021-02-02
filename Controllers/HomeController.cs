using Microsoft.AspNetCore.Mvc;
using NetwrixCoffee.DAL.Services;
using NetwrixCoffee.Models;
using NetwrixCoffee.Services.Interfaces;
using NetwrixCoffee.ViewModels;
using System;
using System.Threading.Tasks;

namespace NetwrixCoffee.Controllers
{
    public class HomeController 
        : Controller
    {
        private readonly ICoffeeMachine _coffeeMachine;
        private readonly ICoffeeMachineRecordService _coffeeMachineRecordService;

        public HomeController(
            ICoffeeMachine coffeeMachine,
            ICoffeeMachineRecordService coffeeMachineRecordService)
        {
            _coffeeMachine = coffeeMachine;
            _coffeeMachineRecordService = coffeeMachineRecordService;
        }


        public IActionResult Index()
        {
            var vm = new CoffeeMachineIndexViewModel
            {
                CoffeeStatus = new CoffeeStatus
                {
                    IsOn = _coffeeMachine.IsOn,
                    IsMakingCoffee = _coffeeMachine.IsMakingCoffee,
                    WaterLevelState = _coffeeMachine.WaterLevelState,
                    BeanFeedState = _coffeeMachine.BeanFeedState,
                    WasteCoffeeState = _coffeeMachine.WasteCoffeeState,
                    WaterTrayState = _coffeeMachine.WaterTrayState,
                    AlertState = _coffeeMachine.GetAlertState()
                }
            };
            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> MakeCoffee(
            CoffeeMachineIndexViewModel vm)
        {
            try
            {
                await _coffeeMachine.MakeCoffeeAsync(vm.CoffeeCreationOptions);

                await _coffeeMachineRecordService.AddCoffeeRecord(vm.CoffeeCreationOptions);

                return Json("Coffee Made");
            }
            catch (InvalidOperationException ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }


        [HttpPost]
        public async Task<IActionResult> FlipMachineSwitch()
        {
            if (!_coffeeMachine.IsOn)
            {
                await _coffeeMachine.TurnOnAsync();
                return Json("Coffee Machine On");
            }
            else
            {
                await _coffeeMachine.TurnOffAsync();
                return Json("Coffee Machine Off");
            }
        }
    }
}
