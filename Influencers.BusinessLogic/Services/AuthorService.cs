using Influencers.Models;
using Influencers.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Influencers.BusinessLogic.Services
{
    public class AuthorService
    {
        private IAuthorRepository authorRepository;
        public AuthorService(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        public bool VerifyIfAuthorExistsByEmail(string email)
        {
            return authorRepository.VerifyIfAuthorExistsByEmail(email);
        }
        public IEnumerable<Author> OrderAuthorsDescendingByVotes(IEnumerable<Author> authors)
        {
            return authorRepository.OrderAuthorsDescendingByVotes(authors);
        }
        public void Add(string nickname, string email)
        {
            authorRepository.Add(new Author()
            {
                Nickname = nickname,
                Email = email,
                Votes = 0,
            }) ;
        }

        public IEnumerable<Author> GetAll()
        {
            return authorRepository.GetAll();
        }

        public Author GetAuthorByArticleId(int articleId)
        {
            return authorRepository.GetAuthorByArticleId(articleId);
        }

        public Author GetAuthorByCommentId(int commentId)
        {
            return authorRepository.GetAuthorByCommentId(commentId);
        }
        public Author GetAuthorByEmail(string email)
        {
            return authorRepository.GetAuthorByEmail(email);
        }

        public void UpdateAuthorPostVotes(Author author, int flag)
        {
            authorRepository.UpdateAuthorPostVotes(author, flag);
        }

        public void UpdateAuthorVotes(int authorId)
        {
            authorRepository.UpdateAuthorVotes(authorId);
        }

    }
}
