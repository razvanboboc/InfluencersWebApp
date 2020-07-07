using Influencers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Influencers.Repository.Abstractions
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IEnumerable<Comment> GetCommentsByArticleId(int id);
        public void UpdateCommentVotes(int commentId, int flag);


    }
}
