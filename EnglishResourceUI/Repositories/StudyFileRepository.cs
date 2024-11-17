using Microsoft.EntityFrameworkCore;

namespace EnglishResourceUI.Repositories
{
    public interface IStudyFileRepository
    {
        Task AddFile(StudyFile studyFile);
        Task DeleteFile(StudyFile studyFile);
        Task<StudyFile?> GetFileById(int id);
        Task<IEnumerable<StudyFile>> GetFiles();
        Task UpdateFile(StudyFile studyFile);
    }
    public class StudyFileRepository : IStudyFileRepository
    {
        private readonly ApplicationDbContext _context;
        public StudyFileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddFile(StudyFile studyFile)
        {
            _context.StudyFiles.Add(studyFile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFile(StudyFile studyFile)
        {
            _context.StudyFiles.Update(studyFile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFile(StudyFile studyFile)
        {
            _context.StudyFiles.Remove(studyFile);
            await _context.SaveChangesAsync();
        }

        public async Task<StudyFile?> GetFileById(int id) => await _context.StudyFiles.FindAsync(id);

        public async Task<IEnumerable<StudyFile>> GetFiles() => 
            await _context.StudyFiles.Include(a => a.Topic).Include(a=>a.Level).ToListAsync();
    }
}
