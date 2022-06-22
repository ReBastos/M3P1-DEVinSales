using DevInSales.Api.Controllers;
using DevInSales.Core.Data.Context;
using DevInSales.Core.Entities;
using DevInSales.EFCoreApi.Core.Interfaces;
using Moq;
using Xunit;

namespace DevInSales.Test
{
    public class UserServiceTest
    {

        public readonly UserController _controller;
        public readonly IUserService _service;
        public readonly DataContext _context;

        private Mock<IUserService> _userServiceMock;

        public UserServiceTest()
        {
            _service = new UserService(_context);
            _controller = new UserController(_service);
        }

        [Fact]
        public void CriarUser_RetornandoId()
        {
            //Arrage
            

            //Act


            //Assert
        }
    }
}