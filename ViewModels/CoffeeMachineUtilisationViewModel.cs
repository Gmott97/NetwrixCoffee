using NetwrixCoffee.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace NetwrixCoffee.ViewModels
{
    public class CoffeeMachineUtilisationViewModel
    {
        [DisplayName("Earliest Coffee")]
        public List<string> EarliestCoffeePerDay { get; set; }
        [DisplayName("Latest Coffee")]
        public List<string> LatestCoffeePerDay { get; set; }
        [DisplayName("Average Coffees")]
        public List<int> AverageCoffeePerDay { get; set; }
        [DisplayName("Average Coffees")]
        public List<int> AverageCoffeePerHour { get; set; }
    }
}