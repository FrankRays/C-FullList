using System;
using System.Collections.Generic;
using Nancy;
using ApiCaller;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace pokeapi{
     public class pokeapimodule : NancyModule{
         public Random rng = new Random();
         public pokeapimodule(){
             Get("/", async arg =>{
                string Name = "";
                object typename = "";
                JArray jsontype;
                long weight = 0;
                long height = 0;
                await WebRequest.SendRequest("http://pokeapi.co/api/v2/pokemon/1", new Action<Dictionary<string, object>>( JsonResponse =>
                {
                    Name = (string)JsonResponse["name"];
                    weight = (long)JsonResponse["weight"];
                    height = (long)JsonResponse["height"];
                    jsontype = JsonResponse["types"] as JArray;
                    foreach (JObject o in jsontype.Children<JObject>()){
                        foreach(JProperty p in o.Properties()){
                            if(p.Name == "type"){
                            typename = p.Value["name"];
                            }
                        }
                    }
                }
                 ));
                 ViewBag.name = Name;
                 ViewBag.weight = $"{weight}";
                 ViewBag.height = $"{height}";
                 ViewBag.typename = $"{typename}";
                return View["index.sshtml"];
            });
            Get("/random", arg =>{
                int num = rng.Next(1,151);
                return Response.AsRedirect($"/{num}");
            });
            Get("/{number}", async arg =>{
                string Name = "";
                object typename = "";
                JArray jsontype;
                long weight = 0;
                long height = 0;
                string address = $"http://pokeapi.co/api/v2/pokemon/{arg.number}";
                await WebRequest.SendRequest(address, new Action<Dictionary<string, object>>( JsonResponse =>
                {
                    Name = (string)JsonResponse["name"];
                    weight = (long)JsonResponse["weight"];
                    height = (long)JsonResponse["height"];
                    jsontype = JsonResponse["types"] as JArray;
                    foreach (JObject o in jsontype.Children<JObject>()){
                        foreach(JProperty p in o.Properties()){
                            if(p.Name == "type"){
                            typename = p.Value["name"];
                            }
                        }
                    }
                }
                 ));
                 ViewBag.name = Name;
                 ViewBag.weight = $"{weight}";
                 ViewBag.height = $"{height}";
                 ViewBag.typename = $"{typename}";
                 return View["index.sshtml"];
            });
         }
     }

}
