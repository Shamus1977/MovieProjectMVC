using System.ComponentModel.DataAnnotations;

namespace L8HandsOn.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string? Title { get; set; }
        [Required]
        [Range(1,100, ErrorMessage = "Please Make A Selection")]
        public Genre? Genre { get; set; }
        public string? Director { get; set; }
        [Range(1900, 2080)]
        public int Year { get; set; }
        [MaxLength(300)]
        public string? Summary { get; set; }
        [Required]
        [Range(0, 5)]
        public Double ViewerRating { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "Please Make A Selection")]
        public Rating? Rating { get; set; }
    }
}
