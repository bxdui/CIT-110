using System;

namespace BigLetter
{
    class BigLetter
    {

        static void Main()
        {
            BigLetter.NormalLine(2);
            Console.WriteLine("HHHHHHHH");
            BigLetter.NormalLine(3);
        }

        static void NormalLine(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("H      H");
            }
        }
    }
}