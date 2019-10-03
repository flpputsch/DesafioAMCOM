using System;
using System.Collections.Generic;

namespace AMcom.Teste.Service.Interfaces
{
    public interface IErroHandler
    {
        bool TemErro();

        void AdicionarErros(ICollection<Exception> erros);

        void AdicionarErros(ICollection<string> erros);

        void AdicionarErro(Exception erro);

        void AdicionarErro(string erro);
    }
}