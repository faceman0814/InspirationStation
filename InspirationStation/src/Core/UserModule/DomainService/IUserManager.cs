using FaceMan.Utils.Domain.Services;

namespace Core.UserModule.DomainService;

public interface IUserManager: IBasicDomainService<User, string>
{
    Task<bool> IsExistAsync(string id);


    Task<User> CreateAsync(User entity);


    Task UpdateAsync(User entity);


    Task DeleteAsync(string id);


    Task BatchDelete(List<string> input);
}