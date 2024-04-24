using FaceMan.Utils.Domain.Repositories;
using FaceMan.Utils.Domain.Services;
using FaceMan.Utils.Timing;
using Microsoft.EntityFrameworkCore;

namespace Core.UserModule.DomainService;

public class UserManager:BasicDomainService<User,string>, IUserManager
{
    public UserManager(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        
    }
    
    public async Task BatchDelete(List<string> input)
    {
        await EntityRepo.DeleteAsync(a => input.Contains(a.Id));
    }

    public async Task BatchUpdate(List<User> input)
    {
        foreach (var inputItem in input)
        {
            await EntityRepo.UpdateAsync(inputItem);
        }
    }

    public async Task BatchCreateAsync(List<User> input)
    {
        foreach (var inputItem in input)
        {
           await CreateAsync(inputItem);
        }
    }

    public async Task<User> CreateAsync(User entity)
    {
        entity.Id = await EntityRepo.InsertAndGetIdAsync(entity);

        return entity;
    }

    public async Task DeleteAsync(string id)
    {
        await EntityRepo.DeleteAsync(id);
    }

    public async Task<bool> IsExistAsync(string id)
    {
        var result = await QueryAsNoTracking.AnyAsync(a => a.Id == id);
        return result;
    }

    public async Task UpdateAsync(User entity)
    {
        await EntityRepo.UpdateAsync(entity);
    }
}