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
    public class UsuarioController : ControllerBase
    {


        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {

            _usuarioService = usuarioService;
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(Usuario usuario)
        {

            if (usuario.Role != "Administrador" &&
                usuario.Role != "Gerente" &&
                usuario.Role != "Usuario")
                return BadRequest("Role deve ser Administrador, Gerente, ou Usuario");


            _usuarioService.CriarUsuario(usuario);

            return Ok();

        }

        

    }
}
