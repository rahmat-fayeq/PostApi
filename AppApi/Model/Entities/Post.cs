using System.ComponentModel.DataAnnotations;

namespace AppApi.Model.Entities
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Title { get; set; } = null!;
        [Required]
        [MinLength(3)]
        [MaxLength(500)]
        public string Body { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
