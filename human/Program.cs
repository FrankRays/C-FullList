using System;

namespace ConsoleApplication
{
    public class Human{
        public string name;
        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public int Dexterity { get; set; }
        public int Health { get; set; }
        public Human(string person){
            name = person;
            Strength = 3;
            Intelligence = 3;
            Dexterity = 3;
            Health = 100;
        }
        public Human(string val, int str, int intell, int dex, int hel){
            name = val;
            Strength = str;
            Intelligence = intell;
            Dexterity = dex;
            Health = hel;
        }
        public void Attack(object target){
            Human enemy = target as Human;
            if(enemy != null){
                enemy.Health -= Strength * 5;
            }
            else{
                Console.WriteLine("Invalid target type!");
            }
        }
    }
    public class Ninja : Human {
        public Ninja(string person) : base (person){
            Dexterity = 175;
        }
        public void steal(object target){
            Human enemy = target as Human;
            if(enemy != null){
                enemy.Health -= Strength * 5;
                Health += 10;
            }
            else{
                Console.WriteLine("Invalid target type!");
            }
        }
        public void get_away(){
            Health -= 15;
        }
    }
    public class Samurai : Human {
        private int Sam_Count; 
        public Samurai(string person) : base (person){
            Health = 200;
            Sam_Count++;
        }
        public void death_blow( object target){
            Human enemy = target as Human;
            if(enemy != null){
                if(enemy.Health < 50){
                    enemy.Health = 0;
                    Console.WriteLine("Death blow!");
                }
                else{
                    Console.WriteLine("Death blow failed.");
                }
            }
        }
        public void meditate(){
            Health = 200;
        }
        private void how_many(){
            Console.WriteLine($"{Sam_Count}");
        }
    }
    public class Wizard : Human {
        Random rng = new Random();
        public Wizard(string person) : base (person){
            Health = 50;
            Intelligence = 25;

        }
        public void Heal(){
            Health += (10 * Intelligence);
        }
        public void Fireball(object target){
            Human enemy = target as Human;
            if(enemy != null){
                enemy.Health -= rng.Next(20, 51);
            }
            else{
                Console.WriteLine("Invalid target type!");
            }
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            
        }
    }
}
