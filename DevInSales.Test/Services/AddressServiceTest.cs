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
    public class AddressServiceTest
    {

      
        private AddressService _addressService;
        private CityService _cityService;
        private StateService _stateService;

        public AddressServiceTest()
        {

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new DataContext(options);

            _cityService = new CityService(context);

            _addressService = new AddressService(context);
            
            _stateService = new StateService(context);

            Seed().Wait();

        }

        private async Task Seed()
        {

            _stateService.Add(new State(1, "Estado", "Es"));
            _cityService.Add(new City(1, "Cidade"));
            _addressService.Add(new Address("Endereco 1", "44200-000", 100, "Complemento1", 1));
            _addressService.Add(new Address("Endereco 2", "44200-000", 1001, "Complemento2", 1));
            _addressService.Add(new Address("Endereco 3", "44200-000", 10001, "Complemento3", 1));

        }

        [Fact]

        public async void GetById_RetornandoAddress()
        {

            var result = _addressService.GetById(1);
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);

        }


        [Theory]
        [InlineData(1, null, null, null)]
        [InlineData(null, 1, null, null)]
        [InlineData(null, null, "Endereco 1", null)]
        [InlineData(null, null, null, "44200-000")]
        public async void GetAll_RetornaListaDeAddress(int? stateId, int? cityId, string? street, string? cep)
        {

            var result = _addressService.GetAll(stateId, cityId, street, cep);

            Assert.NotEmpty(result);
            Assert.Contains(1, result.Select(x => x.City.Id));
            Assert.Contains(1, result.Select(x => x.State.Id));
            Assert.Contains("Endereco 1", result.Select(x => x.Street));
            Assert.Contains("44200-000", result.Select(x => x.Cep));


        }

        [Fact]

        public async Task Add_RetornaAddress()
        {

            _addressService.Add(new Address("Endereco 4", "44200-000", 1, "Complemento", 1));

            var result = _addressService.GetAll(1, null, null, null);

            Assert.NotEmpty(result);
            Assert.Contains("Endereco 4", result.Select(x => x.Street));


        }
         
    }
}
