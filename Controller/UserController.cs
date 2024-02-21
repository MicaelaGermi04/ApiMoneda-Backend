using ApiMoneda.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ApiMoneda.Models.Enum;
using ApiMoneda.Entities;
using ApiMoneda.Models.Dto;

namespace ApiMoneda.Controller;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ISubscriptionService _subscriptionService;

    public UserController(IUserService userService, ISubscriptionService subscriptionService)
    {
        _userService = userService;
        _subscriptionService = subscriptionService;
    }

    [HttpGet]
    public IActionResult GetAll()  
    {
        string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        if (role == Role.Admin.ToString())
        {
            return Ok(_userService.GetAll()); 
        }
        return Forbid(); 
    }

    [HttpGet("{userId}")]
    public IActionResult GetById(int userId) 
    { 
        if (userId == 0)
        {
            return BadRequest("El ID ingresado no pude  ser 0");
        }
        User? user = _userService.GetById(userId);
        if (user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult CreateUser([FromBody] CreateUserDTO dto)
    {
        try
        {
            _userService.Create(dto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Created("Created", dto);
    }

    [HttpPut("{userId}")]
    [AllowAnonymous]
    public IActionResult UpdateUser(int userId, [FromBody] UpdateUserDto dto) 
    {
        if(!_userService.CheckIfUserExists(userId))
        {
            return NotFound();
        }
        try
        {
            _userService.Update(dto, userId);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return NoContent();
    }

    [HttpDelete("{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        if(role == "Admin")
        {
            if (!_userService.CheckIfUserExists(userId))
            {
                return NotFound();
            }
            try
            {
                _userService.Delete(userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent(); 
        }
        return Forbid();
    }
    
    [HttpPatch("{userId}")]
    public IActionResult UpdateUserSubscription(int userId, [FromBody] int subscriptionId)
    {
        if (subscriptionId == null)
        {
            return BadRequest();
        }
        if(!_subscriptionService.CheckIfUserExists(subscriptionId))
        {
            return NotFound();
        }
        _userService.UpdateUserSubscription(userId,subscriptionId); ;
        return NoContent(); 
    }
}

