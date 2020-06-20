using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProdutoServico.DTO;
using ProdutoServico.Interfaces;

namespace ProdutoAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoServico _produtoServico;

        public ProdutoController(IProdutoServico produtoServico)
        {
            _produtoServico = produtoServico;
        }

        /// <summary>
        /// Cadastro de produto 
        /// </summary>
        /// <param name="produto">Produto</param>
        /// <returns></returns>
        [HttpPost("v1/Cadastrar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public ActionResult<Retorno> CadastraProduto([FromBody]Produto produto)
        {
            Retorno retorno = new Retorno();

            var gravouProduto = _produtoServico.GravarProduto(produto);

            if (gravouProduto == null)
            {
                retorno.Mensagem = "Falha ao gravar produto";
                return BadRequest(retorno);
            }

            retorno.Sucesso = true;
            retorno.Mensagem = "Produto gravado com sucesso";

            return Ok(retorno);
        }

        /// <summary>
        /// Alteracao do produto
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        [HttpPut("v1/Alterar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public ActionResult<Retorno> AlterarProduto([FromBody] Produto produto)
        {
            Retorno retorno = new Retorno();

            var alterouProduto = _produtoServico.AlterarProduto(produto);

            if (alterouProduto == null)
            {
                retorno.Mensagem = "Falha ao alterar produto";
                return BadRequest(retorno);
            }

            retorno.Sucesso = true;
            retorno.Mensagem = "Produto alterado com sucesso";

            return Ok(retorno);
        }

        /// <summary>
        /// Remover o produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("v1/Deletar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public ActionResult<Retorno> RemoverProduto([FromQuery] int id)
        {
            Retorno retorno = new Retorno();

            var removeuProduto = _produtoServico.RemoverProduto(id);

            if (removeuProduto == null)
            {
                retorno.Mensagem = "Falha ao remover produto";
                return BadRequest(retorno);
            }

            retorno.Sucesso = true;
            retorno.Mensagem = "Produto removido com sucesso";

            return Ok(retorno);
        }
    }
}