using DevInSales.Core.Data.Context;
using DevInSales.Core.Entities;
using DevInSales.Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Test.Services
{
    public class SaleProductServiceTest
    {

        private SaleProductService _saleProductService;
        private ProductService _productService;

        public SaleProductServiceTest()
        {

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new DataContext(options);

            _saleProductService = new SaleProductService(context);



        }

    }
}
