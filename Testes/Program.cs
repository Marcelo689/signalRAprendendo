// See https://aka.ms/new-console-template for more information
using JsonManipulatorFolder;

namespace Teste
{
    class Usuario
    {
        public string nome { get; set; }
        public int idade { get; set; }
    }
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var usuario = new Usuario();

            var algo = JsonManipulator.TextToObject(usuario, "");

            var teste = algo.nome;
        }
    }

}