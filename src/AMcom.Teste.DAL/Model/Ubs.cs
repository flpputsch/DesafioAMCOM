using CsvHelper.Configuration.Attributes;

namespace AMcom.Teste.DAL.Model
{
    public class Ubs
    {
        [Name("vlr_latitude")]
        public double Latitude { get; set; }

        [Name("vlr_longitude")]
        public double Longitude { get; set; }

        [Name("nom_estab")]
        public string Nome { get; set; }

        [Name("dsc_endereco")]
        public string Endereco { get; set; }

        [Name("dsc_bairro")]
        public string Bairro { get; set; }

        [Name("dsc_cidade")]
        public string Cidade { get; set; }

        [Name("dsc_estrut_fisic_ambiencia")]
        public string AvaliacaoDescritivo { get; set; }

        public UbsAvaliacao AvalicaoNota => UbsAvaliacaoConversor.Converter(AvaliacaoDescritivo);

        protected Ubs()
        {
        }
    }
}