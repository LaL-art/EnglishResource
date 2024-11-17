using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;

namespace EnglishResourceUI.Repositories
{
    public interface ITopicRepository
    {
        Task AddTopic(Topic topic);
        Task UpdateTopic(Topic topic);
        Task<Topic?> GetTopicById(int id);
        Task DeleteTopic(Topic topic);
        Task<IEnumerable<Topic>> GetTopics();
    }
    public class TopicRepository : ITopicRepository
    {
        private readonly ApplicationDbContext _context;
        public TopicRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTopic(Topic topic)
        {
            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTopic(Topic topic)
        {
            _context.Topics.Update(topic);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTopic(Topic topic)
        {
            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();
        }

        public async Task<Topic?> GetTopicById(int id)
        {
            return await _context.Topics.FindAsync(id);
        }

        public async Task<IEnumerable<Topic>> GetTopics()
        {
            return await _context.Topics.ToListAsync();
        }

    }
}
