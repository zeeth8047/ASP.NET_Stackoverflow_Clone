using AutoMapper;
using StackOverflow.Infrastructure.UnitOfWorks;
using CommentEntity = StackOverflow.Infrastructure.Entities.Comment;
using CommentBO = StackOverflow.Infrastructure.BusinessObjects.Comment;

namespace StackOverflow.Infrastructure.Services
{
    public class CommentService : ICommentService
    {
        private readonly IStackOverflowUnitOfWork _stackOverflowUnitOfWork;
        private IMapper _mapper;

        public CommentService(IStackOverflowUnitOfWork stackOverflowUnitOfWork,
            IMapper mapper)
        {
            _stackOverflowUnitOfWork = stackOverflowUnitOfWork;
            _mapper = mapper;
        }

        public void CreateComment(CommentBO comment)
        {
            if(comment == null)
            {
                throw new ArgumentNullException("No comment string written.");
            }

            var commentEntity = _mapper.Map<CommentEntity>(comment);
            _stackOverflowUnitOfWork.CommentRepository.Add(commentEntity);
            _stackOverflowUnitOfWork.Save();
        }

        public void UpdateComment(CommentBO comment)
        {
            try
            {
                var commentEntity = _stackOverflowUnitOfWork.CommentRepository.GetById(comment.Id);
                commentEntity.Description = comment.Description;
                _stackOverflowUnitOfWork.Save();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }

        public void DeleteComment(int id)
        {
            try
            {
                _stackOverflowUnitOfWork.CommentRepository.Remove(id);
                _stackOverflowUnitOfWork.Save();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }

        public CommentBO GetCommentById(int id)
        {
            try
            {
                var commentEntity = _stackOverflowUnitOfWork.CommentRepository
                .Get(x => x.Id == id, "ApplicationUser,Post").FirstOrDefault();

                var comment = _mapper.Map<CommentBO>(commentEntity);
                return comment;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }

        public int CommentApprove(int id)
        {
            try
            {
                var commentEntity = _stackOverflowUnitOfWork.CommentRepository
                .Get(x => x.Id == id, "Post").FirstOrDefault();

                var postId = commentEntity.PostId;
                commentEntity.IsAcceptedAsAnswer = true;
                _stackOverflowUnitOfWork.Save();

                return postId;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }

        public int CommentDisapprove(int id)
        {
            try
            {
                var commentEntity = _stackOverflowUnitOfWork.CommentRepository
                .Get(x => x.Id == id, "Post").FirstOrDefault();

                var postId = commentEntity.PostId;
                commentEntity.IsAcceptedAsAnswer = false;
                _stackOverflowUnitOfWork.Save();

                return postId;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }
    }
}