using ApiMoneda.Entities;
using ApiMoneda.Models.Dto;
using ApiMoneda.Models.Dto.CurrencyDTO;
using ApiMoneda.Models.Enum;
using ApiMoneda.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiMoneda.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService; 
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        public IActionResult GetAll() 
        {
            return Ok(_currencyService.GetAll());
        }

        [HttpPost]
        public IActionResult CreateCurrency(CreateAndUpdateCurrencyDTO dto)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if(role == Role.Admin.ToString())
            {
                try
                {
                    _currencyService.Create(dto);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex); 
                }
                return Created("Created", dto);
            }
            return Forbid();
        }

        [HttpGet("{currencyId}")]
        public IActionResult GetById(int currencyId)
        {
            if(currencyId == 0) 
            {
                return BadRequest();
            }
            Currency? currency = _currencyService.GetById(currencyId);

            if(currency == null)
            {
                return NotFound();
            }

            return Ok(currency);
        }

        [HttpPut("{currencyId}")]
        public IActionResult UpdateCurrency(CreateAndUpdateCurrencyDTO dto, int currencyId)
        {
            string role= User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (role == Role.Admin.ToString())
            {
                if (!_currencyService.CheckIfCurrencyExists(currencyId))
                {
                    return NotFound();
                }
                try
                {
                    _currencyService.Update(dto, currencyId);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
                return NoContent(); // 204 
            }
            return Forbid(); // 403
        }

        [HttpDelete("{currencyId}")] 
        public IActionResult DeleteCurrency(int currencyId)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if(role == Role.Admin.ToString())
            {
                if (!_currencyService.CheckIfCurrencyExists(currencyId))
                {
                    return NotFound();
                }
                try
                {
                   _currencyService.DeleteCurrency(currencyId);
                }
                catch(Exception ex)
                {
                   return BadRequest(ex);
                }
                return NoContent();
            }
            return Forbid();
        }

    }
}
