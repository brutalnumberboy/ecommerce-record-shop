using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Album
    {
        [Key]
        public int AlbumId {get; set;}
        public string Genre {get; set;}
        [Required]
        public string? Description {get; set;}
        public int YearReleased{get; set; }
        [Required]
        public string Artist {get; set; }
        [Required]
        public string Title {get; set; }
        public int Length {get; set; }
    }
}