using System;
using System.Collections.Generic;
using System.Text;
using Influencers.Models;
namespace Influencers.Repository.Abstractions
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Author GetAuthorByArticleId(int articleId);

        Author GetAuthorByEmail(string email);

        bool VerifyIfAuthorExistsByEmail(string email);

        IEnumerable<Author> OrderAuthorsDescendingByVotes(IEnumerable<Author> authors);
    }
}
