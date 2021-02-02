using Microsoft.EntityFrameworkCore;
using NetwrixCoffee.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetwrixCoffee.DAL.Services
{
    public class CoffeeMachineRecordMockService
        : ICoffeeMachineRecordService
    {
        #region Fields
        private CoffeeMachineContext _coffeeMachineContext;
        #endregion Fields



        #region CTORs
        public CoffeeMachineRecordMockService()
        {
            var options = new DbContextOptionsBuilder<CoffeeMachineContext>()
                .UseInMemoryDatabase(databaseName: "CoffeeMachine")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new CoffeeMachineContext(options))
            {
                context.CoffeeMachineRecords.Add(
                    new CoffeeMachineRecord 
                    { 
                        CoffeeMachineRecordId = 1,
                        NumEspressoShots = 1,
                        AddMilk = true, 
                        CreatedDate = DateTime.Parse("8/2/2020 18:00:30 PM"), 
                        DayOfWeek = DayOfWeek.Saturday 
                    });
                context.CoffeeMachineRecords.Add(
                    new CoffeeMachineRecord 
                    { 
                        CoffeeMachineRecordId = 2,
                        NumEspressoShots = 5,
                        AddMilk = false,
                        CreatedDate = DateTime.Parse("8/7/2020 04:10:20 AM"), 
                        DayOfWeek = DayOfWeek.Wednesday 
                    });
                context.CoffeeMachineRecords.Add(
                    new CoffeeMachineRecord 
                    { 
                        CoffeeMachineRecordId = 3,
                        NumEspressoShots = 3,
                        AddMilk = true, 
                        CreatedDate = DateTime.Parse("17/10/2020 11:03:05 AM"),
                        DayOfWeek = DayOfWeek.Saturday 
                    });
                context.CoffeeMachineRecords.Add(
                    new CoffeeMachineRecord 
                    { 
                        CoffeeMachineRecordId = 4,
                        NumEspressoShots = 1,
                        AddMilk = true, 
                        CreatedDate = DateTime.Parse("31/12/2020 15:30:19 PM"),
                        DayOfWeek = DayOfWeek.Thursday
                    });
                context.CoffeeMachineRecords.Add(
                    new CoffeeMachineRecord 
                    { 
                        CoffeeMachineRecordId = 5,
                        NumEspressoShots = 2,
                        AddMilk = false,
                        CreatedDate = DateTime.Parse("10/5/2020 16:46:21 PM"),
                        DayOfWeek = DayOfWeek.Sunday 
                    });
                context.SaveChanges();
            }

            _coffeeMachineContext = new CoffeeMachineContext(options);
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
                .FirstOrDefaultAsync();

            return earliestCoffeeRecordForDayOfWeek?.CreatedDate.ToShortTimeString() ?? "-";
        }


        public async Task<string> GetLatestCoffeeForDayOfWeek(
            DayOfWeek dayOfWeek)
        {
            var earliestCoffeeRecordForDayOfWeek = await _coffeeMachineContext
                .CoffeeMachineRecords
                .Where(c => c.CreatedDate.DayOfWeek == dayOfWeek)
                .OrderByDescending(c => c.CreatedDate.TimeOfDay)
                .FirstOrDefaultAsync();

            return earliestCoffeeRecordForDayOfWeek?.CreatedDate.ToShortTimeString() ?? "-";
        }
        #endregion Public
    }
}
