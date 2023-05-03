using AutoMapper;
using Application.Common.Result.Abstractions.Generics;
using Application.Common.Result.Implementations.Generics;
using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces;
using Application.Common.Result.Abstractions;
using Domain.Entities;
using Application.Common.Result.Implementations;
using Application.Common.Constants;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IResult<List<UserResponse>>> GetUsersAsync()
    {
        try
        {
            var users = await _userRepository.GetAllAsync();

            if (users.Count == 0)
                return Result<List<UserResponse>>.CreateFailed(ErrorModel.UserNotFound);

            return Result<List<UserResponse>>.CreateSuccess(_mapper.Map<List<UserResponse>>(users));
        }
        catch (Exception e)
        {
            return Result<List<UserResponse>>.CreateFailed(ErrorModel.UserNotFound).AddError(e.Message);
        }
    }

    public async Task<IResult<UserResponse>> GetUserByIdAsync(int id)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (!user.Success)
                return Result<UserResponse>.CreateFailed(ErrorModel.UserNotFound);

            return Result<UserResponse>.CreateSuccess(_mapper.Map<UserResponse>(user.Data));
        }
        catch (Exception e)
        {
            return Result<UserResponse>.CreateFailed(ErrorModel.UserNotFound).AddError(e.Message);
        }
    }

    public async Task<IResult<UserResponse>> AddUserAsync(UserAddDto userDto)
    {
        try
        {
            var isLoginUnique = await _userRepository.IsLoginUniqueAsync(userDto.Login);

            if (isLoginUnique == false)
                return Result<UserResponse>.CreateFailed(ErrorModel.LoginNameAlreadyTaken);

            var user = _mapper.Map<User>(userDto);

            return Result<UserResponse>.CreateSuccess(_mapper.Map<UserResponse>(await _userRepository.AddAsync(user)));
        }
        catch (Exception e)
        {
            return Result<UserResponse>.CreateFailed(ErrorModel.UserIsNotCreated).AddError(e.Message);
        }
    }

    public async Task<IResult<UserResponse>> UpdateUserAsync(UserEditDto userEditDto)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(userEditDto.Id);

            if (!user.Success)
                return Result<UserResponse>.CreateFailed(ErrorModel.UserNotFound);

            var isLoginUnique = await _userRepository.IsLoginUniqueAsync(userEditDto.Login);

            if (userEditDto.Login != user.Data.Login && isLoginUnique == false)
                return Result<UserResponse>.CreateFailed(ErrorModel.LoginNameAlreadyTaken);

            _mapper.Map(userEditDto, user.Data);

            await _userRepository.UpdateAsync(user.Data);

            return Result<UserResponse>.CreateSuccess(_mapper.Map<UserResponse>(user.Data));
        }
        catch (Exception e)
        {
            return Result<UserResponse>.CreateFailed(e.Message);
        }
    }

    public async Task<IResult> DeleteUserAsync(int id)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (!user.Success)
                return Result.CreateFailed(ErrorModel.UserNotFound);

            if (user.Data.Orders.Any())
                return Result.CreateFailed(ErrorModel.UserHasOrdersAndCannotBeDeleted);

            await _userRepository.DeleteAsync(user.Data);

            return Result.CreateSuccess();
        }
        catch (Exception e)
        {
            return Result.CreateFailed(ErrorModel.UserIsNotDeleted).AddError(e.Message);
        }
    }
}