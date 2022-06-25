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
    public class DeliveryServiceTest
    {

        private DeliveryService _deliveryService;
        private SaleService _saleService;
        private UserService _userService;
        private AddressService _addressService;
        private CityService _cityService;
        private StateService _stateService;

        public DeliveryServiceTest()
        {

            

            var options = new DbContextOptionsBuilder<DataContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var context = new DataContext(options);

            _deliveryService = new DeliveryService(context);
            _saleService = new SaleService(context);
            _userService = new UserService(context);
            _addressService = new AddressService(context);
            _cityService = new CityService(context);
            _stateService = new StateService(context);

            Seed().Wait();

        }

        public async Task Seed()
        {
            _stateService.Add(new State(1, "Estado", "Es"));
            _cityService.Add(new City(1, "Cidade"));
            _addressService.Add(new Address("Endereco 1", "44200-000", 100, "Complemento", 1));
            _userService.CriarUser(new User("teste@teste.com", "1234", "Buyer", DateTime.Now));
            _userService.CriarUser(new User("teste@teste.com", "1234", "Seller", DateTime.Now));
            _saleService.CreateSaleByUserId(new Sale(1, 2, DateTime.Now));
            _saleService.CreateDeliveryForASale(new Delivery(1, 1, DateTime.Now));

        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(1, 1)]
        public void GetBy_RetornandoListaDeDelivery(int? idAddress, int? idSale)
        {

            var result = _deliveryService.GetBy(idAddress, idSale);

            Assert.NotNull(result);

            Assert.Contains(1, result.Select(x => x.Id));

        }


    }
}
