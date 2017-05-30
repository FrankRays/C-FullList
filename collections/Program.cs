using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] numArray = new int[10];
            for ( int i = 0; i < 10; i++)
            {
                numArray[i] = i + 1;
                Console.WriteLine(numArray[i]);
            }
            string[] nameArray;
            nameArray = new string[] {"Tim","Martin","Nikki","Sara"};
            int[] trueArray = new int[10];
            for ( int i = 0; i < 10; i++)
            {
                if ( i % 2 == 0 ){
                    trueArray[i] = 1;
                }
                else {
                    trueArray[i] = 0;
                }
            }
            int[,] multiArray = new int[10,10];
            for ( int i = 0; i <= 9; i++){
                for (int k = 0; k <= 9; k++){
                    multiArray[i,k] = ( (i+1) * (k+1) );
                }
            }

            List<Dictionary<string,string>> people = new List<Dictionary<string,string>>();

            foreach (string name in nameArray){
                Dictionary<string,string> profile = new Dictionary<string,string>();
                profile.Add("Name", name );
                profile.Add("Favorite Sport","Football");
                profile.Add("Num of Pets","3");
                profile.Add("Ice cream?","True");
                people.Add(profile);
            }
            foreach (var person in people){
                foreach (var entry in person){
                    Console.WriteLine(entry.Key + " - " + entry.Value);
                }
            }
            
            
        }
    }
}
