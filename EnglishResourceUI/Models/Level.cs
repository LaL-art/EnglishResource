using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishResourceUI.Models
{
    [Table("Level")]
    public class Level
    {
        public int Id { get; set; }
        [Required]
        public string CEFRLevel { get; set; }
    }
}
