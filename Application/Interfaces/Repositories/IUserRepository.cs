using Domain.Entities;
using Interfaces.Repositories;

namespace Application.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<bool> IsLoginUniqueAsync(string login);
}