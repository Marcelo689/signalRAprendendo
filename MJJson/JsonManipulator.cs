using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJJson
{
    public static class Formatador
    {
        public static Dictionary<string, object> PegaPropriedades(string texto = "{ nome: 'nome', idade: 25 }")
        {
            var primeiraChave = texto.IndexOf("{");
            var primeiraChaveFechamento = texto.IndexOf("}");
            var parteRecortada = texto.Substring(primeiraChave, primeiraChaveFechamento);

            var propriedades = parteRecortada.Split(",");

            var dicionario = PegaValores(propriedades);

            return dicionario;
        }

        private static Dictionary<string, object> PegaValores(string[] propriedades)
        {
            var dicionario = new Dictionary<string, object>();

            foreach (var linhaPropriedade in propriedades)
            {
                var chaveValor = linhaPropriedade.Split(':');
                var chave = chaveValor[0];
                var valor = chaveValor[1];

                dicionario.Add(chave, valor);
            }

            return dicionario;
        }
    }
    public static class JsonManipulator
    {

        public static TModel TextToObject<TModel>(TModel model, string textJson) where TModel : class, new()
        {
            var objeto = new TModel();

            var teste = Formatador.PegaPropriedades();
            return objeto;
        }
    }
}