using Microsoft.AspNetCore.Mvc;
using NetwrixCoffee.DAL.Services;
using NetwrixCoffee.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetwrixCoffee.Controllers
{
    public class UtilisationController
        : Controller
    {
        private readonly ICoffeeMachineRecordService _coffeeMachineRecordService;

        public UtilisationController(
            ICoffeeMachineRecordService coffeeMachineRecordService)
        {
            _coffeeMachineRecordService = coffeeMachineRecordService;
        }


        public async Task<IActionResult> Index()
        {
            var vm = new CoffeeMachineUtilisationViewModel
            {
                AverageCoffeePerDay = new List<int>(),
                EarliestCoffeePerDay = new List<string>(),
                LatestCoffeePerDay = new List<string>(),
                AverageCoffeePerHour = new List<int>()
            };

            for (var i = 0; i < 7; i++)
            {
                vm.AverageCoffeePerDay.Add(
                    await _coffeeMachineRecordService.GetAverageCoffeeAmountForDayOfWeek((DayOfWeek)i));
                vm.EarliestCoffeePerDay.Add(
                    await _coffeeMachineRecordService.GetEarliestCoffeeForDayOfWeek((DayOfWeek)i));
                vm.LatestCoffeePerDay.Add(
                    await _coffeeMachineRecordService.GetLatestCoffeeForDayOfWeek((DayOfWeek)i));
            }

            for (var i = 0; i < 24; i++)
            {
                vm.AverageCoffeePerHour.Add(
                    await _coffeeMachineRecordService.GetAverageCoffeeAmountForHourOfDay(i));
            }

            return View(vm);
        }
    }
}
