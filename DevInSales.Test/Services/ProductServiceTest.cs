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
    public class ProductServiceTest
    {

        private ProductService _productService;

        public ProductServiceTest()
        {

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new DataContext(options);

            _productService = new ProductService(context);

            Seed().Wait();

        }

        private async Task Seed()
        {

            _productService.CreateNewProduct(new Product("Produto 1", 10));
            _productService.CreateNewProduct(new Product("Produto 2", 20));
            _productService.CreateNewProduct(new Product("Produto 3", 30));

        }

        [Fact]
        public async void ObterProductPorId_Retornandoproduto()
        {

            var result = _productService.ObterProductPorId(1);

            Assert.NotNull(result);

        }

        [Fact]
        public async void ProdutoExiste_RetornandoQuantidadeDeProduto()
        {

            var count = _productService.ProdutoExiste("Produto 1");

            Assert.NotNull(count);
            Assert.True(count);

        }


        [Fact]
        public async void Delete_RetornandoExcecaoNaoEncontrado()
        {
            var expectedErrorMessage = "o Produto não existe";
            var ex = Assert.Throws<Exception>(() => _productService.Delete(10));

            Assert.Equal(expectedErrorMessage, ex.Message);

        }

        [Fact]
        public async void Delete_DeletandoProduto()
        {

            var id = _productService.CreateNewProduct(new Product("Produto 4", 40));

            _productService.Delete(id);

            var result = _productService.ObterProductPorId(id);

            Assert.Null(result);

        }

        [Fact]
        public async void CreateNewProduct_RetornandoIdProdutoCriado()
        {

            var id = _productService.CreateNewProduct(new Product("Produto 6", 60));
            var result = _productService.ObterProductPorId(id);

            Assert.NotNull(result);

        }
    }
}
