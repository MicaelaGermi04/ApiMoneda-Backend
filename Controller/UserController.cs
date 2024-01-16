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
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        if (role == Role.Admin.ToString())
        {
            return Ok(_userService.GetAll());
        }
        return Forbid();   //denegar el acceso a una operación restringida
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
    public IActionResult CreateUser(CreateUserDTO dto)
    {
        try
        {
            _userService.Create(dto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
        return Created("Created", dto);
    }
    [HttpPut("{userId}")]
    public IActionResult UpdateUser(UpdateUserDto dto, int userId) 
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
            return BadRequest(ex);
        }
        return NoContent();
    }

    /// MIrar la parte del segundo if
    [HttpDelete("{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        if(role == "Admin")
        {
            User? user= _userService.GetById(userId); 
            if (user is null)
            {
                return BadRequest("El usuario que intenta eliminar no existe");
            }
            if (user.Id == userId)
            {
                _userService.Delete(userId);
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
        _userService.UpdateUserSubscription(userId,subscriptionId); ;
        return NoContent(); // la solicitud se ha procesado correctamente y no hay contenido adicional para devolver.
    }
}

