using System;
using Xunit;


namespace ApiConsumidor.Tests
{
    
    public class UsuarioTest : BaseTest
    {
        const int CODIGO_SUCESSO = 200;
        [Theory]
        [MemberData(nameof(UsuarioTestDados.FabricarCredenciaisCorretas), MemberType = typeof(UsuarioTestDados))]
        public void QuandoInformoCredenciaisCorretasEntaoRetornoOK(string cenario, int result, Login parametros)
        {
            //ACT
            var resposta = base.Act("http://uol.com.br","GET");
            var mensagemResultado = String.Format("Acesso incorreto cenario:{0} retorno:{1}",cenario,resposta.CodigoRetorno);
            Assert.True(result == resposta.CodigoRetorno,mensagemResultado);
        }
    }
}
