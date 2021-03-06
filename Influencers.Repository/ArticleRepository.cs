﻿using Influencers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
            return dbContext.Article.Where(article => article.Id == id).Include(author => author.Author).SingleOrDefault();
        }

        public void UpdateArticleVotes(int articleId, int flag)
        {
            var article = dbContext.Article.Where(article => article.Id == articleId).SingleOrDefault();
            article.Votes += flag;
            dbContext.SaveChanges();
        }

        public IEnumerable<Article> GetPreviewedArticles(IEnumerable<Article> articles)
        {
            foreach(var article in articles)
            {
                article.Content = article.Content.Substring(0, article.Content.Length/2);
            }

            return articles; 
        }

        public IEnumerable<Article> OrderArticlesDescendinglyByVotes(IEnumerable<Article> articles)
        {
            articles = articles.OrderByDescending(article => article.Votes);

            return articles;
        }

        public IEnumerable<Article> OrderArticleMostRecent(IEnumerable<Article> articles)
        {
            articles = articles.OrderByDescending(article => article.AddedTime);

            return articles;
        }

        public IEnumerable<Article> CategorizeHot(IEnumerable<Article> articles)
        {
            List<Article> articlesList = articles.ToList();
 
            foreach(Article article in articlesList.ToList())
            {
                TimeSpan diff = DateTime.Now - article.AddedTime;
                double hours = diff.TotalHours;

                if(!(hours < 24 && article.Votes > 5))
                {
                    articlesList.Remove(article);
                }
            }

            return articlesList.AsEnumerable();
        }


        public Article GetNewestAddedArticle(string title, string content, string email)
        {
            return dbContext.Article.Where(a => a.Title == title)
                .Where(a => a.Content == content)
                .Where(a => a.Author.Email == email)
                .SingleOrDefault();
        }

        public IEnumerable<Article> SearchArticles(string content)
        {
            var articles = GetAll();

            List<Article> filteredArticles = new List<Article>();

            foreach(var article in articles)
            {
                if(article.Content.Contains(content) || article.Title.Contains(content))
                {
                    filteredArticles.Add(article);
                }
            }

            return filteredArticles.AsEnumerable();
        }

        public IEnumerable<Article> SearchArticlesByTags(MatchCollection tags)
        {
            var articles = dbContext.Article.Include(article => article.Tags).ThenInclude(tags => tags.Tag);

            List<Article> filteredArticles = new List<Article>();

            foreach (var article in articles.ToList())
            {
                var tagsOfArticle = article.Tags;

                foreach (var articleTag in tagsOfArticle)
                {
                    //if (tags.Contains(articleTag.Tag.))
                    //{
                    //    filteredArticles.Add(article);
                    //}

                    foreach(var tag in tags)
                    {
                        if (tag.ToString().Contains(articleTag.Tag.Name))
                        {
                            filteredArticles.Add(article);
                        }
                    }
                }
            }

            return filteredArticles;
        }
    }
}
