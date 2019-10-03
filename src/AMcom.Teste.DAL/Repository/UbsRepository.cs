using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AMcom.Teste.DAL.Interface;
using AMcom.Teste.DAL.Model;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AMcom.Teste.DAL.Repository
{
    public class UbsRepository : IUbsRepository
    {
        private readonly IConfiguration _config;

        public UbsRepository(IConfiguration config)
        {
            _config = config;
        }

        // Implemente um método que retorne uma lista/coleção de objetos do tipo Ubs.
        // Caso necessário, crie um parâmetro para filtrar os objetos dessa coleção se a lógica não for
        // implementada na camada de serviços.
        public List<Ubs> Buscar(decimal coordenadasLatitude, decimal coordenadasLongitude, int quantidadeItens)
        {
            BuscarDados();
            throw new System.NotImplementedException();
        }

        private List<Ubs> BuscarDados()
        {
            var caminho = BuscarCaminhoCsv();
            List<Ubs> dados;

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

        private string BuscarCaminhoCsv()
        {
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

        public class ConvertToDecimal : DefaultTypeConverter
        {
            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                return decimal.Parse(text.Replace(".", ","));
            }
        }

        public sealed class UbsMap : ClassMap<Ubs>
        {
            public UbsMap()
            {
                Map(m => m.Latitude).Name("vlr_latitude").TypeConverter<ConvertToDecimal>();
                Map(m => m.Longitude).Name("vlr_longitude").TypeConverter<ConvertToDecimal>();
                Map(m => m.Estado).Name("nom_estab");
                Map(m => m.Endereco).Name("dsc_endereco");
                Map(m => m.Bairro).Name("dsc_bairro");
                Map(m => m.Cidade).Name("dsc_cidade");
                Map(m => m.AvaliacaoDescritivo).Name("dsc_estrut_fisic_ambiencia");
            }
        }
    }
}