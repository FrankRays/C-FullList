using Nancy;
using System;


namespace GreatNumberGame{
    public class NumberModule : NancyModule{
        public NumberModule(){
            
            bool guesslower = false;
            bool guesshigher = false;
            bool guessnotcorrect = true;
            bool guesscorrect = false;
            Request.Session["Randnum"] = 0;
            Get("/", args => {
                @ViewBag.guesslower = guesslower;
                @ViewBag.guesshigher = guesshigher;
                @ViewBag.guesscorrect = guesscorrect;
                @ViewBag.guessnotcorrect = guessnotcorrect;

                if((int)Session["Randnum"] == 0){
                    int RandNum = new Random().Next(1,101);
                }
                @ViewBag.number = Session["Randnum"];
                return View["NumberGame"];
            });
            Post("/guess", args =>
            {
                if(Request.Form.numguess > Request.Session["Randum"]){
                    guesslower = false;
                    guesshigher = true;
                    guessnotcorrect = true;
                    guesscorrect = false;
                }
                else if(Request.Form.numguess < Request.Session["Randum"]){
                    guesslower = true;
                    guesshigher = false;
                    guessnotcorrect = true;
                    guesscorrect = false;
                }
                else {
                    guesslower = false;
                    guesshigher = false;
                    guesscorrect = true;
                    guessnotcorrect = false;
                }
                return Response.AsRedirect("/");
            });
            Post("/reset", args => 
            {
                guesslower = false;
                guesshigher = false;
                guessnotcorrect = true;
                guesscorrect = false;
                return Response.AsRedirect("/");
            });
            
        }
    }
}