using System;
using Xunit;
using System.Net.Http;


namespace ApiConsumidor.Tests
{
    
    public class ComunicacaoServidorHelper
    {
        public HttpClient Cliente;

        protected ComunicacaoServidorHelper(HttpClient cliente)
        {
            Cliente = cliente;
        }
        public static ComunicacaoServidorHelper CriarCliente(string url)
        {
            var cliente = new HttpClient { BaseAddress = new Uri(url) };
            //MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
            return new ComunicacaoServidorHelper(cliente);
        }

    }

    public class RespostaServico
    {
        public int CodigoRetorno;
        public string Mensagem;

    }
}