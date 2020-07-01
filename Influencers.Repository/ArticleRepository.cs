﻿using Influencers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Influencers.Repository.Abstractions
{
    public class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {
        public ArticleRepository(InfluencersContext dbContext) : base(dbContext)
        {

        }
        public IEnumerable<Article> GetArticlesByAuthorId(int authorId)
        {
            var articles = dbContext.Article.Where(a => a.AuthorId == authorId).AsEnumerable();
            return articles;
        }
        public override IEnumerable<Article> GetAll()
        {
            return dbContext.Article.Include(article => article.Author);
        }

        public Article GetArticleById(int id)
        {
            return dbContext.Article.Where(article => article.Id == id).SingleOrDefault();
        }

        public void UpdateArticleVotes(int articleId, int flag)
        {
            var article = dbContext.Article.Where(article => article.Id == articleId).SingleOrDefault();

            switch (flag)
            {
                case 1:
                    article.Votes++;
                    break;
                case 2:
                    article.Votes--;
                    break;
                case 3:
                    article.Votes += 2;
                    break;
                case 4:
                    article.Votes -= 2;
                    break;
            };

            dbContext.SaveChanges();
        }
    }
}
