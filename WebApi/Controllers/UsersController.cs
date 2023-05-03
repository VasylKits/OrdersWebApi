using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetAllUsersAsync()
    {
        var result = await _userService.GetUsersAsync();

        return result.Success 
            ? Ok(result.Data) 
            : BadRequest(result.ErrorInfo.Error);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> GetByIdAsync([FromRoute] int id)
    {
        var result = await _userService.GetUserByIdAsync(id);

        return result.Success
            ? Ok(result.Data)
            : BadRequest(result.ErrorInfo.Error);
    }

    [HttpPost]
    public async Task<ActionResult<UserResponse>> CreateAsync([FromBody] UserAddDto userAddDto)
    {
        var result = await _userService.AddUserAsync(userAddDto);

        return result.Success
            ? Ok(result.Data)
            : BadRequest(result.ErrorInfo.Error);
    }

    [HttpPut]
    public async Task<ActionResult<UserResponse>> UpdateAsync([FromBody] UserEditDto userEditDto)
    {
        var result = await _userService.UpdateUserAsync(userEditDto);

        return result.Success
            ? Ok(result.Data)
            : BadRequest(result.ErrorInfo.Error);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        var result = await _userService.DeleteUserAsync(id);

        return result.Success
            ? Ok()
            : BadRequest(result.ErrorInfo.Error);
    }
}