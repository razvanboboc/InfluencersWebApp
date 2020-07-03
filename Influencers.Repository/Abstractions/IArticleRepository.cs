using Influencers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Influencers.Repository.Abstractions
{
    public interface IArticleRepository : IRepository<Article>
    {
        IEnumerable<Article> GetArticlesByAuthorId(int authorId);
        Article GetArticleById(int id);
        Article GetNewestAddedArticle(string title, string content, string email);

        void UpdateArticleVotes(int articleId, int flag);

        IEnumerable<Article> GetPreviewedArticles(IEnumerable<Article> articles);

        IEnumerable<Article> OrderArticlesDescendinglyByVotes(IEnumerable<Article> articles);

        IEnumerable<Article> OrderArticleMostRecent(IEnumerable<Article> articles);

        IEnumerable<Article> CategorizeHot(IEnumerable<Article> articles);
    }
}
