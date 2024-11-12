using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishResourceUI.Models
{
    [Table("Favourites")]
    public class Favourites
    {
        public int Id { get; set; }
        public string UserId { get; set; }
    }
}
