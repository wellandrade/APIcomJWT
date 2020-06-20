using ProdutoServico.DTO;

namespace ProdutoServico.Interfaces
{
    public interface IProdutoRepositorio
    {
        string Cadastrar(Produto produto);

        string Alterar(Produto produto);

        string Remover(int id);

    }
}
