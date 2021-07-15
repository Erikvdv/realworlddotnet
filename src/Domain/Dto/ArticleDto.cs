using System.ComponentModel.DataAnnotations;

namespace realworlddotnet.Domain.Dto
{
    public class NewArticleDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public string[] TagList { get; set; }
    }
}