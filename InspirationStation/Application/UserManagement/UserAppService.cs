using Core.UserModule;
using Core.UserModule.DomainService;
using FaceMan.Utils.Domain.Services;

namespace Application.UserManagement;

// public class UserAppService: AppServiceBase, IUserAppService
// {
//     private readonly IUserManager _userManager;
//     private UserAppService(IUserManager userManager)
//     {
//         _userManager = userManager;
//     }
//
//     public async Task<User> CreateUser(string userName)
//     {
//         var user = new User()
//         {
//             Id = Guid.NewGuid().ToString("N"),
//             CreatorUserId = Guid.NewGuid().ToString("N"),
//             Name = userName
//         };
//         return await _userManager.CreateAsync(user);
//     }
// }