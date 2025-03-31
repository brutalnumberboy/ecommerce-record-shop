using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Mapping.DTO;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : Controller
    {
        private readonly ILogger<BasketController> _logger;
        private readonly IMapper _mapper;

        private readonly StoreContext _context;

        public BasketController(ILogger<BasketController> logger, StoreContext context, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<UserBasketDTO>> getUserBasket(int id) {
                var basket = await _context.UserBaskets.FindAsync(id);
                return Ok(basket == null ? null : new UserBasketDTO { BasketItems = _mapper.Map<List<BasketItemDTO>>(basket.BasketItems), TotalPrice = basket.TotalPrice, ShippingAddress = basket.ShippingAddress, ShippingPrice = basket.ShippingPrice });

        }
        
        [HttpPost]
        public async Task<ActionResult<UserBasketDTO>> updateUserBasket(int id, UserBasketDTO basket) {
            var userBasket = await _context.UserBaskets.FindAsync(id);
            if (userBasket == null) {
                return NotFound();
            }
            userBasket.BasketItems = _mapper.Map<List<BasketItem>>(basket.BasketItems);
            userBasket.TotalPrice = basket.TotalPrice;
            userBasket.ShippingAddress = basket.ShippingAddress;
            userBasket.ShippingPrice = basket.ShippingPrice;
            await _context.SaveChangesAsync();
            return Ok(basket);
        }
    }
}