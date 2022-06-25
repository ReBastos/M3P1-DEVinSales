using DevInSales.Core.Data.Context;
using DevInSales.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace DevInSales.Test.Services
{
    public class UserServiceTest
    {

        private UserService _userService;

        public UserServiceTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new DataContext(options);


            _userService = new UserService(context);

            Seed().Wait();

        }

        private async Task Seed()
        {

            _userService.CriarUser(new User("teste1@teste.com", "123", "Teste 1", DateTime.Now));
            _userService.CriarUser(new User("teste2@teste.com", "123", "Teste 2", DateTime.Now));
            _userService.CriarUser(new User("teste3@teste.com", "123", "Teste 3", DateTime.Now));

        }

        [Fact]

        public async void ObterPorId_RetornandoUser()
        {

            var result = _userService.ObterPorId(1);

            Assert.NotNull(result);


        }


        [Fact]
        public async void CriarUser_RetornandoIdDoUsuarioCriado()
        {

            var id = _userService.CriarUser(new User("teste4@teste.com", "123", "Teste 4", DateTime.Now));
            var result = _userService.ObterPorId(id);

            Assert.NotNull(id);
            Assert.NotNull(result);
            
        }

        [Fact]
        public async void ObterUsers_RetornandoListaUsers()
        {

            var result = _userService.ObterUsers("Teste 1", null, null);

            Assert.NotEmpty(result);

        }

        [Fact]
        public async void RemoverUser_RetornandoListaUsers()
        {

            var id = _userService.CriarUser(new User("teste5@teste.com", "123", "Teste 5", DateTime.Now));

            _userService.RemoverUser(id);

            var result = _userService.ObterPorId(id);

            Assert.Null(result);



        }




    }
}
