using Influencers.Models;
using Influencers.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Influencers.Repository
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(InfluencersContext dbContext) : base(dbContext)
        {

        }
        public Author GetAuthorByArticleId(int articleId)
        {
            var article = dbContext.Article.Where(a => a.Id== articleId).SingleOrDefault();

            var authorId =  article.AuthorId;

            var author = dbContext.Author.Where(a => a.Id == authorId).SingleOrDefault();

            return author;
        }

        public Author GetAuthorByEmail(string email)
        {
            return dbContext.Author.Where(a => a.Email == email).SingleOrDefault();
        }

        public IEnumerable<Author> OrderAuthorsDescendingByVotes(IEnumerable<Author> authors)
        {
            return authors.OrderByDescending(author => author.Votes);
        }

        public void UpdateAuthorPostVotes(Author author, int flag)
        {
            switch (flag)
            {
                case 1:
                    author.Votes++;
                    break;
                case 2:
                    author.Votes--;
                    break;
                case 3:
                    author.Votes += 2;
                    break;
                case 4:
                    author.Votes -= 2;
                    break;
            };

            dbContext.SaveChanges();
        }

        public bool VerifyIfAuthorExistsByEmail(string email)
        {
            var authors = dbContext.Author.Where(a => a.Email == email).Count();
            
            if(authors != 0)
            {
                return true;
            }

            return false;
        }
    }
}
