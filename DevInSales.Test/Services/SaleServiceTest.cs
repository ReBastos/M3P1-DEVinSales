using DevInSales.Core.Data.Context;
using DevInSales.Core.Data.Dtos;
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
    public class SaleServiceTest
    {


        private SaleService _saleService;
        private UserService _userService;
        private SaleProductService _saleProductService;
        private ProductService _productService;
        private AddressService _addressService;
        private CityService _cityService;
        private StateService _stateService;
        private DeliveryService _deliveryService;



        public SaleServiceTest()
        {

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new DataContext(options);

            _saleService = new SaleService(context);
            _userService = new UserService(context);
            _saleProductService = new SaleProductService(context);
            _productService = new ProductService(context);
            _addressService = new AddressService(context);
            _cityService = new CityService(context);
            _stateService = new StateService(context);
            _deliveryService = new DeliveryService(context);

            Seed().Wait();

        }


        private async Task Seed()
        {

            _stateService.Add(new State(1, "Estado", "Es"));
            _cityService.Add(new City(1, "Cidade"));
            _addressService.Add(new Address("Endereco 1", "44200-000", 100, "Complemento", 1));
            _userService.CriarUser(new User("teste@teste.com", "1234", "Buyer", DateTime.Now));
            _userService.CriarUser(new User("teste@teste.com", "1234", "Seller", DateTime.Now));
            _saleService.CreateSaleByUserId(new Sale(1, 1, DateTime.Now));
            _productService.CreateNewProduct(new Product("Produto Teste", 20));
            _saleProductService.CreateSaleProduct(1, new SaleProductRequest(1, 20, 10));
            _saleService.CreateDeliveryForASale(new Delivery(1, 1, DateTime.Now));
        }

        [Fact]
        private void GetSaleById_RetornandoNull()
        {

            var result = _saleService.GetSaleById(10);

            Assert.Null(result);

        }

        [Fact]

        private void GetSaleById_RetornandoSale()
        {

            var result = _saleService.GetSaleById(1);

            Assert.NotNull(result);

        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        private void CreateSaleByUserId_RetornandoExcecaoIdNulo(int buyerId, int sellerId)
        {

            var ex = Assert.Throws<ArgumentNullException>(() => _saleService.CreateSaleByUserId(new Sale(buyerId, sellerId, DateTime.Now)));

            var expectedError = "Value cannot be null. (Parameter 'Id não pode ser nulo nem zero.')";

            Assert.Equal(expectedError, ex.Message);
        }

        [Fact]
        private void CreateSaleByUserId_RetornandoExcecaoBuyerIdNaoEncontrado()
        {
            var ex = Assert.Throws<ArgumentException>(() => _saleService.CreateSaleByUserId(new Sale(10, 1, DateTime.Now)));

            var expectedError = "BuyerId não encontrado.";

            Assert.Equal(expectedError, ex.Message);

        }

        [Fact]
        private void CreateSaleByUserId_RetornandoExcecaoSellerIdNaoEncontrado()
        {
            var ex = Assert.Throws<ArgumentException>(() => _saleService.CreateSaleByUserId(new Sale(1, 10, DateTime.Now)));

            var expectedError = "SellerId não encontrado.";

            Assert.Equal(expectedError, ex.Message);

        }

        [Fact]
        private void CreateSaleByUserId_RetornandoIdDaSale()
        {

            var id = _saleService.CreateSaleByUserId(new Sale(1, 1, DateTime.Now));

;            Assert.True(id > 0);

        }

        [Fact]

        private void GetSaleProductsBySaleId_RetornandoNull()
        {

            var result = _saleService.GetSaleProductsBySaleId(1);


            Assert.NotEmpty(result);

        }

        
        [Fact]
        private void GetSaleBySellerId_RetornandoSales()
        {

            var result = _saleService.GetSaleBySellerId(1);

            Assert.NotEmpty(result);

        }

        [Fact]
        private void GetSaleByBuyerId_RetornandoSales()
        {

            var result = _saleService.GetSaleByBuyerId(1);

            Assert.NotEmpty(result);

        }

        [Theory]
        [InlineData(10, 1, 10)]
        [InlineData(1, 10, 10)]
        private void UpdateUnitPrice_RetornandoExcecaoSaleNula(int saleId, int productId, decimal price)
        {

            var ex = Assert.Throws<Exception>(() => _saleService.UpdateUnitPrice(saleId, productId, price));

            Assert.NotNull(ex);

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        private void UpdateUnitPrice_RetornandoExcecaoPrecoMenorIgualAZero(decimal preco)
        {

            var ex = Assert.Throws<ArgumentException>(() => _saleService.UpdateUnitPrice(1, 1, preco));

            Assert.NotNull(ex);

        }

        [Fact]
        private void UpdateUnitPrice_AtualizandoPreco()
        {

            _saleService.UpdateUnitPrice(1, 1, 150);

            var result = _saleService.GetSaleProductsBySaleId(1);

            Assert.Contains(150, result.Select(m => m.UnitPrice));

        }

        [Fact]
        private void UpdateAmount_RetornandoExcecaoSaleNaoEncontrada()
        {

            var ex = Assert.Throws<ArgumentException>(() => _saleService.UpdateAmount(10, 1, 10));

            var expectedError = "Não existe venda com esse Id.";

            Assert.Matches(expectedError, ex.Message);
            Assert.Contains(expectedError, ex.Message );

        }

        [Fact]
        private void UpdateAmount_RetornandoExcecaoProdutoNaoEncontrado()
        {

            var ex = Assert.Throws<ArgumentException>(() => _saleService.UpdateAmount(1, 10, 10));

            var expectedError = "Não existe este produto nesta venda.";

            Assert.Matches(expectedError, ex.Message);
            Assert.Contains(expectedError, ex.Message);

        }


        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        private void UpdateAmount_RetornandoExcecaoQuantidadeMenorIgualAZero(int qtd)
        {

            var ex = Assert.Throws<ArgumentException>(() => _saleService.UpdateAmount(1, 1, qtd));
            
            var expectedError = "Quantidade não pode ser menor ou igual a zero.";

            Assert.Matches(expectedError, ex.Message);
            Assert.Contains(expectedError, ex.Message);
        }

        [Fact]
        private void CreateDeliveryForASale_RetornandoExcecaoSaleNaoEncontrada()
        {
            var ex = Assert.Throws<ArgumentException>(() => _saleService.CreateDeliveryForASale(new Delivery(1,10, DateTime.Now)));

            var expectedError = "Não existe venda com esse Id.";

            Assert.Matches(expectedError, ex.Message);
            Assert.Contains(expectedError, ex.Message);
        }

        [Fact]
        private void CreateDeliveryForASale_RetornandoExcecaoEnderecoNaoEncontrado()
        {
            var ex = Assert.Throws<ArgumentException>(() => _saleService.CreateDeliveryForASale(new Delivery(10, 1, DateTime.Now)));

            var expectedError = "Não existe endereço com esse Id.";

            Assert.Matches(expectedError, ex.Message);
            Assert.Contains(expectedError, ex.Message);
        }

        [Fact]
        private void CreateDeliveryForASale_RetornandoDeliveryId()
        {

            var result = _saleService.CreateDeliveryForASale(new Delivery(1, 1, DateTime.Now));

            Assert.NotNull(result);
            Assert.True(result > 0);

        }

        [Fact]
        private void GetDeliveryById()
        {

            var result = _saleService.GetDeliveryById(1);

            Assert.NotNull(result);

        }

    }
}
