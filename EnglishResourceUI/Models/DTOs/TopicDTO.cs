using System.ComponentModel.DataAnnotations;

namespace EnglishResourceUI.Models.DTOs
{
    public class TopicDTO
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string TopicName { get; set; }
    }
}
