using Influencers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Influencers.Repository.Abstractions
{
    public interface IArticle : IRepository<Article>
    {
        IEnumerable<Article> GetArticlesByAuthorId(int authorId);

        IEnumerable<Article> OrderArticlesByDateAscending();
        IEnumerable<Article> OrderArticlesByDateDescending();
        IEnumerable<Article> OrderArticlesByVotesAscending();
        IEnumerable<Article> OrderArticlesByVotesDescending();

     }
}
