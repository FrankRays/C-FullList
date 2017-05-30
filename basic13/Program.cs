using System;

namespace ConsoleApplication
{
    public class Program
    {
        public static void printRange(){
            for (int i = 1; i <= 255; i++){
                Console.WriteLine(i);
            }
        }
        public static void printOdd(){
            for (int i = 1; i<= 255; i++){
                if (i % 2 != 0){
                    Console.WriteLine(i);
                }
            }
        }
        public static void printSum(){
            int sum = 0;
            for (int i = 0; i <= 255; i++){
                Console.WriteLine(i);
                sum = sum + i;
                Console.WriteLine("New total is " + sum);
            }
        }
        public static void iterateArray(){
            int[] x;
            x = new int[] {1,3,5,7,9,13};
            foreach (int item in x){
                Console.WriteLine(item);
            }
        }
        public static int findMax(int[] array ){
            int max;
            max = array[0];
            foreach(int item in array){
                if (item > max){
                    max = item;
                }
            }
            return max;
        }
        public static int getAverage(int[] array){
            int sum = 0;
            foreach(int item in array){
                sum = sum + item;
            }
            int avg = sum / array.Length;
            return avg;
        }
        public static void oddArray(){
            int[] oddarray = new int[128];
            for(int i = 0; i<=127; i++){
                oddarray[i] = ((i*2)+1);
                Console.WriteLine(oddarray[i]);
            }
        }
        public static int greaterThanY(int y, int[] array){
            int count = 0;
            foreach(int item in array){
                if(item > y){
                    count++;
                }
            }
            return count;
        }
        public static int[] squareSelf(int[] array){
            for(int i = 0; i < array.Length; i++){
                array[i] = array[i]*array[i];
            }
            return array;
        }
        public static int[] removeNeg(int[] array){
            for(int i = 0; i < array.Length;i++){
                if(array[i] < 0){
                    array[i] = 0;
                }
            }
            return array;
        }
        public static int[] minMaxAvg(int[] x){
            int max = x[0];
            int min = x[0];
            int sum = 0;
            int[] output = new int[3];
            foreach(int item in x){
                if(item > max){
                    max = item;
                }
                else if(item < min){
                    min = item;
                }
                sum = sum + item;
            }
            output[0] = min;
            output[1] = max;
            output[2] = sum / x.Length;
            return output;
        }
        public static int[] shiftArray(int[] x){
            for(int i = 0; i < x.Length; i++){
                if(i != (x.Length - 1)){
                    x[i] = x[i+1];
                } else {
                    x[i] = 0;
                }
            }
            return x;
        }
        public static object[] numToString(object[] array){
            for(int i = 0; i < array.Length; i++){
               if((int)array[i] < 0){
                   array[i] = "Dojo";
               }
            }
            return array;
        }
        public static void Main(string[] args)
        {
            
            
        }
    }
}
