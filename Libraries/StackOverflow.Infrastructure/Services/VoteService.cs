using AutoMapper;
using StackOverflow.Infrastructure.UnitOfWorks;
using VoteEntity = StackOverflow.Infrastructure.Entities.Vote;

namespace StackOverflow.Infrastructure.Services
{
    public class VoteService : IVoteService
    {
        private readonly IStackOverflowUnitOfWork _stackOverflowUnitOfWork;
        private readonly IMapper _mapper;

        public VoteService(IStackOverflowUnitOfWork stackOverflowUnitOfWork,
            IMapper mapper)
        {
            _stackOverflowUnitOfWork = stackOverflowUnitOfWork;
            _mapper = mapper;
        }

        public void PostVoteUp(int postId, Guid userId)
        {
            try
            {
                var voteEntity = _stackOverflowUnitOfWork.VoteRepository
                    .Get(x => x.PostId == postId && x.UserId == userId, string.Empty).FirstOrDefault();
                
                var entity = new VoteEntity();
                if (voteEntity is null)
                {
                    entity.PostId = postId;
                    entity.UserId = userId;
                    _stackOverflowUnitOfWork.VoteRepository.Add(entity);
                }
                _stackOverflowUnitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void PostVoteDown(int postId, Guid userId)
        {
            try
            {
                var voteEntity = _stackOverflowUnitOfWork.VoteRepository
                    .Get(x => x.PostId == postId && x.UserId == userId, string.Empty).FirstOrDefault();
                
                if (voteEntity != null)
                    _stackOverflowUnitOfWork.VoteRepository.Remove(voteEntity);

                _stackOverflowUnitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void CommentVoteUp(int commentId, Guid userId)
        {
            try
            {
                var voteEntity = _stackOverflowUnitOfWork.VoteRepository
                    .Get(x => x.CommentId == commentId && x.UserId == userId, string.Empty).FirstOrDefault();
                
                var entity = new VoteEntity();
                if (voteEntity is null)
                {
                    entity.CommentId = commentId;
                    entity.UserId = userId;
                    _stackOverflowUnitOfWork.VoteRepository.Add(entity);
                }
                _stackOverflowUnitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void CommentVoteDown(int commentId, Guid userId)
        {
            try
            {
                var voteEntity = _stackOverflowUnitOfWork.VoteRepository.
                    Get(x => x.CommentId == commentId && x.UserId == userId, string.Empty).FirstOrDefault();
                
                if (voteEntity != null)
                    _stackOverflowUnitOfWork.VoteRepository.Remove(voteEntity);

                _stackOverflowUnitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
