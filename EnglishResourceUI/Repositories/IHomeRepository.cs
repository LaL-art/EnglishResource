namespace EnglishResourceUI
{
    public interface IHomeRepository
    {
        Task<IEnumerable<StudyFile>> DisplayFiles(string sTerm = "", int levelID = 0);
        Task<IEnumerable<Level>> Levels();
    }
}