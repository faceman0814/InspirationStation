using Core.UserModule;
using EntityFramework.Repository;
using FaceMan.Utils.Auditing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Host.Controllers;

public class HomeController : Controller
{
    private readonly IRepository<User, string> _userRepository;
    private readonly IRepository<OrganizationUnit, string> _organizationUnitRepository;

    public HomeController(IServiceProvider serviceProvider)
    {
        _userRepository = serviceProvider.GetService<IRepository<User, string>>();
        _organizationUnitRepository = serviceProvider.GetService<IRepository<OrganizationUnit, string>>();
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
        var res = await _userRepository.GetAllIncluding(t=>t.OrganizationUnit).ToListAsync();
        // var user = new User
        // {
        //     Name = name, Email = email, Password = "password", PhoneNumber = "1234567890",
        //     CreatorUserId = Guid.NewGuid().ToString("N"), Id = Guid.NewGuid().ToString("N")
        // };
        // await _userRepository.InsertAsync(user);
        // var result = await _userRepository.GetAllListAsync();
        // string message = $"Name: {result.First().Name}, Email: {result.First().Email}";
        // // Do something with the data
        return Ok(res);
    }
    
    // POST
    [HttpPost]
    [Route("AddOrganizationUnit")]
    public async Task<IActionResult> AddOrganizationUnit(string name,string code)
    {
        var organizationUnit = new OrganizationUnit()
        {
            Name = name,
            Code = code,
            CreatorUserId = Guid.NewGuid().ToString("N"),
            Id = Guid.NewGuid().ToString("N")
        };
        await _organizationUnitRepository.InsertAsync(organizationUnit);
        // Do something with the data
        return Ok(organizationUnit);
    }
}