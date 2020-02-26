using System;
using Xunit;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text;

namespace ApiConsumidor.Tests
{
    
    public class ComunicacaoServidorHelper
    {
        private HttpClient Cliente;

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

        public ComunicacaoServidorHelper RedefinirCabecalhos()
        {
            this.Cliente.DefaultRequestHeaders.Accept.Clear();
            this.Cliente.DefaultRequestHeaders.CacheControl  = new CacheControlHeaderValue(){NoCache = true};
            return this;
        }

       public RespostaServico Enviar(string url, HttpMethod verboHttp, Dictionary<string,string> headers = null, string body = "")
       {
           var response = EnviarRequestHttp(url,verboHttp,headers,body);
           return new RespostaServico {CodigoRetorno = (int) response.StatusCode};
       }

       public RespostaServico ObterTokenAutenticacao(string url, HttpMethod verboHttp, Dictionary<string,string> headers = null, string body = "")
       {
            var response = EnviarRequestHttp(url,verboHttp,headers,body);
            var respostaServico = new RespostaServico();
            respostaServico.CodigoRetorno = (int) response.StatusCode;
            if (response != null && response.Headers != null && response.Headers.Contains("Token") && response.Headers.GetValues("Token") != null)
            {
                respostaServico.Resultado = ((string[])(response.Headers.GetValues("Token")))[0];
            }
            return respostaServico;
       }

       private HttpResponseMessage EnviarRequestHttp(string url, HttpMethod verboHttp, Dictionary<string,string> headers = null, string body = "") 
       {
        var request = new HttpRequestMessage();
           request.Method = verboHttp;
           request.RequestUri = new Uri(url);
           if (headers !=null)
           {
             foreach(var key in headers.Keys)
             {
                 request.Headers.Add(key, headers[key]);
             }
           }
           if (!String.IsNullOrWhiteSpace(body))
            request.Content = new StringContent(body, Encoding.UTF8);
           
           return this.Cliente.SendAsync(request).Result;        
       }

    }
    public class RespostaServico
    {
        public int CodigoRetorno;
        public string Resultado;

    }
}