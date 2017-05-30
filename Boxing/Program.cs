using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int sum = 0;
            List<object> list = new List<object>();
            list.Add(7);
            list.Add(28);
            list.Add(-1);
            list.Add(false);
            foreach (var item in list){
                if (item is int){
                    Console.WriteLine("This is an int of " + item);
                    sum = sum + (int)item;
                }
                if (item is bool){
                    Console.WriteLine("The correct answer is " + item);
                }
            }
            Console.WriteLine(sum);

        }
    }
}
