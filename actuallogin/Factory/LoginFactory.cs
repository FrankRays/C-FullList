using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using actuallogin.Models;
using Microsoft.Extensions.Options;

namespace actuallogin.Factory{
    public class LoginFactory : IFactory<User>
    {
        private string connectionString;
        private readonly IOptions<MySqlOptions> mysqlConfig;
        public LoginFactory(IOptions<MySqlOptions> conf)
        {
            mysqlConfig = conf;
        }
        internal IDbConnection Connection{
            get {
                return new MySqlConnection(mysqlConfig.Value.ConnectionString);
            }
        }
        public void Add(User item){
            using (IDbConnection dbConnection = Connection) {
                string query = "INSERT INTO User (firstname,lastname,email,password,created_at,updated_at) VALUES (@firstname,@lastname,@email,@password, NOW(), NOW())"; 
                dbConnection.Open();
                dbConnection.Execute(query, item);
           }
        }
        public IEnumerable<User> FindAll(){
            using (IDbConnection dbConnection = Connection) {
                dbConnection.Open();
                return dbConnection.Query<User>("SELECT * FROM quotes");
            }
        }
        public User FindByID(int id){
            using (IDbConnection dbConnection = Connection){
                dbConnection.Open();
                return dbConnection.Query<User>("SELECT * FROM User WHERE id = @Id", new { Id = id}).FirstOrDefault();
            }
        }
        public User FindByEmail(string email){
            using (IDbConnection dbConnection = Connection){
                dbConnection.Open();
                return dbConnection.Query<User>("SELECT * FROM User WHERE email = @Email", new { Email = email}).FirstOrDefault();
        }
    }
    }
}
