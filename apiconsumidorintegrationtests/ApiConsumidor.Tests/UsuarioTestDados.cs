using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace ApiConsumidor.Tests
{
    public class Login
        {
           public string Usuario{get;set;}
           public string Senha{get;set;}
        }
    
    public class Autenticacao
    {
        public string access_token{get;set;}
        public string token_type{get;set;}

        public string IdUsuarioInternet{get;set;}
    }

    public static class UsuarioTestDados 
    {
        

        public static IEnumerable<object[]> FabricarCredenciaisCorretas =>
        new List<object[]>
        {
            new object[] {"cenario 001",200, new Login(){ Usuario="userapi@teste.com", Senha="123456"}}
            //new object[] {"cenario 002",200, new Login(){ Usuario="userapi@teste.com", Senha="123456"}}
        };

        public static IEnumerable<object[]> FabricarUsuarioQuePossuiConsumidor() =>
        new List<object[]>
        {
            new object[] {"cenario 011",200, 231272}
        };

        public static IEnumerable<object[]> FabricarUsuarioQuePossuiFichas() =>
        new List<object[]>
        {
            new object[] {"cenario 021",200, 231272}
        };
    }
}