using System;
using Xunit;
using System.Net.Http;
using System.Net.Http.Headers;


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

        public ComunicacaoServidorHelper ConfigurarInicialCabecalhos()
        {
            this.Cliente.DefaultRequestHeaders.Accept.Clear();
            this.Cliente.DefaultRequestHeaders.CacheControl  = new CacheControlHeaderValue(){NoCache = true};
            return this;
        }

        public ComunicacaoServidorHelper RedefinirCabecalho(string header, string conteudo)
        {
            if (this.Cliente.DefaultRequestHeaders.Contains(header))
                this.Cliente.DefaultRequestHeaders.Remove(header);

            this.Cliente.DefaultRequestHeaders.Add(header,conteudo);
            return this;
        }

    }

    public class RespostaServico
    {
        public int CodigoRetorno;
        public string Mensagem;

    }
}