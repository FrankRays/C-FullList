using System;
using System.Collections.Generic;
using Nancy;
using DbConnection;
using CryptoHelper;
using System.Text.RegularExpressions;

namespace Nancylogin{
    
    public class NancyloginMod : NancyModule{
        
        
        public NancyloginMod(){
            Get("/", arg =>{
                return View["index.html"];
            });
            Get("/reg", arg => {
                Console.WriteLine((string)ViewBag.passerror);
               return View["reg"];
            });
            Post("/login", args =>{
                
                ViewBag.passerror = false;
                string email = Request.Form.email;
                string query = "SELECT * FROM nancyuser WHERE email = {'email'}";
                List<Dictionary<string,object>> users = DbConnector.ExecuteQuery(query);
                if(users == null){
                    ViewBag.usererror = true;
                    return Response.AsRedirect("/");
                }
                Dictionary<string,object> user = users[0];
                string userpass = Request.Form.password;
                string hpass = (string)user["password"];
                bool isPasswordMatch = Crypto.VerifyHashedPassword(hpass, userpass);
                if(isPasswordMatch == false){
                    ViewBag.passerror = true;
                    return Response.AsRedirect("/");
                }
                Request.Session["id"] = user["id"];
                return Response.AsRedirect("/success");
            });
            Get("/success", arg => {
                if(Request.Session["id"] == null){
                    return Response.AsRedirect("/");
                }
                int id = (int)Request.Session["id"];
                string query = $"SELECT * FROM nancyuser WHERE id = '{id}'";
                List<Dictionary<string,object>> users = DbConnector.ExecuteQuery(query);
                var user = users[0];
                return View["login",user];
            });
            Post("/register", args =>{
                string pattern = "[a-z]{2,20}@[a-z]{3,10}[.](com|net|org)";
                Regex reg = new Regex(pattern);
                if(Request.Form.password != Request.Form.password2){
                    ViewBag.passerror = true;
                    return Response.AsRedirect("/reg");
                }
                if (Request.Form.fname.Count() < 2 || Request.Form.lname.Count() < 2){
                    ViewBag.nameerror = true;
                    return Response.AsRedirect("/reg");
                }
                if (Request.Form.password.Count() < 8){
                    ViewBag.passcounterror = true;
                    return Response.AsRedirect("/reg");
                }
                if(!reg.IsMatch(Request.Form.email)){
                    ViewBag.emailerror = true;
                    return Response.AsRedirect("/reg");
                }
                string fname = Request.Form.fname;
                string lname = Request.Form.lname;
                string email = Request.Form.email;
                string password = Crypto.HashPassword(Request.Form.pass);
                string query = $"INSERT INTO nancyuser (fname, lname, email, pass, created_at) VALUES ('{fname}','{lname}','{email}','{password}',NOW())";
                DbConnector.ExecuteQuery(query);
                query = $"SELECT id FROM nancyuser WHERE email = '{email}'";
                var users = DbConnector.ExecuteQuery(query);
                Request.Session["id"] = users[0]["id"];
                return Response.AsRedirect("/success"); 
            });

        }
    }
}
