using Nancy;

/*
 * References:
 * - http://www.c-sharpcorner.com/UploadFile/suthish_nair/who-is-nancy/
 * 
 * 
 * */

namespace WebApplication
{
     public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = parameters => @"<h1>Hello World!</hi><br/> <br/ ><h3>Welcome to C# Corner!</h3>
   <p style='color:orange'>My first Nancy example.</p>";
            Get["/Sample/{yourname}"] = req => "Hello " + req.yourname;
        }
    }
}