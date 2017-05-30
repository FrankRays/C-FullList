using System;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            for (int i =1; 1 <= 255; i++)
            {
                Console.WriteLine(i)
            }
            for (int i = i; 1 <= 100; i++)
            {
                if ( i % 3 == 0 || i % 5 ==0 )
                {
                    if ( i % 3 != 0 && i % 5 != 0)
                    {
                        Console.WriteLine(i)
                    }
                }

            }
        }
    }
}
