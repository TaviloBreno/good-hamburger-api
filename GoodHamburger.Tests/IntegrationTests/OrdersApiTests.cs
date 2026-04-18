using GoodHamburger.Core.DTOs;
using GoodHamburger.Core.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;

namespace GoodHamburger.Tests.IntegrationTests;

public class OrdersApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public OrdersApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateOrder_ShouldReturnCreated()
    {
        var request = new CreateOrderRequest(SandwichType.XBurger, SideDishType.Fries, DrinkType.Soda);

        var response = await _client.PostAsJsonAsync("/api/orders", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task GetAllOrders_ShouldReturnOk()
    {
        var response = await _client.GetAsync("/api/orders");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOrderById_WithInvalidId_ShouldReturnNotFound()
    {
        var response = await _client.GetAsync($"/api/orders/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateOrder_WithInvalidId_ShouldReturnNotFound()
    {
        var request = new UpdateOrderRequest(SandwichType.XBurger, null, null);
        var response = await _client.PutAsJsonAsync($"/api/orders/{Guid.NewGuid()}", request);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteOrder_WithInvalidId_ShouldReturnNotFound()
    {
        var response = await _client.DeleteAsync($"/api/orders/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetMenu_ShouldReturnOk()
    {
        var response = await _client.GetAsync("/api/menu");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("X Burger", content);
        Assert.Contains("Batata Frita", content);
        Assert.Contains("Refrigerante", content);
    }
}