using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EnglishResourceUI.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Level>> Levels()
        {
            return await _db.Levels.ToListAsync(); 
        }

        public async Task<IEnumerable<StudyFile>> DisplayFiles(string sTerm = "", int levelID = 0)
        {
            sTerm = sTerm.ToLower();
            IEnumerable<StudyFile> studyFiles = await (from studyFile in _db.StudyFiles
                              join level in _db.Levels
                              on studyFile.LevelId equals level.Id
                              join topic in _db.Topics
                              on studyFile.TopicId equals topic.Id
                              where string.IsNullOrWhiteSpace(sTerm) || (studyFile!=null && studyFile.FileName.ToLower().StartsWith(sTerm))
                              select new StudyFile
                              {
                                  Id = studyFile.Id,
                                  FileName = studyFile.FileName,
                                  TopicId = studyFile.TopicId,
                                  LevelId = studyFile.LevelId,
                                  TopicName = topic.TopicName,
                                  LevelName = level.CEFRLevel
                              }
                              ).ToListAsync();
            if(levelID > 0)
            {
                studyFiles = studyFiles.Where(a => a.LevelId == levelID).ToList();
            }
            return studyFiles;
        }
    }
}
