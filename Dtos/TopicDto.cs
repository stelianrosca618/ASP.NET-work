using Microsoft.EntityFrameworkCore;
using MyRazorPagesApp.Data;
using MyRazorPagesApp.Models;

namespace MyRazorPagesApp.Dtos
{
    public interface ITopicDto
    {
        Task<int> CreateTopic(Topic newTopic);
        Task<bool> DeleteTopic(int id);
        Task<List<Topic>> GetTopic();
        Task<Topic> GetTopicById(int id);
        Task<int> UpdateTopic(Topic newTopic);
    }

    public class TopicDto : ITopicDto
    {
        protected ApplicationDbContext _context;
        public TopicDto(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<int> CreateTopic(Topic newTopic)
        {
            try
            {
                if (newTopic != null)
                {
                    var entity = _context.Topics.Add(new Topic
                    {
                        TopicName = newTopic.TopicName
                    });

                    await _context.SaveChangesAsync();

                    return entity.Entity.TopicID;
                }

            }
            catch { }

            return 0;
        }
        public async Task<int> UpdateTopic(Topic newTopic)
        {
            try
            {
                var existing = await _context.Topics.FirstOrDefaultAsync(x => x.TopicID == newTopic.TopicID);

                if (existing != null)
                {
                    existing.TopicName = newTopic.TopicName;
                };
                await _context.SaveChangesAsync();

                return existing.TopicID;
            }
            catch (Exception ex) { }

            return 0;
        }
        public async Task<List<Topic>> GetTopic()
        {
            var Topics = new List<Topic>();
            try
            {
                var existingPost = await _context.Topics.ToListAsync();

                Topics = existingPost;
            }
            catch { }

            return Topics;
        }
        public async Task<Topic> GetTopicById(int id)
        {
            var Post = new Topic();
            try
            {
                var TopicBy = await _context.Topics.FirstOrDefaultAsync(x => x.TopicID == id);
                Post = TopicBy;
                return Post;

            }
            catch { }

            return null;
        }
        public async Task<bool> DeleteTopic(int id)
        {
            var Post = new Topic();
            try
            {
                var TopicToDel = await _context.Topics.SingleOrDefaultAsync(x => x.TopicID == id);
                if (TopicToDel != null)
                {
                    _context.Topics.Remove(TopicToDel);
                    await _context.SaveChangesAsync();

                    return true;
                }
            }
            catch { }

            return false;
        }
    }
}
