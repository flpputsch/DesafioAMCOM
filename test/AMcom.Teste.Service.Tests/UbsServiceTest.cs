using System.IO;
using System.Linq;
using AMcom.Teste.DAL.Repository;
using AMcom.Teste.Service.Services;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace AMcom.Teste.Service.Tests
{
    public class UbsServiceTest
    {
        private const string ConfiguracaoJsonValida = "appsettings.json";
        private const string ConfiguracaoJsonInvalida = "appsettings.invalida.json";
        private const string ConfiguracaoJsonErrada = "appsettings.erro.json";

        [Theory]
        [InlineData(-15.1514, -47.1546, 5)]
        [InlineData(16.5487, -25.234567, 5)]
        public void Busca_Ubs_Coordenadas_Validas(double latitude, double longitude, int quantidadeItens)
        {
            UbsService servico = ConstruirServico();

            var retornoService = servico.Buscar(latitude, longitude);

            Assert.Equal(!servico._erroHandler.TemErro() && retornoService.Any() && retornoService.Count == quantidadeItens, true);
        }

        [Theory]
        [InlineData(-1115.1514, -751546)]
        [InlineData(100.5487, -25234567)]
        public void Busca_Ubs_Coordenadas_invalidas(double latitude, double longitude)
        {
            UbsService servico = ConstruirServico();

            servico.Buscar(latitude, longitude);

            Assert.Equal(servico._erroHandler.TemErro(), true);
        }

        [Fact]
        public void Configuracao_CSV_Valida()
        {
            UbsService servico = ConstruirServico();
            var quantidadeItens = 5;
            var retornoService = servico.Buscar(1, 1);

            Assert.Equal(!servico._erroHandler.TemErro() && retornoService.Any() && retornoService.Count == quantidadeItens, true);
        }

        [Fact]
        public void Configuracao_CSV_Invalida()
        {
            UbsService servico = ConstruirServico(ConfiguracaoJsonInvalida);

            servico.Buscar(1, 1);

            Assert.Equal(servico._erroHandler.TemErro(), true);
        }

        [Fact]
        public void Configuracao_CSV_Erro()
        {
            UbsService servico = ConstruirServico(ConfiguracaoJsonErrada);

            servico.Buscar(1, 1);

            Assert.Equal(servico._erroHandler.TemErro(), true);
        }

        private UbsService ConstruirServico(string configuracaoJson = "appsettings.json")
        {
            var configuration = GenerateConfigurator(configuracaoJson);

            var errorHandler = new ErroHandler();
            var repository = new UbsRepository(configuration);

            return new UbsService(repository, errorHandler);
        }

        private IConfiguration GenerateConfigurator(string configuracaoJson)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configuracaoJson);

            return builder.Build();
        }
    }
}