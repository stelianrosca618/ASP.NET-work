using Microsoft.EntityFrameworkCore;
using MyRazorPagesApp.Data;
using MyRazorPagesApp.Models;

namespace MyRazorPagesApp.Dtos
{
    public interface ICommunitiesDto
    {
        Task<int> CreateComment(Community community);
        Task<bool> DeleteCommunity(int id);
        Task<Community> GetAnswerById(int id);
        Task<List<Community>> GetCommunity();
        Task<int> UpdateCommunity(Community update);
    }

    public class CommunitiesDto : ICommunitiesDto
    {
        protected readonly ApplicationDbContext _context;
        public CommunitiesDto(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<int> CreateComment(Community community)
        {
            try
            {
                if (community != null && community.Topic != null)
                {
                    //var existingtopic = _context.Topics.FirstOrDefault(x => x.TopicID == community.Topic);
                    var entity = _context.Communities.Add(
                        new Community
                        {
                            CommunityName = community.CommunityName,
                            Topic = community.Topic,
                            Description = community.Description,
                            IsAdultContent = community.IsAdultContent,

                        });
                    await _context.SaveChangesAsync();

                    return entity.Entity.CommunityID;
                }
            }
            catch { }

            return 0;
        }
        public async Task<int> UpdateCommunity(Community update)
        {
            try
            {
                var existing = await _context.Communities.FirstOrDefaultAsync(x => x.CommunityID == update.CommunityID);

                if (existing != null)
                {
                    existing.CommunityName = update.CommunityName;
                    existing.Topic = update.Topic;
                    existing.Description = update.Description;
                    existing.IsAdultContent = update.IsAdultContent;

                };
                await _context.SaveChangesAsync();
                
                if(existing!=null)
                    return existing.CommunityID;
                else
                {
                    return 0;
                }
            }
            catch (Exception ex) { }

            return 0;
        }
        public async Task<List<Community>> GetCommunity()
        {
            var community = new List<Community>();
            try
            {
                var existingCommunity = await _context.Communities.ToListAsync();

                community = existingCommunity;

                return community;
            }
            catch { }
            return null;
        }
        public async Task<Community> GetAnswerById(int id)
        {
            var Community = new Community();
            try
            {
                var TopicBy = await _context.Communities.FirstOrDefaultAsync(x => x.CommunityID == id);
                
                Community = TopicBy;

                return Community;

            }
            catch { }

            return null;
        }
        public async Task<bool> DeleteCommunity(int id)
        {

            try
            {
                var CommunityToDel = await _context.Communities.SingleOrDefaultAsync(x => x.CommunityID == id);
                if (CommunityToDel != null)
                {
                    _context.Communities.Remove(CommunityToDel);
                    await _context.SaveChangesAsync();

                    return true;
                }
            }
            catch { }

            return false;
        }
    }
}
