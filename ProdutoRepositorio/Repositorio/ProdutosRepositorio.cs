using ProdutoServico.DTO;
using ProdutoServico.Interfaces;

namespace ProdutoRepositorio.Repositorio
{
    public class ProdutosRepositorio : IProdutoRepositorio
    {
        public string Alterar(Produto produto)
        {
            return "Produto alterado";
        }

        public string Cadastrar(Produto produto)
        {
            return "Produto cadastrado";
        }

        public string Remover(int id)
        {
            return "Produto removido";
        }
    }
}
