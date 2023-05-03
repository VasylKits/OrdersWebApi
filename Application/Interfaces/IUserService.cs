using Application.Common.Result.Abstractions;
using Application.Common.Result.Abstractions.Generics;
using Application.DTOs;

namespace Application.Interfaces;

public interface IUserService
{
    Task<IResult<List<UserResponse>>> GetUsersAsync();
    Task<IResult<UserResponse>> GetUserByIdAsync(int userId);
    Task<IResult<UserResponse>> AddUserAsync(UserAddDto userDto);
    Task<IResult<UserResponse>> UpdateUserAsync(UserEditDto userDto);
    Task<IResult> DeleteUserAsync(int userId);
}