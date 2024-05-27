using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MyRazorPagesApp.Data;
using MyRazorPagesApp.Models;
using MyRazorPagesApp.ViewModel;

namespace MyRazorPagesApp.Dtos
{
    public interface IQuestionDto
    {
        Task<int> CreateQuestion(QuestionViewModel Questiom);
        Task<bool> DeleteQuestion(int id);
        Task<List<QuestionViewModel>> GetQuestion();
        List<QuestionViewModel> GetQuestionByPost(int postId);
        Task<QuestionViewModel> GetQuestionById(int id);
        Task<int> UpdateCommunity(QuestionViewModel update);
    }

    public class QuestionDto : IQuestionDto
    {
        protected readonly ApplicationDbContext _context;
        public QuestionDto(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<int> CreateQuestion(QuestionViewModel Question)
        {
            try
            {
                if (Question != null )
                {
                    var entity = _context.Posts.Add(
                        new Post
                        {
                            ParentPostId = Question.ParentPostId,
                            Title = Question.Title,
                            Content = Question.Content,
                            CreatedDate = DateTime.Now,
                            PostType = "Que",
                            UserId = Question.UserId
                        });
                    await _context.SaveChangesAsync();

                    return entity.Entity.Id;
                }
            }
            catch { }

            return 0;
        }
        public async Task<int> UpdateCommunity(QuestionViewModel update)
        {
            try
            {
                var existing = await _context.PostTopics.FirstOrDefaultAsync();

                if (existing != null)
                {
                    existing.PostID = update.ParentPostId.Value;
                };
                await _context.SaveChangesAsync();

                return existing.Id;
            }
            catch (Exception ex) { }

            return 0;
        }
        public async Task<List<QuestionViewModel>> GetQuestion()
        {
            var question = new List<QuestionViewModel>();
            try
            {
                var existingQuestion = await _context.PostTopics.ToListAsync();

                question = existingQuestion.Select(x => new QuestionViewModel
                {

                }).ToList();

                return question;
            }
            catch { }
            return null;
        }
        public async Task<QuestionViewModel> GetQuestionById(int id)
        {
            var Community = new QuestionViewModel();
            try
            {
                var TopicBy = await _context.PostTopics.FirstOrDefaultAsync(x => x.Id == id);

                Community = new QuestionViewModel
                {

                };

                return Community;

            }
            catch { }

            return null;
        }
        public async Task<bool> DeleteQuestion(int id)
        {

            try
            {
                var CommunityToDel = await _context.PostTopics.SingleOrDefaultAsync(x => x.Id == id);
                if (CommunityToDel != null)
                {
                    _context.PostTopics.Remove(CommunityToDel);
                    await _context.SaveChangesAsync();

                    return true;
                }
            }
            catch { }

            return false;
        }

        public List<QuestionViewModel> GetQuestionByPost(int postId)
        {
            var questions = new List<QuestionViewModel>();
            try
            {
                var posts = _context.Posts.AsEnumerable();

                var fromDb = posts.Where(x => x.ParentPostId== postId).ToList();

                questions = fromDb.Select(c => new QuestionViewModel
                {
                    Id = c.Id,
                    ParentPostId = c.ParentPostId,
                    ParentPostTitle = posts.FirstOrDefault(x => x.Id == c.ParentPostId)?.Title,
                    Content = c.Content,
                    CreatedDate = DateTime.Now,
                    Title = c.Title,
                    UserId = c.UserId,
                }).ToList();
            }
            catch { }

            return questions;
        }
    }
}
