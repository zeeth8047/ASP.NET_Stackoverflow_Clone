using AutoMapper;
using StackOverflow.Infrastructure.UnitOfWorks;
using PostEntity = StackOverflow.Infrastructure.Entities.Post;
using PostBO = StackOverflow.Infrastructure.BusinessObjects.Post;
using System.Security.Cryptography.X509Certificates;

namespace StackOverflow.Infrastructure.Services
{
    public class PostService : IPostService
    {
        private readonly IStackOverflowUnitOfWork _stackOverflowUnitOfWork;
        private IMapper _mapper;

        public PostService(IStackOverflowUnitOfWork stackOverflowUnitOfWork,
            IMapper mapper)
        {
            _stackOverflowUnitOfWork = stackOverflowUnitOfWork;
            _mapper = mapper;
        }

        public void CreatePost(PostBO post)
        {
            try
            {
                var postCount = _stackOverflowUnitOfWork.PostRepository
                .GetCount(x => x.Title == post.Title);

                if (postCount == 0)
                {
                    var entity = _mapper.Map<PostEntity>(post);

                    _stackOverflowUnitOfWork.PostRepository.Add(entity);
                    _stackOverflowUnitOfWork.Save();
                } 
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }

        public void UpdatePost(PostBO post)
        {
            try
            {
                var count = _stackOverflowUnitOfWork.PostRepository
                .GetCount(x => x.Title == post.Title && x.Id != post.Id);

                var postEntity = _stackOverflowUnitOfWork.PostRepository.GetById(post.Id);
                _mapper.Map(post, postEntity);
                _stackOverflowUnitOfWork.Save();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }

        public void DeletePost(int id)
        {
            try
            {
                _stackOverflowUnitOfWork.PostRepository.Remove(id);
                _stackOverflowUnitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public PostBO GetPostById(int id)
        {
            try
            {
                var postEntity = _stackOverflowUnitOfWork.PostRepository.
                Get(x => x.Id == id, "ApplicationUser,Comments,Comments.ApplicationUser,Tags,Votes").FirstOrDefault();

                var post = _mapper.Map<PostBO>(postEntity);
                return post;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public IList<PostBO> GetAllPosts()
        {
            try
            {
                var posts = new List<PostBO>();

                var postEntities = _stackOverflowUnitOfWork.PostRepository.GetAll();

                foreach (PostEntity entity in postEntities)
                {
                    posts.Add(_mapper.Map<PostBO>(entity));
                }

                return posts;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public (int total, int displayTotal, IList<PostBO> records) 
            GetAllPosts(int pageIndex, int pageSize, string searchText, string orderBy, Guid userId)
        {
            try
            {
                var posts = new List<PostBO>();

                var result = _stackOverflowUnitOfWork.PostRepository
                    .GetDynamic(x => x.UserId == userId,
                        orderBy, "ApplicationUser,Tags", pageIndex, pageSize, true);

                if (!string.IsNullOrEmpty(searchText))
                {
                    result = _stackOverflowUnitOfWork.PostRepository
                        .GetDynamic(x => x.UserId == userId && x.Title.Contains(searchText),
                        orderBy, "ApplicationUser,Tags", pageIndex, pageSize, true);
                }

                foreach (PostEntity entitiy in result.data)
                {
                    posts.Add(_mapper.Map<PostBO>(entitiy));
                }
                return (result.total, result.totalDisplay, posts);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public (int total, int displayTotal, IList<PostBO> records)
            GetPosts(int pageIndex, int pageSize, string searchTerm, string orderBy)
        {
            try
            {
                var result = _stackOverflowUnitOfWork.PostRepository.GetDynamic(null,
                orderBy, "ApplicationUser,Tags,Votes", pageIndex, pageSize, true);

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    result = _stackOverflowUnitOfWork.PostRepository.GetDynamic(x => x.Title.Contains(searchTerm),
                        orderBy, "ApplicationUser,Tags,Votes", pageIndex, pageSize, true);
                }

                var posts = new List<PostBO>();

                foreach (PostEntity entity in result.data)
                {
                    posts.Add(_mapper.Map<PostBO>(entity));
                }

                return (result.total, result.totalDisplay, posts);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
