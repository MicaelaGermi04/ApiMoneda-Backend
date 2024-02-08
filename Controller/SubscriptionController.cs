using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ApiMoneda.Service.Interface;
using ApiMoneda.Entities;

namespace ApiMoneda.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        public SubscriptionController (ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public IActionResult GetAll() 
        {
            return Ok(_subscriptionService.GetAll());
        }

        [HttpGet("{subscriptionId}")]
        public IActionResult GetById(int subscriptionId)
        {
            Subscription subscription = _subscriptionService.GetSubscriptionById(subscriptionId);
            if (subscription is null)
            {
                return NotFound();
            }
            return Ok(subscription);
        }

        [HttpGet("{subscriptionId}/AmountOfConversions")]
        public IActionResult GetSubscriptionAmountOfConversions(int subscriptionId)
        {
            int amountOfConversions = _subscriptionService.GetSubscriptionAmountOfConversions(subscriptionId);
            return Ok(amountOfConversions);
        }
    }

}
