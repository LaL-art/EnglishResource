using Microsoft.EntityFrameworkCore;

namespace EnglishResourceUI.Repositories
{
    public interface ILevelRepository
    {
        Task AddLevel(Level level);
        Task UpdateLevel(Level level);
        Task<Level?> GetLevelById(int id);
        Task DeleteLevel(Level level);
        Task<IEnumerable<Level>> GetLevels();
    }
    public class LevelRepository : ILevelRepository
    {
        private readonly ApplicationDbContext _context;
        public LevelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddLevel(Level level)
        {
            _context.Levels.Add(level);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateLevel(Level level)
        {
            _context.Levels.Update(level);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLevel(Level level)
        {
            _context.Levels.Remove(level);
            await _context.SaveChangesAsync();
        }

        public async Task<Level?> GetLevelById(int id)
        {
            return await _context.Levels.FindAsync(id);
        }

        public async Task<IEnumerable<Level>> GetLevels()
        {
            return await _context.Levels.ToListAsync();
        }
    }
}
