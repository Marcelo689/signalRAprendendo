using System;

namespace JsonManipulatorFolder
{
    public static class Formatador
    {
        public static Dictionary<string, object> PegaPropriedades(string texto = "{ nome: 'nome', idade: 25 }")
        {
            var primeiraChave = texto.IndexOf("{")+ 1;
            var primeiraChaveFechamento = texto.IndexOf("}")-1;
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
            TModel objeto = new TModel();
            var propriedades = objeto.GetType().GetProperties();    

            var teste = Formatador.PegaPropriedades();
            
            var dicChaves = teste.Keys;
            var chaves = dicChaves.ToList();
            List<object> valores = teste.Values.ToList();

            for( var i =0; i < valores.Count; i++)
            {
                var chave = chaves[i].Replace("'", "").Trim();
                object valor = valores[i];

                var propriedade = objeto.GetType().GetProperty(chave);

                if(propriedade.GetType() == typeof(string) )
                {
                    var texto = valor.ToString().Replace("'", "").Trim();
                    propriedade.SetValue(objeto, texto);
                }

                if(propriedade.GetType().Name == typeof(int).Name)
                {
                    int num  =  Convert.ToInt32(valor);
                    propriedade.SetValue(objeto, num);
                }
            }
            return objeto;
        }
    }
}