using System;
using Xunit;
using System.Net.Http;
using System.Collections.Generic;
using System.Net;

namespace ApiConsumidor.Tests
{
    
    public class UsuarioTest : BaseTest
    {
        [Theory]
        [MemberData(nameof(UsuarioTestDados.FabricarCredenciaisCorretas), MemberType = typeof(UsuarioTestDados))]
        public void QuandoInformoCredenciaisCorretasEntaoRetornoOK(string cenario, int result, Login parametros)
        {
            //ARRANGE
             ClienteHttp
                 .RedefinirCabecalhos();
            
            var url = String.Concat(base.ServiceBaseUrl,"/api/seguranca/token"); 
            var headers = new Dictionary<string,string>()
            {
                {HttpRequestHeader.ContentType.ToString(),"application/x-www-form-urlencoded"}
            };
            var encondingUsuarioHtml = parametros.Usuario.Replace("@","%40");
            var body = String.Concat("username=",encondingUsuarioHtml,"&password=",parametros.Senha,"&grant_type=password");
            //ACT
            var resposta = base.Act(url,HttpMethod.Get,headers,body);
            var mensagemResultado = String.Format("Acesso incorreto cenario:{0} retorno:{1}",cenario,resposta.CodigoRetorno);
            //ASSERT
            Assert.True(result == resposta.CodigoRetorno,mensagemResultado);
        }
        [Theory]
        [MemberData(nameof(UsuarioTestDados.FabricarUsuarioQuePossuiFichas), MemberType = typeof(UsuarioTestDados))]
        public void QuandoInformoUsuarioQuePossuiFichasEntaoRetornoOK(string cenario, int result, int codigoUsuario)
        {
            //ARRANGE
             ClienteHttp
                 .RedefinirCabecalhos();
            
            var url = String.Concat(base.ServiceBaseUrl,String.Format("/api/atendimento/usuarios/{0}/fichas", codigoUsuario)); 
            var token = base.ObterTokenAutorizacao();
            var headers = new Dictionary<string,string>()
            {
                {HttpRequestHeader.ContentType.ToString(),"application/x-www-form-urlencoded"},
                {HttpRequestHeader.Authorization.ToString(),String.Format("Bearer {0}",token)}
            };
            //ACT
            var resposta = base.Act(url,HttpMethod.Get,headers);
            var mensagemResultado = String.Format("Problemas retornos dados ficha cenario:{0} retorno:{1}",cenario,resposta.CodigoRetorno);
            //ASSERT
            Assert.True(result == resposta.CodigoRetorno,mensagemResultado);
        }

        [Theory]
        [MemberData(nameof(UsuarioTestDados.FabricarUsuarioQuePossuiConsumidor), MemberType = typeof(UsuarioTestDados))]
        public void QuandoInformoUsuarioQuePossuiConsumidorEntaoRetornoOK(string cenario, int result, int codigoUsuario)
        {
            //ARRANGE
             ClienteHttp
                 .RedefinirCabecalhos();
            
            var url = String.Concat(base.ServiceBaseUrl,String.Format("/api/atendimento/usuarios/{0}/consumidor", codigoUsuario)); 
            var token = base.ObterTokenAutorizacao();
            var headers = new Dictionary<string,string>()
            {
                {HttpRequestHeader.ContentType.ToString(),"application/x-www-form-urlencoded"},
                {HttpRequestHeader.Authorization.ToString(),String.Format("Bearer {0}",token)}
            };
            //ACT
            var resposta = base.Act(url,HttpMethod.Get,headers);
            var mensagemResultado = String.Format("Problemas retorno dados consumidor:{0} retorno:{1}",cenario,resposta.CodigoRetorno);
            //ASSERT
            Assert.True(result == resposta.CodigoRetorno,mensagemResultado);
        }

    }
}
