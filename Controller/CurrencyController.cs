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
        [AllowAnonymous]
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
            if(currencyId == 0) // No chequeo si es nulo porque el resultado siempre seria false, un valor 'int' nunca es igual a 'NULL'
            {
                return BadRequest();
            }
            Currency? currency = _currencyService.GetById(currencyId);

            if(currency == null)
            {
                return NotFound();
            }

            var currencydto = new CurrencyDTO
            {
                Id = currency.Id,
                Name = currency.Name,
                isOcode = currency.ISOcode,
                Value = currency.Value,
            };
            return Ok(currencydto);
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
                return NoContent(); // 204 la solicitud ha sido aceptada y procesada correctamente, pero no hay datos adicionales para devolver en la respuesta
            }
            return Forbid(); // No autorizado
        }

        [HttpDelete("{currencyId}")] 
        public IActionResult DeleteCurrency(int currencyId)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if(role == Role.Admin.ToString())
            {
                Currency? currency = _currencyService.GetById(currencyId);
                if (currency is null)
                {
                    return NotFound();
                }
                _currencyService.DeleteCurrency(currencyId);
                
                return NoContent();
            }
            return Forbid();
        }

    }
}
