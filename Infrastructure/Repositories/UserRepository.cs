using Microsoft.EntityFrameworkCore;
using Application.Common.Constants;
using Application.Common.Result.Abstractions.Generics;
using Application.Common.Result.Implementations.Generics;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users
            .Include(u => u.Orders)
            .OrderByDescending(u => u.Id)
            .ToListAsync();
    }

    public async Task<IResult<User>> GetByIdAsync(int id)
    {
        var user = await _context.Users.Include(u => u.Orders)
            .FirstOrDefaultAsync(u => u.Id == id);

        return (user != null)
            ? Result<User>.CreateSuccess(user)
            : Result<User>.CreateFailed(ErrorModel.UserNotFound);
    }

    public async Task<User> AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsLoginUniqueAsync(string login)
    {
        return await _context.Users.AllAsync(u => u.Login != login);
    }
}