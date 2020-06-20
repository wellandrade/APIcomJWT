using ProdutoServico.DTO;

namespace ProdutoServico.Interfaces
{
    public interface IProdutoServico
    {
        string GravarProduto(Produto produto);

        string AlterarProduto(Produto produto);

        string RemoverProduto(int id);
    }
}
