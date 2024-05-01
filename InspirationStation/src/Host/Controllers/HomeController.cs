using Core.UserModule;
using EntityFramework.Repository;
using FaceMan.Utils.Auditing;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers;

public class HomeController : Controller
{
    private readonly IRepository<User> _userRepository;

    public HomeController(IServiceProvider serviceProvider)
    {
        _userRepository = serviceProvider.GetService<IRepository<User>>();
    }


    [DisableAuditing]
    public IActionResult Index()
    {
        // return RedirectToAction("Index", "Monitor");
        return Redirect("/swagger");
    }

    // POST
    [HttpPost]
    [Route("basic")]
    public async Task<IActionResult> Index(string name, string email)
    {
        // var user = new User
        // {
        //     Name = name, Email = email, CreatorUserId = Guid.NewGuid().ToString("N"), Id = Guid.NewGuid().ToString("N")
        // };
        // await _userRepository.AddAsync(user);
        var result = await _userRepository.GetAllAsync();
        string message = $"Name: {result.First().Name}, Email: {result.First().Email}";
        // Do something with the data
        return Ok(message);
    }
}