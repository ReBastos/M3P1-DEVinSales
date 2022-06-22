using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Core.Data.Dtos
{
    public class LoginResponse
    {

        public string Username { get; set; }
        public string Nome { get; set; }
        public string Token { get; set; }
    }
}
