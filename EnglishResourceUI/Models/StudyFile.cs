using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishResourceUI.Models
{
    [Table("StudyFiles")]
    public class StudyFile
    {
        public int Id { get; set; }
        [Required, MaxLength (60)]
        public string FileName { get; set; }
        [Required]
        public string FileURL { get; set; }
        [Required]
        public int TopicId { get; set; }
        public int LevelId { get; set; }
        public List<FavouritesDetail> FavouritesDetail { get; set; }
        public Level Level { get; set; }
        public Topic Topic { get; set; }
    }
}
