using System;
using Xunit;
using System.Net.Http;
using System.Collections.Generic;
using System.Net;

namespace ApiConsumidor.Tests
{
    
    public class UsuarioTest : BaseTest
    {
        const int CODIGO_SUCESSO = 200;
        [Theory]
        [MemberData(nameof(UsuarioTestDados.FabricarCredenciaisCorretas), MemberType = typeof(UsuarioTestDados))]
        public void QuandoInformoCredenciaisCorretasEntaoRetornoOK(string cenario, int result, Login parametros)
        {
            //ARRANGE
             ClienteHttp
                 .ConfigurarInicialCabecalhos();
            
            var headers = new Dictionary<string,string>()
            {
                {HttpRequestHeader.ContentType.ToString(),"application/x-www-form-urlencoded"}
            };
            var body = "username=userapi%40teste.com&password=123456&grant_type=password";

            //ACT
            var resposta = base.Act("http://servicos.procon.sp.gov.br/api/seguranca/token",HttpMethod.Get,headers,body);
            var mensagemResultado = String.Format("Acesso incorreto cenario:{0} retorno:{1}",cenario,resposta.CodigoRetorno);
            Assert.True(result == resposta.CodigoRetorno,mensagemResultado);
        }
    }
}
