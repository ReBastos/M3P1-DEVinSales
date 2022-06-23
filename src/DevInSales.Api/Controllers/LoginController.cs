using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;
using DevInSales.Core.Interfaces;
using DevInSales.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevInSales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;

        public LoginController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public ActionResult Logar(ReadLogin login)
        {
            if (login == null)
                return BadRequest();

            var account = _usuarioService.LogarUsuario(login);



            var token = TokenService.GerarToken(account);

            return Ok(new LoginResponse()
            {
                Nome = account.Nome,
                Username = account.Username,
                Token = token
            });



        }

    }
}
