using System;

namespace GameTheGame
{
    class Program
    {
        static void Main(string[] args)
        {
            MInteger mInteger = new MInteger();
            Console.WriteLine(mInteger);
            mInteger.Append(true);
            mInteger.Append(false);
            mInteger.Append(true);
            Console.WriteLine(mInteger);
            Console.Write("Press any key to exit...");
            Console.ReadKey(true);
        }
    }
}
