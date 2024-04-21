using FaceMan.Utils.Auditing;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers;

public class HomeController : Controller
{
    [DisableAuditing]
    public IActionResult Index()
    {
        // return RedirectToAction("Index", "Monitor");
        return Redirect("/swagger");
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