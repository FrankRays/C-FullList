using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using newquotes.Models;

namespace newquotes.Factory{
    public class QuoteFactory : IFactory<Quote>
    {
        private string connectionString;
        public QuoteFactory()
        {
            connectionString = "server=localhost;userid=root;password=root;port=8889;database=NetConnect;SslMode=None";
        }
        internal IDbConnection Connection{
            get {
                return new MySqlConnection(connectionString);
            }
        }
        public void Add(Quote item){
            using (IDbConnection dbConnection = Connection) {
                string query = "INSERT INTO quotes (username,quote,created_at,updated_at) VALUES (@username, @quote, NOW(), NOW())"; 
                dbConnection.Open();
                dbConnection.Execute(query, item);
           }
        }
        public IEnumerable<Quote> FindAll(){
            using (IDbConnection dbConnection = Connection) {
                dbConnection.Open();
                return dbConnection.Query<Quote>("SELECT * FROM quotes");
            }
        }
        public Quote FindByID(int id){
            using (IDbConnection dbConnection = Connection){
                dbConnection.Open();
                return dbConnection.Query<Quote>("SELECT * FROM quotes WHERE id = @Id", new { Id = id}).FirstOrDefault();
            }
        }
    }
}