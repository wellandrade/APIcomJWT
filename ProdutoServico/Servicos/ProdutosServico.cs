using ProdutoServico.DTO;
using ProdutoServico.Interfaces;

namespace ProdutoServico.Servicos
{
    public class ProdutosServicos : IProdutoServico
    {
        private readonly IProdutoRepositorio _produtoRepositorio;

        public ProdutosServicos(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public string GravarProduto(Produto produto)
        {
            if (produto == null)
            {
                return null;
            }

            return _produtoRepositorio.Cadastrar(produto);
        }

        public string AlterarProduto(Produto produto)
        {
            if (produto == null)
            {
                return null;
            }

            return _produtoRepositorio.Alterar(produto);
        }

        public string RemoverProduto(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return _produtoRepositorio.Remover(id);
        }
    }
}
