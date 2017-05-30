using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using TheWall.Models;
using Microsoft.Extensions.Options;

namespace TheWall.Factory{
    public class PostFactory : IFactory<Post>{
        private readonly IOptions<MySqlOptions> mysqlConfig;
        public PostFactory(IOptions<MySqlOptions> conf){
            mysqlConfig = conf;
        }
        internal IDbConnection Connection{
            get{
                return new MySqlConnection(mysqlConfig.Value.ConnectionString);
            }
        }
        public void AddPost(Post item){
            using (IDbConnection dbConnection = Connection) {
                string query = "INSERT INTO WallPost (content,created_at,updated_at, User_id) VALUES (@content, NOW(), NOW(), @User_id);"; 
                dbConnection.Open();
                dbConnection.Execute(query, item);
           }
        } 
        public void AddComment(Comment item, User user, Post post){
            using (IDbConnection dbConnection = Connection) {
                string query = "";
            }
        }
        public IEnumerable<Post> FindAllPosts(){
            using (IDbConnection dbConnection = Connection) {
                // string query = "SELECT * FROM WallPost";
                // string query ="SELECT WallPost.content, concat(User.firstname,' ',User.lastname) AS name, WallPost.created_at AS created_at, WallPost.id as id, FROM WallPost JOIN User on User.id = WallPost.User_id WHERE User.id = WallPost.User_id JOIN WallComment ON WallPost.WallComments_id WHERE WallComment_id = WallPost.Comments_id ORDER BY WallPost.created at Desc";
                // string query = "SELECT WallPost.content AS message, concat(User.firstname,' ',User.lastname) AS name, WallPost.created_at AS created_at, WallPost.id AS p_id WallPost.User_id as u_id, FROM WallPost LEFT JOIN User ON User.id = WallPost.User_id ORDER BY WallPost.created_at DESC;";
                dbConnection.Open();
                // return dbConnection.Query<Post, User, Post>(query, (post,user)=>{
                //     post.User_id = user; return post;
                // });
                return dbConnection.Query<Post>("SELECT * FROM WallPost");
            }
        } 
        public IEnumerable<Comment> FindAllComments(){
            using (IDbConnection dbConnection = Connection) {
                string query ="SELECT WallComments.content AS comment, concat(User.firstname, ' ',User.lastname) AS name, WallComments.created_at AS created_at, WallComments.id AS com_id, WallPost.id as p_id, User.id as u_id FROM WallComments LEFT JOIN WallPost ON WallPost.id = WallComments.WallPost_id JOIN User ON User.id = WallComments.User_id;";
                dbConnection.Open();  
                return dbConnection.Query<Comment>(query);
        }
    }
    }
    public class LoginFactory : IFactory<User>
    {
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
