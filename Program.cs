using System;
namespace GameTheGame
{
    class Program
    {
        static void Main(string[] args)
        {
            MInteger a = new MInteger("100");
            Console.WriteLine(a);
            MInteger b = new MInteger("50");
            Console.WriteLine(b);
            //Console.WriteLine(b.Append(a));
            MInteger c = MInteger.Add(a, b);
            Console.WriteLine(c);

            Console.Write("Press any key to exit...");
            Console.ReadKey(true);
        }
    }
}
