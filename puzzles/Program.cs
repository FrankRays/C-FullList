using System;

namespace ConsoleApplication
{
    public class Program
    {
        public static int[] randomArray(){
            Random rnd = new Random();
            int[] array = new int[10];
            for(int i = 0; i < 10; i++){
                array[i] = rnd.Next(5,26);
            }
            return array;
        }
        public static string TossCoin(){
            Console.WriteLine("Tossing a Coin!");
            Random rnd = new Random();
            int result = rnd.Next(0,101);
            Console.WriteLine(result);
            if(result < 50){
                Console.WriteLine("Heads!");
                return "Heads";
            } else {
                Console.WriteLine("Tails!");
                return "Tails";
            }
        }
        public static Double tossMultiCoins(int num){
            double heads = 0;
            double tails = 0;
            string[] results = new string[num];
            for(int i = 0; i <= num-1; i++){
                results[i] = TossCoin();
            }
            foreach(string item in results){
                if(item == "Heads"){
                    heads++;
                }else{
                    tails++;
                }
            }

            Console.WriteLine(heads);
            Double avg = heads / num;
            Console.WriteLine(avg);
            return avg;
        }
        public static string[] names(){
            string[] names = {"Todd","Tiffany","Charlie","Geneva","Sydney"};
            Random rnd = new Random();
            int num = rnd.Next(1,6);
            int count = 0;
            string[] newNames = new string[5];
            while(count < 5){
                newNames[count] = names[num];
                count++;
                num++;
                if(num == 5){
                    num = 0;
                }
            }
            foreach(string item in newNames){
                Console.WriteLine(item);
            }
            string[] lengthNames = new string[names.Length-1];
            count = 0;
            foreach(string item in names){
                if(item.Length > 5){
                    lengthNames[count] = item;
                    count++;
                }
            }
            return lengthNames;
        }
        public static void Main(string[] args)
        {
            // int[] array = randomArray();
            // int min = 25;
            // int max = 0;
            // int sum = 0;
            // foreach(int item in array){
            //     if(item > max){
            //         max = item;
            //     }
            //     else if(item < min){
            //         min = item;
            //     }
            //     sum = sum + item;
            // }
            // Console.WriteLine("Max is " + max);
            // Console.WriteLine("Min is " + min);
            // Console.WriteLine("Total Sum is " + sum);

            // Double ratio = tossMultiCoins(4);
            // Console.WriteLine("Total heads ratio is : " + ratio);

            string[] loggednames = names();
            foreach(string item in loggednames){
                Console.WriteLine("Names over 5 characters - " +item);
            }
        }
    }
}
