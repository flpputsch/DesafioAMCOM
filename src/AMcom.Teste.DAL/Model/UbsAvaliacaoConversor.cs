using System.Collections.Generic;
using System.Linq;

namespace AMcom.Teste.DAL.Model
{
    public static class UbsAvaliacaoConversor
    {
        public static UbsAvaliacao Converter(string avaliacao)
        {
            return DadosCadastrados().FirstOrDefault(x => x.Key == avaliacao).Value;
        }

        private static Dictionary<string, UbsAvaliacao> DadosCadastrados()
        {
            var dados = new Dictionary<string, UbsAvaliacao>();
            dados.Add("Teste1", UbsAvaliacao.Ruim);
            dados.Add("Desempenho mediano ou um pouco abaixo da média", UbsAvaliacao.Bom);
            dados.Add("Teste2", UbsAvaliacao.Excelente);

            return dados;
        }
    }
}