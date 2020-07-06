using Influencers.Models;
using Influencers.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Influencers.BusinessLogic.Services
{
    public class CommentService
    {
        private ICommentRepository commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }
        public IEnumerable<Comment> GetCommentsByArticleId(int id)
        {
            return commentRepository.GetCommentsByArticleId(id);
        }

        public void Add(Article article, Author author, string content)
        {
            commentRepository.Add(new Comment
            { Article = article, ArticleId = article.AuthorId, 
                Author = author, AuthorId = author.Id, 
                Content = content, AddedTime = DateTime.Now, Votes = 0 });
        }
    }
}
