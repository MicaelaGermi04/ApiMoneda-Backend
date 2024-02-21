using ApiMoneda.Service.Interface;
using ApiMoneda.Data;
using ApiMoneda.Entities;
using Microsoft.EntityFrameworkCore;
using ApiMoneda.Models.Dto;
using ApiMoneda.Models.Enum;
using System.ComponentModel.DataAnnotations;
using ApiMoneda.Models.Dto.UserDTO;
using ApiMoneda.Models.Dto.SubscriptionDTO;

namespace ApiMoneda.Service.Implementations
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ConversorContext _context;
        public SubscriptionService(ConversorContext Context)
        {
            _context = Context;
        }
        public Subscription GetSubscriptionById(int subscriptionId)
        {
            return _context.Subscriptions.FirstOrDefault(s => s.Id == subscriptionId);
        }

        public List<Subscription> GetAll()
        {
            return _context.Subscriptions.ToList();
        }

        public int GetSubscriptionAmountOfConversions(int subscriptionId)
        {
            Subscription? subscription = GetSubscriptionById(subscriptionId);
            if (subscription != null)
            {
                return subscription.AmountOfConvertions;
            }
            return 0;
        }
        public bool CheckIfUserExists(int subscriptionId)
        {
            Subscription? subscription = _context.Subscriptions.SingleOrDefault(s => s.Id == subscriptionId);
            if (subscription == null)
            {
                return false;
            }
            return true;
        }
    }
}