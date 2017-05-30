using System;
using System.Collections.Generic;
using System.Data.Common;
using MySql.Data.MySqlClient;


namespace ConsoleWithDb
{
    public class DbConnector
    {
        public static List<Dictionary<string, object>> ExecuteQuery(string queryString)
        {
            string server = "localhost";
            string db = "NetConnect";
            string port = "8889";
            string user = "root";
            string pass = "root";
            using(var connection = new MySqlConnection(
                $"Server={server};Database={db};Port={port};UserID={user};Password={pass};"))
            {
                connection.Open();
                using(var command = new MySqlCommand(queryString, connection))
                {
                    var result = new List<Dictionary<string, object>>();
                    using(DbDataReader rdr = command.ExecuteReader())
                    {
                        while(rdr.Read())
                        {
                            var dict = new Dictionary<string, object>();
                            for( int i = 0; i < rdr.FieldCount; i++ ) {
                                dict.Add(rdr.GetName(i), rdr.GetValue(i));
                            }
                            result.Add(dict);
                        }
                    }
                    return result;
                }
            }
        }
    }
    

    public class Program
    {
        public static void Create(){
            Console.Clear();
            Console.WriteLine("Please input user's first name.");
            string newname = Console.ReadLine();
            Console.WriteLine("You entered \"{}\".",newname);
            Console.WriteLine("Please input user's last name.");
            string newlastname = Console.ReadLine();
            Console.WriteLine("You entered \"{}\".",newlastname);
            Console.WriteLine("Please input user's favorite number.");
            string newfavnum = Console.ReadLine();
            Console.WriteLine("You entered \"{}\".",newfavnum);
            Console.WriteLine("Finally, please input favorite color.");
            string newfavcolor = Console.ReadLine();
            Console.WriteLine("You entered \"{}\".",newfavcolor);
            Console.WriteLine("Great! Please hit (enter) to continue.");
            string query = "INSERT INTO User (firstname,lastname,favnum,favcolor,created_at,updated_at) VALUES (\""+newname+"\",\""+newlastname+"\",\""+newfavnum+"\",\""+newfavcolor+"\",NOW(),NOW())";
            DbConnector.ExecuteQuery(query);
            Console.Clear();
            Console.WriteLine("Great! Looping back to the start.");
        }
        public static void Delete(){
            Console.WriteLine("Please enter the first name of the user you wish to delete.");
            string searchname = Console.ReadLine();
            string dquery = "SELECT id firstname lastname FROM users WHERE firstname = \"" + searchname+"\";";
            List<Dictionary<string, object>> deleteResults = DbConnector.ExecuteQuery(dquery);
            var specDelResults = deleteResults[0];
            bool exit = false;
            while(exit != true){
            Console.WriteLine("Do you wish to delete {} {}? Y/N ", specDelResults["firstname"],specDelResults["lastname"] );
            string deleteChoice = Console.ReadLine();
            switch(deleteChoice){
                case "Y":
                    string deletequery = "DELETE FROM User WHERE id ="+specDelResults["id"]+";";
                    DbConnector.ExecuteQuery(deletequery);
                    break;
                case "N":
                    Console.Clear();
                    Console.WriteLine("Looping back to top menu.");
                    return;
                    
                default:
                    Console.WriteLine("Please select Y/N");
                    break;
                }
            }
        } 
        static void Update(){
            Console.WriteLine("Please enter the first name of the user you wish to update.");
            string upsearchname = Console.ReadLine();
                    string upquery = "SELECT id firstname lastname favcolor favnum FROM users WHERE firstname = \"" + upsearchname+"\";";
                    List<Dictionary<string, object>> updateResults = DbConnector.ExecuteQuery(upquery);
                    var specUpResults = updateResults[0];
                    string updatedFirstname = specUpResults["firstname"] as String;
                    string updatedLastname = specUpResults["lastname"] as String;
                    string updatedfanum = specUpResults["favnum"] as String;
                    string updatedfacolor = specUpResults["favcolor"] as String;
                    Console.Clear();
                    Console.WriteLine("Alright.");
                    bool fnameProgress = false;
                    bool exit = false;
                    while(exit != true){
                    while(fnameProgress == false){
                    Console.WriteLine("The first name is currently "+ specUpResults["firstname"]);
                    Console.WriteLine("Would you like to update this? Y/N");
                    string fnamechoice = Console.ReadLine();
                    switch(fnamechoice){
                        case "Y":
                            Console.WriteLine("Please enter new first name.");
                            updatedFirstname = Console.ReadLine();
                            fnameProgress = true;
                            break;
                        case "N":
                            Console.Clear();
                            Console.WriteLine("Moving to next item.");
                            
                            fnameProgress = true;
                            break;
                        default:
                            Console.WriteLine("Please select Y/N");
                            break;
                    }
                    if(exit == true){break;}
                    }
                    bool lnameprogress = false;
                    while(lnameprogress == false){
                        Console.WriteLine("The last name is currently "+ specUpResults["lastname"]);
                        Console.WriteLine("Would you like to update this? Y/N");
                    string lnamechoice = Console.ReadLine();
                    switch(lnamechoice){
                        case "Y":
                            Console.WriteLine("Please enter new last name.");
                            updatedLastname = Console.ReadLine();
                            lnameprogress = true;
                            break;
                        case "N":
                            Console.Clear();
                            Console.WriteLine("Moving onto next item.");
                            lnameprogress = true;
                            break;
                        default:
                            Console.WriteLine("Please select Y/N");
                            break;
                    }    
                }
                bool fanumprogress = false;
                    while(fanumprogress == false){
                        Console.WriteLine("The last name is currently "+ specUpResults["favnum"]);
                        Console.WriteLine("Would you like to update this? Y/N");
                    string fanumchoice = Console.ReadLine();
                    switch(fanumchoice){
                        case "Y":
                            Console.WriteLine("Please enter new favorite number.");
                            updatedfanum = Console.ReadLine();
                            fanumprogress = true;
                            break;
                        case "N":
                            Console.Clear();
                            Console.WriteLine("Moving onto next item.");
                            fanumprogress = true;
                            break;
                        default:
                            Console.WriteLine("Please select Y/N");
                            break;
                    }    
                }
                 bool facolorprogress = false;
                    while(facolorprogress == false){
                        Console.WriteLine("The last name is currently "+ specUpResults["facolorprogress"]);
                        Console.WriteLine("Would you like to update this? Y/N");
                    string facolorchoice = Console.ReadLine();
                    switch(facolorchoice){
                        case "Y":
                            Console.WriteLine("Please enter new favorite number.");
                            updatedfacolor = Console.ReadLine();
                            facolorprogress = true;
                            break;
                        case "N":
                            Console.Clear();
                            Console.WriteLine("Moving onto next item.");
                            facolorprogress = true;
                            break;
                        default:
                            Console.WriteLine("Please select Y/N");
                            break;
                    }    
                }
                Console.Clear();
                Console.WriteLine("Alright. Your new settings are as follows : ");
                Console.WriteLine();
            }   
        }
        static void ReadAll(){
                List<Dictionary<string, object>> results = DbConnector.ExecuteQuery("SELECT all FROM users");
                foreach(var item in results){
                    Console.WriteLine("{}", item["id"]);
                    Console.WriteLine("First name: {}", item["firstname"]);
                    Console.WriteLine("Last name: {}", item["lastname"]);
                    Console.WriteLine("Favorite Number: {}", item["favnum"]);
                    Console.WriteLine("Favorite Color: {}", item["favcolor"]);
                    Console.WriteLine("------------------------");
                }
            }

        public static void Main(string[] args)
        {
            bool running = true;
            while(running == true){
            
            Console.WriteLine("Here is the database so far!");
            ReadAll();
            Console.WriteLine("Please select an option!");
            Console.WriteLine("(Insert) new data into database.");
            Console.WriteLine("(Delete) item from database.");
            Console.WriteLine("(Update) item from database.");
            Console.WriteLine("Please input query or type (quit) to end!");
            string InputLine = Console.ReadLine();
            switch(InputLine){
                case "Insert":
                    Create();
                    break;
                case "Delete":
                    Delete();
                    break;
                case "Update":
                    Update();
                    break;
                case "quit":
                    Environment.Exit(0);
                    return;
                default:
                    Console.WriteLine("Please select a listed option.");
                    break;
                    
        }
        }
    }
}
}
