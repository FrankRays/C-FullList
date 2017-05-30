using System;
using System.Collections.Generic;
using Nancy;
using DbConnection;

namespace QuotingDojo{
    
    public class QuotingDojoModule : NancyModule{
        
        
        public QuotingDojoModule(){
            Get("/", arg =>{
                return View["index"];
            });
            Get("/quotes", arg => {
                string query = "SELECT * FROM quotes ORDER BY created_at DESC";
                List<Dictionary<string, object>> quotes = DbConnector.ExecuteQuery(query);
                return View["quotes.sshtml", quotes];
            });
            Post("/quotes", args =>{
                string name = Request.Form.username;
                string quote = Request.Form.quote;
                string query = $"INSERT INTO quotes (username,quote,created_at,updated_at) VALUES ('{name}', '{quote}', NOW(), NOW())";
                DbConnector.ExecuteQuery(query);
                return Response.AsRedirect("/quotes");
            });
        }
    }
}
