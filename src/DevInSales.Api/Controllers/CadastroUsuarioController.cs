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
    public class CadastroUsuarioController : ControllerBase
    {


        private readonly IUsuarioService _usuarioService;

        public CadastroUsuarioController(IUsuarioService usuarioService)
        {

            _usuarioService = usuarioService;
        }

        [HttpPost]

        public ActionResult CadastrarUsuario(Usuario usuario)
        {

            if (usuario.Role != "Administrador" ||
                usuario.Role != "Gerente" ||
                usuario.Role != "Usuario")
                return BadRequest("Role deve ser Administrador, Gerente, ou Usuario");


            var id = _usuarioService.CriarUsuario(usuario);

            return CreatedAtAction(nameof(CadastrarUsuario), new { id = id }, id);

        }

        [HttpPost]

        public IActionResult Logar(ReadLogin login)
        {
            if (login == null)
                return BadRequest();

            _usuarioService.LogarUsuario


            var usuarioTeste = new Usuario()
            {
                Nome = "Renato",
                Username = "natinho",
                Senha = "1502",
                Role = "Administrador"
            };

            var token = TokenService.GerarToken(usuarioTeste);

            return Ok(new LoginResponse()
            {
                Nome = usuarioTeste.Nome,
                Username = usuarioTeste.Username,
                Token = token
            });



        }

    }
}
