using AMcom.Teste.DAL.Model;

namespace AMcom.Teste.Service.DTOs
{
    public class UbsDTO
    {
        // Esta classe deve conter as seguintes propriedades:
        // Nome, Endereco e Avaliacao

        public string Nome { get; private set; }
        public string Endereco { get; private set; }
        public string Avaliacao { get; private set; }

        public UbsDTO(string nome, string endereco, string avaliacao)
        {
            Nome = nome;
            Endereco = endereco;
            Avaliacao = avaliacao;
        }

        public static explicit operator UbsDTO(Ubs d)
        {
            return new UbsDTO(d.Nome, d.Endereco, d.AvaliacaoDescritivo);
        }
    }
}