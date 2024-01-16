using ApiMoneda.Entities;
using ApiMoneda.Models.Dto.SubscriptionDTO;

namespace ApiMoneda.Service.Interface
{
    public interface ISubscriptionService
    {
        List<Subscription> GetAll();
        Subscription? GetSubscriptionById(int id);
        int GetSubscriptionAmountOfConversions(int subscriptionId);

    }

}
