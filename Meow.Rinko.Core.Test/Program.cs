

namespace Meow.Rinko.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var k = new Core.Gets.News().Data;
            System.Console.WriteLine(k);
        }
    }
}
