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

        public IEnumerable<ArticleTags> GetArticlesIncludingTags()
        {
            var y = dbContext.Article.Include(article => article.Tags).ThenInclude(row => row.Tag).Select(articleTag => new { articleTag, articleTag.Tags }).AsEnumerable();
            var x = dbContext.ArticleTags.Include(articleTag => articleTag.Article).Include(articleTag => articleTag.Tag).Select(articleTag => new { articleTag.Article, articleTag.Tag }).AsEnumerable(); 
            return dbContext.ArticleTags.Include(articleTag => articleTag.Article).Include(articleTag => articleTag.Tag).AsEnumerable();
        }

        public IEnumerable<Tags> GetTagsOfArticleById(int articleId)
        {
            return dbContext.ArticleTags.Where(a => a.ArticleId == articleId).Select(t => t.Tag).AsEnumerable();
        }
    }
}
