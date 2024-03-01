using StackOverflow.Infrastructure.BusinessObjects;

namespace StackOverflow.Infrastructure.Services
{
    public interface IPostService
    {
        void CreatePost(Post post);
        void UpdatePost(Post post);
        void DeletePost(int id);

        public (int total, int displayTotal, IList<Post> records) 
            GetAllPosts(int pageIndex, int pageSize, string searchText, string orderBy, Guid userId);
        
        public (int total, int displayTotal, IList<Post> records)
            GetPosts(int pageIndex, int pageSize, string searchText, string orderBy);
        
        Post GetPostById(int id);
        IList<Post> GetAllPosts();
    }
}
