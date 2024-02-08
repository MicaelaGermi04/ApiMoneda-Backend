using ApiMoneda.Entities;
using ApiMoneda.Models.Dto.ConversionDTO;
using ApiMoneda.Service.Interface;
using ApiMoneda.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiMoneda.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConversionController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IConversionService _conversionService;
        private readonly ICurrencyService _currencyService;

        public ConversionController(IUserService userService, ISubscriptionService subscriptionService, IConversionService conversionService, ICurrencyService currencyService)
        {
            _userService = userService;
            _subscriptionService = subscriptionService;
            _conversionService = conversionService;
            _currencyService = currencyService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            //obtener el valor de un atributo NameIdentifier de un objeto Claims y convertirlo a un entero utilizando Int32.Parse().
            int userId = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            return Ok(_conversionService.GetAll(userId));
        }

        [HttpPost("convert")]  //Recibo los paramentros en el body de la request por Post
        [AllowAnonymous]
        public IActionResult PerformAConversion([FromBody] ConversionRequestDTO requestbody)  
        {
            int userId = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            
            //Comparar Id de usuario ya autenticado con el Id de la request
            if(userId != requestbody.UserId)
            {
                return Forbid("No autorizado"); //El usuario no tiene autorizacion
            }

           User user= _userService.GetById(userId);
            int subscriptionId = user.SubscriptionId;
            int conversionCounter = _conversionService.ConversionCounter(requestbody.UserId);
            Subscription userSubscription= _subscriptionService.GetSubscriptionById(subscriptionId);


            if(userSubscription == null)
            {
                return BadRequest("Subscripcion no encontrada");
            }

            if (conversionCounter < userSubscription.AmountOfConvertions)
            {
                Currency firstCurrency = _currencyService.GetByName(requestbody.FirstCurrencyName);
                Currency secondCurrency = _currencyService.GetByName(requestbody.SecondCurrencyName);

                if(firstCurrency == null ||  secondCurrency == null)
                {
                    return BadRequest("Monedas no encontradas");
                }

                //Conversion
                decimal convertedAmount = requestbody.FirstCurrencyAmount * (firstCurrency.Value / secondCurrency.Value);

                var createDto = new CreateConversionDTO
                {
                    UserId = requestbody.UserId,
                    //FirstCurrencyId = requestbody.FirstCurrencyId,
                    //SecondCurrencyId = requestbody.SecondCurrencyId,
                    FirstCurrencyAmount = requestbody.FirstCurrencyAmount,
                    ConvertedAmount = convertedAmount,
                    FirstCurrencyName = firstCurrency.Name,
                    SecondCurrencyName = secondCurrency.Name,
                };

                try
                {
                    _conversionService.CreateConversion(createDto);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error al realizar la conversion: {ex.Message}");
                }
                return Ok(convertedAmount);
            }
            return Forbid("El usuario supera la cantidad de conversiones"); /// 
        }

        [HttpGet("AmountConversions")]
        [AllowAnonymous]

        public IActionResult GetAmountOfConversatios()
        {
            int userId = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            int conversationsAmount = _conversionService.ConversionCounter(userId);
            return Ok(conversationsAmount);
        }
    }
}
