using Influencers.Models;
using Influencers.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Influencers.BusinessLogic.Services
{
    public class ArticleService
    {
        private IArticleRepository articleRepository;
        private IAuthorRepository authorRepository;

        public ArticleService(IArticleRepository articleRepository, IAuthorRepository authorRepository)
        {
            this.articleRepository = articleRepository;
            this.authorRepository = authorRepository;
        }
        public void UpdateArticleVotes(int articleId, bool flag)
        {
            articleRepository.UpdateArticleVotes(articleId, flag);
        }

        public IEnumerable<Article> GetAll()
        {
            var articles = articleRepository.GetAll();
            
            return articles;
        }

        public void AddArticle(string title, string content, string email)
        {
            var author = authorRepository.GetAuthorByEmail(email);

            articleRepository.Add(new Article()
            {
                Title = title,
                Content = content,
                AddedTime = DateTime.Now,
                Author = author,
                Votes = 0,
            }) ;
        }

        public Article GetArticleById(int id)
        {
            return articleRepository.GetArticleById(id);
        }
    }
}
