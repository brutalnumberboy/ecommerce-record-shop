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
        public AlbumController(StoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // api/album
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<AlbumDTO>>> GetAllAlbums()
        {
            var albums = await _context.Albums.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<AlbumDTO>>(albums));
        }

        // api/album/search?query={album title or artist name}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<AlbumDTO>>> SearchAlbumByTitleOrArtist([FromQuery] string query)
        {
            var result = await _context.Albums.Where(search => search.Title.ToLower().Contains(query) || search.Artist.ToLower().Contains(query)).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<AlbumDTO>>(result));
        }

        // api/album/filter?{genre or year}=
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<AlbumDTO>>> FilterAlbums([FromQuery] string[]? genre, [FromQuery] int[]? year)
        {
            
            var albums = _context.Albums.AsQueryable();
            if (genre != null && genre.Length > 0){
                albums = albums.Where(search => genre.Contains(search.Genre));
            }

            if (year != null && year.Length > 0){
                albums = albums.Where(search => year.Contains(search.YearReleased));
            }
            return Ok(_mapper.Map<IEnumerable<AlbumDTO>>(await albums.ToListAsync()));
        }

        [HttpGet("random")]
        public async Task<ActionResult<AlbumDTO>> GetRandomAlbum([FromQuery] int? count)
        {
            var albumCount = await _context.Albums.CountAsync();
            if (albumCount == 0)
            {
                return NotFound("No albums available.");
            }

            int takeCount = count ?? 1;
            takeCount = Math.Min(takeCount, albumCount);
            var randomAlbum = await _context.Albums.OrderBy(r => Guid.NewGuid()).Take(takeCount).ToListAsync();

            return Ok(_mapper.Map<IEnumerable<AlbumDTO>>(randomAlbum));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumDTO>> GetAlbumById(string id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound("Album not found.");
            }
            return Ok(_mapper.Map<AlbumDTO>(album));
        }

    }
}