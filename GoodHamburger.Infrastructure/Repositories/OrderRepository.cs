using System.Linq.Expressions;
using GoodHamburger.Core.Entities;
using GoodHamburger.Core.Interfaces;
using GoodHamburger.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetByIdAsync(Guid id)
        => await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

    public async Task<IEnumerable<Order>> GetAllAsync()
        => await _context.Orders
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

    public async Task<IEnumerable<Order>> FindAsync(Expression<Func<Order, bool>> predicate)
        => await _context.Orders.Where(predicate).ToListAsync();

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Order order)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Expression<Func<Order, bool>> predicate)
        => await _context.Orders.AnyAsync(predicate);

    public async Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        => await _context.Orders
            .Where(o => o.CreatedAt >= startDate && o.CreatedAt <= endDate)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

    public async Task<decimal> GetTotalSalesAsync()
        => await _context.Orders.SumAsync(o => o.Total);
}