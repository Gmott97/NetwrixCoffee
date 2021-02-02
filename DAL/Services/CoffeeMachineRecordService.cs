using Microsoft.EntityFrameworkCore;
using NetwrixCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetwrixCoffee.DAL.Services
{
    public class CoffeeMachineRecordService
        : ICoffeeMachineRecordService
    {
        #region Fields
        private CoffeeMachineContext _coffeeMachineContext;
        #endregion Fields



        #region CTORs
        public CoffeeMachineRecordService(
            CoffeeMachineContext coffeeMachineContext)
        {
            _coffeeMachineContext = coffeeMachineContext;
        }
        #endregion CTORs


        #region Public
        public async Task AddCoffeeRecord(
            CoffeeCreationOptions options)
        {
            var record = new CoffeeMachineRecord
            {
                AddMilk = options.AddMilk,
                NumEspressoShots = options.NumEspressoShots,
                CreatedDate = DateTime.Now,
                DayOfWeek = DateTime.Now.DayOfWeek
            };

            await _coffeeMachineContext
                .CoffeeMachineRecords
                .AddAsync(record);

            await _coffeeMachineContext.SaveChangesAsync();
        }


        public async Task<int> GetAverageCoffeeAmountForDayOfWeek(
            DayOfWeek dayOfWeek)
        {
            var coffeesForDayOfWeek = await _coffeeMachineContext
                .CoffeeMachineRecords
                .Where(c => c.DayOfWeek == dayOfWeek)
                .ToListAsync();

            var totalDaysMachineRanForDayOfWeek = coffeesForDayOfWeek
                .Select(c => c.CreatedDate.Date)
                .Distinct()
                .Count();

            return totalDaysMachineRanForDayOfWeek == 0
                ? 0
                : (int)Math.Round(
                    (decimal)coffeesForDayOfWeek.Count() / (decimal)totalDaysMachineRanForDayOfWeek);
        }


        public async Task<int> GetAverageCoffeeAmountForHourOfDay(
            int hour)
        {
            var coffeesForHourOfDay = await _coffeeMachineContext
                .CoffeeMachineRecords
                .Where(c => c.CreatedDate.Hour == hour)
                .ToListAsync();

            var totalDaysMachineRanForHourOfDay = coffeesForHourOfDay
                .Select(c => c.CreatedDate.Date)
                .Distinct()
                .Count();

            return totalDaysMachineRanForHourOfDay == 0
                ? 0
                : (int)Math.Round(
                    (decimal)coffeesForHourOfDay.Count() / (decimal)totalDaysMachineRanForHourOfDay);
        }


        public async Task<string> GetEarliestCoffeeForDayOfWeek(
            DayOfWeek dayOfWeek)
        {
            var earliestCoffeeRecordForDayOfWeek = await _coffeeMachineContext
                .CoffeeMachineRecords
                .Where(c => c.CreatedDate.DayOfWeek == dayOfWeek)
                .OrderBy(c => c.CreatedDate.TimeOfDay)
                .FirstAsync();

            return earliestCoffeeRecordForDayOfWeek?.CreatedDate.ToShortTimeString() ?? "";
        }


        public async Task<string> GetLatestCoffeeForDayOfWeek(
            DayOfWeek dayOfWeek)
        {
            var earliestCoffeeRecordForDayOfWeek = await _coffeeMachineContext
                .CoffeeMachineRecords
                .Where(c => c.CreatedDate.DayOfWeek == dayOfWeek)
                .OrderByDescending(c => c.CreatedDate.TimeOfDay)
                .FirstAsync();

            return earliestCoffeeRecordForDayOfWeek?.CreatedDate.ToShortTimeString() ?? "";
        }
        #endregion Public
    }
}
