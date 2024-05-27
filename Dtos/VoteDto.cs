using Microsoft.EntityFrameworkCore;
using MyRazorPagesApp.Components;
using MyRazorPagesApp.Data;
using MyRazorPagesApp.Models;
using System.ComponentModel.Design;

namespace MyRazorPagesApp.Dtos
{
    public interface IVoteDto
    {
        Task<int> CreateVote(Vote Vote);
        Task<bool> DeleteVote(int id);
        Task<List<VoteViewModel>> GetVote();
        Task<List<VoteViewModel>> GetVoteByEntity(int id, ContentEntity entity);
        Task<VoteViewModel> GetVote(int id);
        Task<int> UpdateVote(Vote update);
        Task<long> GetUpVoteByEntity(int id, ContentEntity entity);
        Task<long> GetDownVoteByEntity(int id, ContentEntity entity);
        bool CheckVoteByUser(int id, string userId, ContentEntity entity);
        Task<int> CreateVoteByEntity(VoteViewModel viewModel, ContentEntity entity);
    }

    public class VoteDto : IVoteDto
    {
        protected readonly ApplicationDbContext _context;
        public VoteDto(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<int> CreateVote(Vote Vote)
        {
            try
            {
                if (Vote != null)
                {
                    //var existingtopic = await _context.Posts.FirstOrDefaultAsync(x => x.Id == Vote.PostId);
                    var entity = _context.Votes.Add(
                        new Vote
                        {
                            Upvote = Vote.Upvote,
                            Downvote = Vote.Downvote,
                            CommentId = Vote.CommentId,
                            PostId = Vote.PostId,
                            AnswerId = Vote.AnswerId,
                            UserId = Vote.UserId,

                        });
                    await _context.SaveChangesAsync();

                    return entity.Entity.VoteId;
                }
            }
            catch { }

            return 0;
        }
        public async Task<int> UpdateVote(Vote update)
        {
            try
            {
                var existing = await _context.Votes.FirstOrDefaultAsync(x => x.VoteId == update.VoteId);
                //when there is need to test the vote kindly update logic post/question present in datbase or not
                if (existing != null)
                {
                    existing.Upvote = update.Upvote;
                    existing.Downvote = update.Downvote;
                    existing.PostId = update.PostId;
                    existing.UserId = update.UserId;
                    existing.CommentId = existing.CommentId;
                    existing.AnswerId = existing.AnswerId;
                };
                await _context.SaveChangesAsync();

                return existing.VoteId;
            }
            catch (Exception ex) { }

            return 0;
        }
        public async Task<List<VoteViewModel>> GetVote()
        {
            var vote = new List<VoteViewModel>();
            try
            {
                var existingVote = await _context.Votes.ToListAsync();

                vote = existingVote.Select(x=> new VoteViewModel
                {
                    AnswerId = x.AnswerId,
                    CommentId = x.CommentId,
                    Downvote = x.Downvote,
                    PostId = x.PostId,
                    Upvote = x.Upvote,
                    UserId = x.UserId,
                    VoteId = x.VoteId
                }).ToList();
                return vote;
            }
            catch { }

            return vote;
        }
        public async Task<VoteViewModel> GetVote(int id)
        {
            var Community = new VoteViewModel();
            try
            {
                var x = await _context.Votes.FirstOrDefaultAsync(x => x.VoteId == id);
                
                Community = new VoteViewModel
                {
                    AnswerId = x.AnswerId,
                    CommentId = x.CommentId,
                    Downvote = x.Downvote,
                    PostId = x.PostId,
                    Upvote = x.Upvote,
                    UserId = x.UserId,
                    VoteId = x.VoteId
                };

                return Community;

            }
            catch { }

            return null;
        }
        public async Task<bool> DeleteVote(int id)
        {

            try
            {
                var CommunityToDel = await _context.Votes.SingleOrDefaultAsync(x => x.VoteId == id);
                if (CommunityToDel != null)
                {
                    _context.Votes.Remove(CommunityToDel);
                    await _context.SaveChangesAsync();

                    return true;
                }
            }
            catch { }

            return false;
        }
        public async Task<int> CreateVoteByEntity(VoteViewModel viewModel, ContentEntity entity)
        {
            try
            {
                var newVote = new Vote();

                if (entity == ContentEntity.Post)
                {
                    newVote = new Vote
                    {
                        PostId = viewModel.Id
                    };
                }
                else if (entity == ContentEntity.Question)
                {
                    newVote = new Vote
                    {
                        PostId = viewModel.Id
                    };
                }
                else if (entity == ContentEntity.Answer)
                {
                    newVote = new Vote
                    {
                        AnswerId = viewModel.Id
                    };
                }
                else if (entity == ContentEntity.Comment)
                {
                    newVote = new Vote
                    {
                        CommentId = viewModel.Id
                    };
                }
                newVote.Upvote = viewModel.Upvote;
                newVote.Downvote = viewModel.Downvote;
                newVote.UserId = viewModel.UserId;

                var newEntity = _context.Votes.Add(newVote);
                await _context.SaveChangesAsync();

                return newEntity.Entity.VoteId;
            }
            catch { }

            return 0;
        }
        public async Task<List<VoteViewModel>> GetVoteByEntity(int id, ContentEntity entity)
        {
            var vote = new List<VoteViewModel>();
            try
            {
                var existingVote = new List<Vote>();

                if (entity == ContentEntity.Post)
                {
                    existingVote = await _context.Votes.Where(x => x.PostId == id).ToListAsync();
                }
                else if (entity == ContentEntity.Question)
                {
                    existingVote = await _context.Votes.Where(x => x.PostId == id).ToListAsync();
                }
                else if (entity == ContentEntity.Answer)
                {
                    existingVote = await _context.Votes.Where(x => x.AnswerId == id).ToListAsync();
                }
                else if (entity == ContentEntity.Comment)
                {
                    existingVote = await _context.Votes.Where(x => x.CommentId == id).ToListAsync();
                }

                vote = existingVote.Select(x => new VoteViewModel
                {
                    AnswerId = x.AnswerId,
                    CommentId = x.CommentId,
                    Downvote = x.Downvote,
                    PostId = x.PostId,
                    Upvote = x.Upvote,
                    UserId = x.UserId,
                    VoteId = x.VoteId
                }).ToList();
                return vote;
            }
            catch { }

            return vote;
        }

        public async Task<long> GetUpVoteByEntity(int id, ContentEntity entity)
        {
            long vote = 0;
            try
            {
                var existingVote = new List<Vote>();

                if (entity == ContentEntity.Post)
                {
                    existingVote = await _context.Votes.Where(x => x.PostId == id).ToListAsync();
                }
                else if (entity == ContentEntity.Question)
                {
                    existingVote = await _context.Votes.Where(x => x.PostId == id).ToListAsync();
                }
                else if (entity == ContentEntity.Answer)
                {
                    existingVote = await _context.Votes.Where(x => x.AnswerId == id).ToListAsync();
                }
                else if (entity == ContentEntity.Comment)
                {
                    existingVote = await _context.Votes.Where(x => x.CommentId == id).ToListAsync();
                }

                vote = existingVote.Sum(x => x.Upvote);
                return vote;
            }
            catch { }

            return vote;
        }

        public async Task<long> GetDownVoteByEntity(int id, ContentEntity entity)
        {
            long vote = 0;
            try
            {
                var existingVote = new List<Vote>();

                if (entity == ContentEntity.Post)
                {
                    existingVote = await _context.Votes.Where(x => x.PostId == id).ToListAsync();
                }
                else if (entity == ContentEntity.Question)
                {
                    existingVote = await _context.Votes.Where(x => x.PostId == id).ToListAsync();
                }
                else if (entity == ContentEntity.Answer)
                {
                    existingVote = await _context.Votes.Where(x => x.AnswerId == id).ToListAsync();
                }
                else if (entity == ContentEntity.Comment)
                {
                    existingVote = await _context.Votes.Where(x => x.CommentId == id).ToListAsync();
                }

                vote = existingVote.Sum(x => x.Downvote);
                return vote;
            }
            catch { }

            return vote;
        }

        public bool CheckVoteByUser(int id,string userId, ContentEntity entity)
        {
            try
            {
                if (entity == ContentEntity.Post)
                {
                    return _context.Votes.Any(x => x.PostId == id && x.UserId == userId);
                }
                else if (entity == ContentEntity.Question)
                {
                    return _context.Votes.Any(x => x.PostId == id && x.UserId == userId);
                }
                else if (entity == ContentEntity.Answer)
                {
                    return _context.Votes.Any(x => x.AnswerId == id && x.UserId == userId);
                }
                else if (entity == ContentEntity.Comment)
                {
                    return _context.Votes.Any(x => x.CommentId == id && x.UserId == userId);
                }
            }
            catch { }

            return false;
        }
    }
}

