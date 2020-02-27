using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Net;

namespace ApiConsumidor.Tests
{
    public class BaseTest : IDisposable
    {
        #region "propriedades"
        protected readonly string ServiceBaseUrl;
        public ComunicacaoServidorHelper ClienteHttp;
        private readonly string usuarioAutenticacao;
        private readonly string senhaAutenticacao;
        #endregion
        public BaseTest()
        {
            ServiceBaseUrl = "https://servicos.procon.sp.gov.br";
            usuarioAutenticacao = "userapi%40teste.com";
            senhaAutenticacao = "123456";
            ClienteHttp = ComunicacaoServidorHelper.CriarCliente(ServiceBaseUrl);
        }

        public RespostaServico<string> Act(string url, HttpMethod verboHttp, Dictionary<string,string> headers = null, string body = "")
        {
           return ClienteHttp.Enviar(url,verboHttp,headers,body);
        }

        public string ObterTokenAutorizacao()
        {
            ClienteHttp.RedefinirCabecalhos();
            var url = String.Concat(ServiceBaseUrl,"/api/seguranca/token");
            var headers = new Dictionary<string,string>()
            {
                {HttpRequestHeader.ContentType.ToString(),"application/x-www-form-urlencoded"}
            };
            var body = String.Concat("username=",usuarioAutenticacao,"&password=",senhaAutenticacao,"&grant_type=password");
            var resposta = ClienteHttp.ObterTokenAutenticacao(url,HttpMethod.Post,headers,body);
            
            if (resposta.CodigoRetorno == 200)
                return resposta.Resultado.access_token;

            throw new Exception("Token não obtido. Codigo Retorno" + resposta.CodigoRetorno);

        }

        public void Dispose()
        {
           ClienteHttp.Dispose();
        }
    }
}