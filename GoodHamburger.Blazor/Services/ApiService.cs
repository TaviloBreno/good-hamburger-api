using GoodHamburger.Core.DTOs;

namespace GoodHamburger.Blazor.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ApiService>? _logger;

    public ApiService(HttpClient httpClient, ILogger<ApiService>? logger = null)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<OrderResponse>?> GetAllOrdersAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<OrderResponse>>("api/orders");
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error fetching orders");
            return new List<OrderResponse>();
        }
    }

    public async Task<OrderResponse?> GetOrderByIdAsync(Guid id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<OrderResponse>($"api/orders/{id}");
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error fetching order {Id}", id);
            return null;
        }
    }

    public async Task<OrderResponse?> CreateOrderAsync(CreateOrderRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/orders", request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<OrderResponse>();
            }
            return null;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error creating order");
            return null;
        }
    }

    public async Task<OrderResponse?> UpdateOrderAsync(Guid id, UpdateOrderRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/orders/{id}", request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<OrderResponse>();
            }
            return null;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error updating order {Id}", id);
            return null;
        }
    }

    public async Task<bool> DeleteOrderAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/orders/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error deleting order {Id}", id);
            return false;
        }
    }
}