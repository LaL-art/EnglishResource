namespace EnglishResourceUI.Models.DTOs
{
    public class StudyFileDisplayModel
    {
        public IEnumerable<StudyFile> StudyFiles { get; set; }
        public IEnumerable<Level> Levels { get; set; }
        public string STerm { get; set; } = "";
        public int LevelId { get; set; } = 0;
    }
}
