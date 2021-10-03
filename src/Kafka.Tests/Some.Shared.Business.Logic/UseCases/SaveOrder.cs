using Some.Shared.Business.Logic.Repository;
using System.Threading.Tasks;

namespace Some.Shared.Business.Logic.UseCases
{
    public interface ISaveOrder
    {
        Task Execute(OrderEvent order);
    }
    public class SaveOrder : ISaveOrder
    {
        private readonly IOrderRepository orderRepository;

        public SaveOrder(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task Execute(OrderEvent order)
        {
            await orderRepository.SaveAsync(order);
        }
    }
}
