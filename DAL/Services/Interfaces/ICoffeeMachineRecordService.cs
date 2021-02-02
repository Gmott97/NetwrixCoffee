using NetwrixCoffee.Models;
using System;
using System.Threading.Tasks;

namespace NetwrixCoffee.DAL.Services
{
    public interface ICoffeeMachineRecordService
    {
        Task AddCoffeeRecord(
            CoffeeCreationOptions options);


        Task<int> GetAverageCoffeeAmountForDayOfWeek(
            DayOfWeek dayOfWeek);


        Task<int> GetAverageCoffeeAmountForHourOfDay(
            int hour);


        Task<string> GetEarliestCoffeeForDayOfWeek(
            DayOfWeek dayOfWeek);


        Task<string> GetLatestCoffeeForDayOfWeek(
            DayOfWeek dayOfWeek);
    }
}
