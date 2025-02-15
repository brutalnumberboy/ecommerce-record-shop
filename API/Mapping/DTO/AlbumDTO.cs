
namespace API.Mapping.DTO
{
    public class AlbumDTO
    {
        public string Genre { get; set; }
        public string? Description { get; set; }
        public int YearReleased { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public int Length { get; set; }
        public bool InStock { get; set; }
    }
}