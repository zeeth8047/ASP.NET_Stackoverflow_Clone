using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Infrastructure.Services
{
    public interface IVoteService
    {
        void PostVoteUp(int postId, Guid userId);
        void PostVoteDown(int postId, Guid userId);
        void CommentVoteUp(int commentId, Guid userId);
        void CommentVoteDown(int commentId, Guid userId);
    }
}
