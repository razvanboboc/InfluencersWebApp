using Influencers.Models;
using Influencers.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Influencers.Repository
{
    public class ArticleTagsRepository : BaseRepository<ArticleTags>, IArticleTagsRepository
    {
        public ArticleTagsRepository(InfluencersContext dbContext) : base(dbContext)
        {

        }


        public IEnumerable<Tags> GetTagsOfArticleById(int articleId)
        {
            return dbContext.ArticleTags.Where(a => a.ArticleId == articleId).Select(t => t.Tag).AsEnumerable();
        }
    }
}
