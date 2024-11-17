using System.ComponentModel.DataAnnotations;

namespace EnglishResourceUI.Models.DTOs
{
    public class LevelDTO
    {
        public int Id { get; set; }
        [Required]
        public string CEFRLevel { get; set; }
    }
}
