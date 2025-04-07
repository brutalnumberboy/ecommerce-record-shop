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
using Microsoft.EntityFrameworkCore;

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

        [HttpPost]
        public async Task<ActionResult<UserBasketDTO>> createUserBasket([FromBody] UserBasketDTO basket)
        {
            var userBasket = new UserBasket
            {
                BasketItems = basket.BasketItems.Select(item => new BasketItem
                {
                    Title = item.Title,
                    Amount = item.Amount,
                    Price = item.Price,
                    AlbumId = item.AlbumId
                }).ToList(),
                TotalPrice = basket.TotalPrice,
                ShippingAddress = basket.ShippingAddress,
                ShippingPrice = basket.ShippingPrice
            };

            _context.UserBaskets.Add(userBasket);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(getUserBasket), new { id = userBasket.Id }, basket);
        }

        [HttpGet("id")]
        public async Task<ActionResult<UserBasketDTO>> getUserBasket(int id) {
                var basket = await _context.UserBaskets.Include(b => b.BasketItems).FirstOrDefaultAsync(b => b.Id == id);
                return Ok(basket == null ? null : new UserBasketDTO { BasketItems = _mapper.Map<List<BasketItemDTO>>(basket.BasketItems), TotalPrice = basket.TotalPrice, ShippingAddress = basket.ShippingAddress, ShippingPrice = basket.ShippingPrice });
        }
        
        [HttpPost("id")]
        public async Task<ActionResult<UserBasketDTO>> updateUserBasket(int id, [FromBody] UserBasketDTO basket) {
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