using OrderingApp.Data.Models;

namespace OrderingApp.Logic.Services.Interface
{
    public interface IOrderService
    {
        public Task<bool> OrderNameExist(string name, Guid? Id, CancellationToken cancellationToken);
        public Task<Order> GetBasicOrderInfo(Guid Id, CancellationToken cancellationToken);
    }
}
