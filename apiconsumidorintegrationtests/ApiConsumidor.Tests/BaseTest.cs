using System;
using Xunit;

namespace ApiConsumidor.Tests
{
    public class BaseTest
    {
        #region "propriedades"
        protected readonly string ServiceBaseUrl;
        private ComunicacaoServidorHelper _cliente;
        #endregion
        public BaseTest()
        {
            ServiceBaseUrl = "http://uol.com.br";
            _cliente = ComunicacaoServidorHelper.CriarCliente(ServiceBaseUrl);    
        }

        public RespostaServico Act(string url, string verboHttp)
        {
           var response = _cliente.Cliente.GetAsync(url);
           return new RespostaServico() { CodigoRetorno = (int)response.Result.StatusCode };
        }

       

    }
}