using System;
using System.Collections.Generic;
using System.Text;
using Influencers.Models;
namespace Influencers.Repository.Abstractions
{
    public interface IAuthor : IRepository<Author>
    {
        Author GetAuthorByArticleId(int articleId);
    }
}
