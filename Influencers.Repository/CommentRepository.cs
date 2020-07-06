using Influencers.Models;
using Influencers.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Influencers.Repository
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(InfluencersContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<Comment> GetCommentsByArticleId(int id)
        {
            return dbContext.Comment.Where(comment => comment.ArticleId == id).AsEnumerable();
        }
    }
}
