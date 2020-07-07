using Influencers.Models;
using Influencers.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
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
            return dbContext.Comment.Where(comment => comment.ArticleId == id).Include(comment => comment.Author).AsEnumerable();
        }

        public void UpdateCommentVotes(int commentId, int flag)
        {
            var comment = dbContext.Comment.Where(comment => comment.Id == commentId).SingleOrDefault();
            comment.Votes += flag;
            dbContext.SaveChanges();
        }
    }
}
