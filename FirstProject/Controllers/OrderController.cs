using AutoMapper;
using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.user;

namespace FirstProject.Controllers;
[ApiController]

[Route("api/[controller]")]


public class OrderController : ControllerBase
{
    private IOrderService _orderService;
    private  IMapper mapper;

    public OrderController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        this.mapper = mapper;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> Get()
    {
        var users = await _orderService.GetOrders();
        if (users.Count() > 0)
            return Ok(users);
        return NotFound();
    }
    [HttpPost]
    public async Task<ActionResult<CreateOrderDTO>> AddOrder([FromBody] CreateOrderDTO orderDto)
    {
        Order order = mapper.Map<CreateOrderDTO, Order>(orderDto);
        var newOrder = await _orderService.AddOrder(order);
        var returnOrder = mapper.Map<Order, CreateOrderDTO>(newOrder.Data);
        if (newOrder.StatusCode == 200)
            return Ok(returnOrder);
        if (newOrder.StatusCode == 401)
        {
            return Unauthorized();
        }
        return NotFound();
    }



}
