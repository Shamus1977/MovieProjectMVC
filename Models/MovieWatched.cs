using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L8HandsOn.Models
{
    public class MovieWatched
    {
        public int Id { get; set; }
        
        public string? Email { get; set; }
        public string? WatcherId { get; set; }
        
        public string? Title { get; set; }
    }
}
