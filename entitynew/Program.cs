using System;
using System.Linq;
using ConsoleApplication.Models;
using System.Collections.Generic;


namespace ConsoleApplication
{
    public class Program
    {
        public static void Main()
        {
            using(var db = new YourContext()){
                
                List<Person> ReturnedValues = db.User.ToList();
                Console.WriteLine("Here's the list of everyone in the database.");
                foreach(var item in ReturnedValues){
                    Console.WriteLine($"Name: {item.firstname} {item.lastname}");
                    Console.WriteLine($"Email: {item.email}");
                    Console.WriteLine("=========================================");
                }
                
                Person NewPerson = new Person{
                    firstname = "Jim",
                    lastname = "Word",
                    email = "email3@example.com",
                    password = "password1"
                };
                db.Add(NewPerson);
                db.SaveChanges();
                Person UpdatingUser = db.User.SingleOrDefault(users => users.ID == 5);
                UpdatingUser.firstname = "Michael";
                db.SaveChanges();
                Console.WriteLine($"Changed that to {UpdatingUser.firstname} {UpdatingUser.lastname}");
                ReturnedValues = db.User.ToList();
                Console.WriteLine("Hit the any key to continue");
                Console.ReadKey();
                Console.Clear();
                 foreach(var item in ReturnedValues){
                    Console.WriteLine($"Name: {item.firstname} {item.lastname}");
                    Console.WriteLine($"Email: {item.email}");
                    Console.WriteLine("=========================================");
                }
                // Person UserToDelete = db.User.SingleOrDefault(users => users.email == "email@example.com");
                // db.User.Remove(UserToDelete);
                // db.SaveChanges(); 
                } 
            }
        }
    }

