using System;
using System.Net.Http;
using System.Text;
using System.Net.Mime;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace ApiConsumidor.Tests
{
    public class BaseTest
    {
        #region "propriedades"
        protected readonly string ServiceBaseUrl;
        public ComunicacaoServidorHelper ClienteHttp;
        #endregion
        public BaseTest()
        {
            ServiceBaseUrl = "http://uol.com.br";
            ClienteHttp = ComunicacaoServidorHelper.CriarCliente(ServiceBaseUrl);    
        }

        public RespostaServico Act(string url, HttpMethod verboHttp, Dictionary<string,string> headers = null, string body = "")
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
           
           var response = ClienteHttp.Cliente.SendAsync(request);
           return new RespostaServico() { CodigoRetorno = (int)response.Result.StatusCode };
        }
       

    }
}