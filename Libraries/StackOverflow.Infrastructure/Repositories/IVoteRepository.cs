using StackOverflow.Infrastructure.Data;
using StackOverflow.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Infrastructure.Repositories
{
    public interface IVoteRepository : IRepository<Vote, int>
    {
    }
}