using API.Context;
using API.Mapping.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        public AlbumController(StoreContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }

        // api/album
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumDTO>>> GetAllAlbums(){
            var albums = await _context.Albums.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<AlbumDTO>>(albums));
        }

        // api/album/{id}
        [HttpGet("id")]
        public async Task<ActionResult<AlbumDTO>> GetAlbumById(int id){
            var album = await _context.Albums.FindAsync(id);
            return Ok(_mapper.Map<AlbumDTO>(album));
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<AlbumDTO>>> SearchAlbumByTitleOrArtist([FromQuery] string query){
            var result = await _context.Albums.Where(search => search.Title.Contains(query) || search.Artist.Contains(query)).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<AlbumDTO>>(result));
        }
    }
}