using Influencers.Models;
using Influencers.Repository.Abstractions;
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
            throw new NotImplementedException();
        }

        public Author GetAuthorByEmail(string email)
        {
            return dbContext.Author.Where(a => a.Email == email).SingleOrDefault();
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
