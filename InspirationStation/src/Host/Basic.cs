using Microsoft.AspNetCore.Mvc;

namespace Host;

public class Basic : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    // POST
    [HttpPost]
    [Route("basic")]
    public IActionResult Index(string name, string email)
    {
        string message = $"Name: {name}, Email: {email}";
        // Do something with the data
        return Ok(message);
    }
}