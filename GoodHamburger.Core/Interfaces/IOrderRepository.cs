using GoodHamburger.Core.Entities;

namespace GoodHamburger.Core.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<decimal> GetTotalSalesAsync();
}