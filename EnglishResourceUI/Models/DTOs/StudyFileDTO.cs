using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EnglishResourceUI.Models.DTOs
{
    public class StudyFileDTO
    {
        public int Id { get; set; }
        [Required, MaxLength(60)]
        public string FileName { get; set; }
        [Required]
        public string FileURL { get; set; }
        [Required]
        public int TopicId { get; set; }
        public int LevelId { get; set; }
        public IEnumerable<SelectListItem>? TopicList { get; set; }
        public IEnumerable<SelectListItem>? LevelList { get; set; }
    }
}
