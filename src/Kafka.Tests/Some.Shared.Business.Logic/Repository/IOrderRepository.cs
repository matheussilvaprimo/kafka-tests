using System.Threading.Tasks;

namespace Some.Shared.Business.Logic.Repository
{
    public interface IOrderRepository
    {
        Task SaveAsync(OrderEvent order);
    }
}
