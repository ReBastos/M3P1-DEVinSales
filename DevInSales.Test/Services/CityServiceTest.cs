using DevInSales.Core.Data.Context;
using DevInSales.Core.Entities;
using DevInSales.Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DevInSales.Test.Services
{
    public class CityServiceTest
    {

        private CityService _cityService;
        private StateService _stateService;

        public CityServiceTest()
        {

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _cityService = new CityService(new DataContext(options));
            _stateService = new StateService(new DataContext(options));

            Seed().Wait();
        }

        private async Task Seed()
        {
            _stateService.Add(new State(1, "Estado", "Es"));
            _cityService.Add(new City(1, "Santo Amaro"));
            _cityService.Add(new City(1, "Salvador"));
            _cityService.Add(new City(1, "Cachoeira"));
          
        }


        [Fact]

        public void GetById_RetornaCity()
        {

            var result = _cityService.GetById(1);
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);

        }



    }
}
