using System;
using System.Net.Http;
using System.Text;
using System.Net.Mime;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Net;

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
            ObterTokenAutorizacao();    
        }

        public RespostaServico Act(string url, HttpMethod verboHttp, Dictionary<string,string> headers = null, string body = "")
        {
           return ClienteHttp.Enviar(url,verboHttp,headers,body);
        }

        public string ObterTokenAutorizacao()
        {
            ClienteHttp.RedefinirCabecalhos();
            const string url = "http://servicos.procon.sp.gov.br/api/seguranca/token";
            var headers = new Dictionary<string,string>()
            {
                {HttpRequestHeader.ContentType.ToString(),"application/x-www-form-urlencoded"}
            };
            var body = "username=userapi%40teste.com&password=123456&grant_type=password";
            var token = ClienteHttp.ObterTokenAutenticacao(url,HttpMethod.Post,headers,body).Resultado;
            Console.WriteLine("Token:" + token);
            return token;

        }

    }
}