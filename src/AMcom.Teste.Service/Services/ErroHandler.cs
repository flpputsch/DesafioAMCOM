using System;
using System.Collections.Generic;
using System.Linq;
using AMcom.Teste.Service.Interfaces;

namespace AMcom.Teste.Service.Services
{
    public class ErroHandler : IErroHandler
    {
        private List<string> Erros { get; set; }

        public bool TemErro() => Erros.Any();

        private void Adicionar(string mensagem)
        {
            Erros.Add(mensagem);
        }

        public ErroHandler()
        {
            Erros = new List<string>();
        }

        public void AdicionarErros(ICollection<Exception> erros)
        {
            foreach (var erro in erros)
            {
                AdicionarErro(erro);
            }
        }

        public void AdicionarErros(ICollection<string> erros)
        {
            foreach (var erro in erros)
            {
                AdicionarErro(erro);
            }
        }

        public void AdicionarErro(Exception erro)
        {
            Adicionar(erro.Message);
        }

        public void AdicionarErro(string erro)
        {
            Adicionar(erro);
        }

        public List<string> BuscarErros()
        {
            return Erros;
        }
    }
}