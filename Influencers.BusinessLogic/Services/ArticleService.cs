using Influencers.Models;
using Influencers.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Influencers.BusinessLogic.Services
{
    public class ArticleService
    {
        private IArticleRepository articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public IEnumerable<Article> GetAll()
        {
            var articles = articleRepository.GetAll();
            
            return articles;
        }

        public void AddArticle(string title, string content)
        {
            articleRepository.Add(new Article()
            {
                Title = title,
                Content = content,
                Date = DateTime.Now,
            });
        }
    }
}
