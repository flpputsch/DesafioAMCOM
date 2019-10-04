using AMcom.Teste.DAL.Interface;
using AMcom.Teste.DAL.Model;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using GeoCoordinatePortable;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AMcom.Teste.DAL.Repository
{
    public class UbsRepository : IUbsRepository
    {
        private readonly IConfiguration _config;

        public UbsRepository(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Dados das Ubs
        /// </summary>
        /// <param name="coordenadaBuscada">Coordenadas</param>
        /// <param name="quantidadeItens">Quantidade de itens</param>
        /// <returns>Ubs</returns>
        public List<Ubs> Buscar(GeoCoordinate coordenadaBuscada, int quantidadeItens)
        {
            return BuscarDados()
                 .OrderBy(x => new GeoCoordinate((double)x.Latitude, (double)x.Longitude)
                     .GetDistanceTo(coordenadaBuscada))
                 .ThenByDescending(x => (int)x.AvalicaoNota)
                 .Take(quantidadeItens)
                 .ToList();
        }

        /// <summary>
        /// Busca o CSV e converte na entidade
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Ubs> BuscarDados()
        {
            //Busca e válida o caminho do csv
            var caminho = BuscarCaminhoCsv();
            List<Ubs> dados;

            //Abre o reader e lê os dados
            using (var reader = new StreamReader(caminho, Encoding.UTF8))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.PrepareHeaderForMatch = (header, index) => header.ToLower();
                csv.Configuration.Delimiter = ",";
                csv.Configuration.RegisterClassMap<UbsMap>();
                dados = csv.GetRecords<Ubs>().ToList();
            }

            return dados;
        }

        /// <summary>
        /// Busca o válida o caminho do csv
        /// </summary>
        /// <returns></returns>
        private string BuscarCaminhoCsv()
        {
            //Busca o csv das configurações
            var pathCsv = _config?.GetSection("Configuracoes")?.GetSection("PathCsvData")?.Value;

            if (string.IsNullOrEmpty(pathCsv))
            {
                throw new ConfigurationException("Caminho do arquivo não informado");
            }

            if (!File.Exists(pathCsv))
            {
                throw new FileNotFoundException("Arquivo de dados não encontrado!");
            }

            return pathCsv;
        }

        /// <summary>
        /// Conversor de célula do csv para objeto
        /// </summary>
        private class ConvertToDouble : DefaultTypeConverter
        {
            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                return double.Parse(text.Replace(".", ","));
            }
        }

        /// <summary>
        /// Conversor de célula do csv para objeto
        /// </summary>
        private class StringClear : DefaultTypeConverter
        {
            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                return text.Replace("  ", " ");
            }
        }

        /// <summary>
        /// Map da entidade para o CSV
        /// </summary>
        private sealed class UbsMap : ClassMap<Ubs>
        {
            public UbsMap()
            {
                Map(m => m.Latitude).Name("vlr_latitude").TypeConverter<ConvertToDouble>();
                Map(m => m.Longitude).Name("vlr_longitude").TypeConverter<ConvertToDouble>();
                Map(m => m.Nome).Name("nom_estab").TypeConverter<StringClear>();
                Map(m => m.Endereco).Name("dsc_endereco").TypeConverter<StringClear>();
                Map(m => m.Bairro).Name("dsc_bairro").TypeConverter<StringClear>();
                Map(m => m.Cidade).Name("dsc_cidade").TypeConverter<StringClear>();
                Map(m => m.AvaliacaoDescritivo).Name("dsc_estrut_fisic_ambiencia").TypeConverter<StringClear>();
            }
        }
    }
}