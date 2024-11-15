using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishResourceUI.Models
{
    [Table("FavouritesDetail")]
    public class FavouritesDetail
    {
        public int Id { get; set; }
        [Required]
        public int FavouritesId { get; set; }
        public int StudyFileId { get; set; }
        public StudyFile StudyFile { get; set; }
        public Favourites Favourites { get; set; }
    }
}
