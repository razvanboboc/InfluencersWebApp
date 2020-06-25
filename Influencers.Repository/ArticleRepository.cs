using Influencers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Influencers.Repository.Abstractions
{
    public class ArticleRepository : BaseRepository<Article>, IArticle
    {
        public ArticleRepository(InfluencersContext dbContext) : base(dbContext)
        {

        }
        public IEnumerable<Article> GetArticlesByAuthorId(int authorId)
        {
            var articles = dbContext.Article.Where(a => a.AuthorId == authorId).AsEnumerable();
            return articles;
        }

        public IEnumerable<Article> OrderArticlesByDateAscending()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Article> OrderArticlesByDateDescending()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Article> OrderArticlesByVotesAscending()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Article> OrderArticlesByVotesDescending()
        {
            throw new NotImplementedException();
        }
    }
}
