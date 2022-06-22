using DevInSales.Core.Data.Context;
using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;
using DevInSales.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Core.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly DataContext _context;
        public UsuarioService(DataContext context)
        {
            _context = context;
        }

        public int CriarUsuario(Usuario usuario)
        {

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return usuario.Id;

        }

        public Usuario LogarUsuario(ReadLogin login)
        {

            var account = _context.Usuarios.FirstOrDefault(p => p.Username == login.Username && p.Senha == login.Senha);

            if (account == null)
                throw new Exception("Usuário inválido");

            return account;
            

        }
    }
}
