using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProdutoAPI.Token;

namespace ProdutoAPI.Controllers.V1
{
    [Route("api/v1")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AutenticacaoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GerarToken")]
        [AllowAnonymous]
        public object GerarToken() 
        {
            var token = new Gerar().Token(_configuration.GetSection("AppSettings:Segredo").Value);

            return new
            {
                autenticado = true,
                token = token
            };
        }
    }
}