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
            var dados = new Dictionary<string, UbsAvaliacao>
            {
                {"Desempenho mediano ou um pouco abaixo da média", UbsAvaliacao.Ruim},
                {"Desempenho acima da média", UbsAvaliacao.Bom},
                {"Desempenho muito acima da média", UbsAvaliacao.Excelente}
            };

            return dados;
        }
    }
}