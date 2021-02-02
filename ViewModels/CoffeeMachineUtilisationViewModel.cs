using NetwrixCoffee.Models;
using System;
using System.Collections.Generic;

namespace NetwrixCoffee.ViewModels
{
    public class CoffeeMachineUtilisationViewModel
    {
        public List<string> EarliestCoffeePerDay { get; set; }
        public List<string> LatestCoffeePerDay { get; set; }
        public List<int> AverageCoffeePerDay { get; set; }
        public List<int> AverageCoffeePerHour { get; set; }
    }
}