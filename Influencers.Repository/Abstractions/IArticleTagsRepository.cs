using Influencers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Influencers.Repository.Abstractions
{
    public interface IArticleTagsRepository : IRepository<ArticleTags>
    {
        IEnumerable<Tags> GetTagsOfArticleById(int articleId);

        IEnumerable<Article> GetArticlesIncludingTags();

    }
}
