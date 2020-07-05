using Influencers.Models;
using Influencers.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Influencers.BusinessLogic.Services
{
    public class ArticleTagsService
    {
        private IArticleTagsRepository articleTagsRepository;

        public ArticleTagsService(IArticleTagsRepository articleTagsRepository)
        {
            this.articleTagsRepository = articleTagsRepository;
        }

        public void Add(Tags tag, Article article)
        {
            articleTagsRepository.Add(new ArticleTags { Article = article, ArticleId = article.Id, Tag = tag, TagId = tag.Id});
        }

    }
}
