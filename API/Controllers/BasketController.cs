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
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

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

        private string? GetUserIdFromToken()
        {
            var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader == null)
            {
                return null;
            }

            var token = authHeader.StartsWith("Bearer ") ? authHeader.Substring("Bearer  ".Length).Trim() : authHeader;
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            return userId;
        }

        [Authorize]
        [HttpPut("current")]
        public async Task<ActionResult<UserBasketDTO>> updateUserBasket([FromBody] UserBasketInputDTO basket)
        {
            var userId = User.FindFirst("nameid")?.Value;
            if (userId == null)
            {
                return Unauthorized("User not logged in.");
            }

            if (basket == null)
            {
                return NotFound("Basket not found for the current user.");
            }
            var userBasket = await _context.UserBaskets
                .Include(b => b.BasketItems)
                .FirstOrDefaultAsync(b => b.UserId == userId);
            if (userBasket == null)
            {
                return NotFound();
            }

            decimal totalPrice = 0;

            // Remove items not in the new basket
            var incomingIds = basket.BasketItems.Select(i => i.AlbumId).ToList();
            userBasket.BasketItems.RemoveAll(bi => !incomingIds.Contains(bi.AlbumId));

            foreach (var item in basket.BasketItems)
            {
                var album = await _context.Albums.FirstOrDefaultAsync(a => a.Id == item.AlbumId);
                if (album == null)
                {
                    return BadRequest($"Album with ID {item.AlbumId} not found.");
                }

                var existingItem = userBasket.BasketItems.FirstOrDefault(bi => bi.AlbumId == item.AlbumId);
                if (existingItem != null)
                {
                    existingItem.Amount = item.Amount;
                }
                else
                {
                    userBasket.BasketItems.Add(new BasketItem
                    {
                        Amount = item.Amount,
                        AlbumId = item.AlbumId,
                        Album = album,
                        UserId = userId
                    });
                }
                totalPrice += album.Price * item.Amount;
            }

            userBasket.TotalPrice = totalPrice;
            userBasket.ShippingAddress = basket.ShippingAddress;
            userBasket.ShippingPrice = basket.ShippingPrice;

            await _context.SaveChangesAsync();
            return Ok(basket);
        }

        [Authorize]
        [HttpGet("current")]
        public async Task<ActionResult<UserBasketDTO>> GetCurrentUserBasket()
        {
            var userId = User.FindFirst("nameid")?.Value;
            if (userId == null)
            {
                return Unauthorized("User not logged in.");
            }

            var result = await this.getBasketByUserId(userId);
            var basket = result.Value;

            if (basket == null)
            {
                return NotFound("Basket not found for the current user.");
            }

            var basketDto = new UserBasketDTO
            {
                BasketItems = basket.BasketItems.Select(item => new BasketItemDTO
                {
                    AlbumId = item.AlbumId,
                    Title = item.Album?.Title,
                    Artist = item.Album?.Artist,
                    Price = item.Album?.Price ?? 0,
                    Amount = item.Amount
                }).ToList(),
                TotalPrice = basket.TotalPrice,
                ShippingAddress = basket.ShippingAddress,
                ShippingPrice = basket.ShippingPrice
            };

            return Ok(basketDto);
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddAlbumToBasket([FromBody] BasketItemInputDTO basketItemDto)
        {
            var userId = User.FindFirst("nameid")?.Value;
            if (userId == null)
            {
                return Unauthorized("User not logged in.");
            }

            var userBasket = await _context.UserBaskets
                .Include(b => b.BasketItems)
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (userBasket == null)
            {
                userBasket = new UserBasket
                {
                    UserId = userId,
                    BasketItems = new List<BasketItem>(),
                    TotalPrice = 0,
                    ShippingAddress = "",
                    ShippingPrice = 0
                };
                _context.UserBaskets.Add(userBasket);
            }

            var album = await _context.Albums.FirstOrDefaultAsync(a => a.Id == basketItemDto.AlbumId);

            var basketItem = userBasket.BasketItems.FirstOrDefault(b => b.AlbumId == basketItemDto.AlbumId);
            if (basketItem == null)
            {
                basketItem = new BasketItem
                {
                    AlbumId = basketItemDto.AlbumId,
                    Amount = basketItemDto.Amount,
                    Album = album,
                    UserId = userId
                };
                userBasket.BasketItems.Add(basketItem);
            }
            else
            {
                basketItem.Amount += basketItemDto.Amount;
            }

            userBasket.TotalPrice += album.Price * basketItemDto.Amount;

            await _context.SaveChangesAsync();
            return Ok("Album added to basket.");
        }

        [Authorize]
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveAlbumFromBasket([FromQuery] BasketItemInputDTO basketItemDTO)
        {
            var userId = User.FindFirst("nameid")?.Value;
            if (userId == null)
            {
                return Unauthorized("User not logged in.");
            }

            var userBasket = await _context.UserBaskets
                .Include(b => b.BasketItems)
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (userBasket == null)
            {
                return NotFound("Basket not found for the current user.");
            }

            var album = await _context.Albums.FirstOrDefaultAsync(a => a.Id == basketItemDTO.AlbumId);

            var basketItem = userBasket.BasketItems.FirstOrDefault(b => b.AlbumId == basketItemDTO.AlbumId);

            var basketItemAmount = basketItem?.Amount;

            if (basketItem == null)
            {
                return NotFound("Album not found in the basket.");
            }

            userBasket.TotalPrice -= album.Price * basketItem.Amount;

            userBasket.BasketItems.Remove(basketItem);

            await _context.SaveChangesAsync();
            return Ok(userBasket);
        }

        public async Task<ActionResult<UserBasket>> getBasketByUserId(string userId)
        {
            var basket = await _context.UserBaskets
                .Include(b => b.BasketItems)
                    .ThenInclude(i => i.Album)
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (basket == null)
            {
                return NotFound();
            }

            return basket;
        }
    }
}
