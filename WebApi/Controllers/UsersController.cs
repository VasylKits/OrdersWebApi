using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Interfaces;
using Application.Common.Result.Implementations;

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

    [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorInfo), StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<ActionResult> GetAllUsersAsync()
    {
        var result = await _userService.GetUsersAsync();

        return result.Success
            ? Ok(result.Data)
            : BadRequest(result.ErrorInfo.Error);
    }

    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorInfo), StatusCodes.Status400BadRequest)]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetByIdAsync([FromRoute] int id)
    {
        var result = await _userService.GetUserByIdAsync(id);

        return result.Success
            ? Ok(result.Data)
            : BadRequest(result.ErrorInfo.Error);
    }

    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorInfo), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] UserAddDto userAddDto)
    {
        var result = await _userService.AddUserAsync(userAddDto);

        return result.Success
            ? Ok(result.Data)
            : BadRequest(result.ErrorInfo.Error);
    }

    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorInfo), StatusCodes.Status400BadRequest)]
    [HttpPut]
    public async Task<ActionResult> UpdateAsync([FromBody] UserEditDto userEditDto)
    {
        var result = await _userService.UpdateUserAsync(userEditDto);

        return result.Success
            ? Ok(result.Data)
            : BadRequest(result.ErrorInfo.Error);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorInfo), StatusCodes.Status400BadRequest)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        var result = await _userService.DeleteUserAsync(id);

        return result.Success
            ? Ok()
            : BadRequest(result.ErrorInfo.Error);
    }
}