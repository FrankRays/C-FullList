using Nancy;

namespace HelloNancy{
    public class HelloModule : NancyModule{
        public HelloModule(){
            Get("/", args => {
                
                return View["Hello"];
            });
            Post("/template", args =>
            {
                return Response.AsRedirect("/");
            });
            Get("/{name}", args => $"Hello {args.name}!");
        }
    }
}