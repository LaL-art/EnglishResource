using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishResourceUI.Models
{
    [Table("Topic")]
    public class Topic
    {
        public int Id { get; set; }
        [Required, MaxLength (100)]
        public string TopicName { get; set; }
        public List<StudyFile> StudyFiles{ get; set;}
    }
}
