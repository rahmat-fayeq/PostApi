using System.ComponentModel.DataAnnotations;

namespace AppApi.Model.Dto
{
    public class PostDto
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
    }
}
