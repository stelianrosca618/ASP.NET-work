using Microsoft.EntityFrameworkCore;
using MyRazorPagesApp.Data;
using MyRazorPagesApp.Models;
using MyRazorPagesApp.ViewModel;

namespace MyRazorPagesApp.Dtos
{
    public interface IAnswerDto
    {
        Task<int> CreateAnswer(AnswerViewModel answer);
        Task<bool> DeleteAnswer(int id);
        Task<List<AnswerViewModel>> GetAnswers();
        Task<int> UpdateAnswer(AnswerViewModel update);
        Task<List<AnswerViewModel>> GetAnswersByUser(string userId);
    }

    public class AnswerDto : IAnswerDto
    {
        protected readonly ApplicationDbContext _context;
        public AnswerDto(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<int> CreateAnswer(AnswerViewModel answer)
        {
            try
            {
                if (answer != null && answer.PostTopicId != 0)
                {
                    var existingTopicPost = await _context.PostTopics.FirstOrDefaultAsync(x => x.Id == answer.PostTopicId);
                    if (existingTopicPost != null)
                    {
                        var entity = _context.Answers.Add(
                            new Answer
                            {
                                Content = answer.Content,
                                PostId = answer.PostId,
                                CreatedDate = DateTime.Now, 
                                UserId = answer.UserId
                            });
                        await _context.SaveChangesAsync();

                        return entity.Entity.Id;
                    }
                }
            }
            catch { }

            return 0;
        }
        public async Task<int> UpdateAnswer(AnswerViewModel update)
        {
            try
            {
                var existing = await _context.Answers.FirstOrDefaultAsync(x => x.Id == update.Id);

                if (existing != null)
                {
                    existing.Content = update.Content;
                    existing.CreatedDate = DateTime.Now;
                };
                await _context.SaveChangesAsync();

                return existing.Id;
            }
            catch (Exception ex) { }

            return 0;
        }
        public async Task<List<AnswerViewModel>> GetAnswers()
        {
            var Answers = new List<AnswerViewModel>();
            try
            {
                var existingPost = await _context.Answers.ToListAsync();

                Answers = existingPost.Select(x => new AnswerViewModel
                {
                    Content = x.Content,
                    Id = x.Id,
                    CreatedDate = x.CreatedDate,
                    DownVote = x.DownVote,
                    PostId = x.PostId,
                    UpVote = x.UpVote,
                    UserId = x.UserId,
                }).ToList();

                return Answers;
            }
            catch { }
            return null;
        }

        public async Task<List<AnswerViewModel>> GetAnswersByUser(string userId)
        {
            var Answers = new List<AnswerViewModel>();
            try
            {
                var existingAns = await _context.Answers.Where(x => x.UserId == userId).ToListAsync();
                
                var postIds = existingAns.Select(x => x.PostId).ToList();

                var existingPost = await _context.Posts.Where(x => postIds.Contains(x.Id)).ToListAsync();

                Answers = existingAns.Select(x => new AnswerViewModel
                {
                    Content = x.Content,
                    Id = x.Id,
                    CreatedDate = x.CreatedDate,
                    Question = existingPost.FirstOrDefault(q => q.Id == x.PostId)?.Title,
                    DownVote = x.DownVote,
                    PostId = x.PostId,
                    UpVote = x.UpVote,
                    UserId = x.UserId,
                }).ToList();



                return Answers;
            }
            catch { }
            return Answers;
        }

        //public async Task<Answer> GetAnsByTopic(int id)
        //{
        //    var Post = new Answer();
        //    try
        //    {
        //        var Ans = await _context.Answers.FirstOrDefaultAsync(x => x.PostTopicId == id);
        //        JavaScriptSerializer serializer = new JavaScriptSerializer();
        //        string jsonResult = serializer.Serialize(Ans);
        //        Post = serializer.Deserialize<Answer>(jsonResult);

        //        return Post;

        //    }
        //    catch { }

        //    return null;
        //}
        public async Task<bool> DeleteAnswer(int id)
        {

            try
            {
                var TopicToDel = await _context.Answers.SingleOrDefaultAsync(x => x.Id == id);
                if (TopicToDel != null)
                {
                    _context.Answers.Remove(TopicToDel);
                    await _context.SaveChangesAsync();

                    return true;
                }
            }
            catch { }

            return false;
        }
    }
}
